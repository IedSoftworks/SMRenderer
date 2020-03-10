using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SMRenderer.Animations;
using SMRenderer.Data;
using SMRenderer.Input;
using SMRenderer.Visual.Drawing;
using SMRenderer.Visual.Renderers;
using Keyboard = OpenTK.Input.Keyboard;
using Mouse = SMRenderer.Input.Mouse;

namespace SMRenderer.Visual
{
    /// <summary>
    ///     The actual window-class. Is child of the OpenTK GameWindow-class
    /// </summary>
    public class GLWindow : GameWindow
    {
        private void ApplyBloom()
        {
            GL.UseProgram(BloomRenderer.program.mProgramId);

            int loopCount = 4;

            for (int i = 0; i < loopCount; i++)
            {
                int sourceTex;
                if (i % 2 == 0)
                {
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, _framebufferIdBloom1);
                    if (i == 0) sourceTex = _framebufferTextureBloomDownsampled;
                    else sourceTex = _framebufferTextureBloom2;
                }
                else
                {
                    sourceTex = _framebufferTextureBloom1;
                    if (i == loopCount - 1) GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                    else GL.BindFramebuffer(FramebufferTarget.Framebuffer, _framebufferIdBloom2);
                }

                BloomRenderer.program.DrawBloom(
                    ref _modelMatrixBloom,
                    i % 2 == 0,
                    i == loopCount - 1,
                    Width,
                    Height,
                    _framebufferTextureMainDownsampled,
                    sourceTex);
            }

            GL.UseProgram(0);
        }

        public static bool CheckGLErrors()
        {
            bool hasError = false;
            ErrorCode c;
            while ((c = GL.GetError()) != ErrorCode.NoError)
            {
                hasError = true;
                Debug.WriteLine(c.ToString());
            }

            return hasError;
        }

        #region | Normal Rendering |

        public RendererCollection rendererList = new RendererCollection();
        private Matrix4 _modelMatrixBloom;
        public float deltatimeScale = 1;

        public Mouse mouse;
        public Camera camera;
        public GameController controller;

        /// <summary>
        ///     Represents the Current window to the public
        /// </summary>
        public static GLWindow Window { get; private set; }

        /// <summary>
        ///     Represents the Current Size of the drawing board
        /// </summary>
        public Vector2 PxSize { get; private set; } = new Vector2(0);

        /// <summary>
        ///     Stores a System.Drawing.Rectangle for intersect checks
        ///     <para>Contains only values for the drawing board.</para>
        ///     <para>For the actual window rectangle check ClientRectangle</para>
        /// </summary>
        public Rectangle WindowRect { get; private set; }

        public bool ErrorAtLoading = false;

        public GLWindow(int width, int height) : base(width, height)
        {
            Title = "GLWíndow Default Title";
            Window = this;

            mouse = new Mouse {window = this};
            if (GeneralConfig.UseGameController) controller = new GameController(this);

            MouseMove += mouse.SaveMousePosition;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused && GeneralConfig.OnlyRenderIfFocused) return;

            KeyboardState state = Keyboard.GetState();

            double time = (float) e.Time * deltatimeScale;

            SMGlobals.currentDeltaTime = time;
            SMGlobals.currentDeltaTimeUnscaled = e.Time;

            Timer.TickChange(time);
            Animation.Update(time);
            controller?.Check();

            Scene.Current.matrixSetFunc(Scene.Current);
            Scene.Current.lights.CreateShaderArgs();

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            double time = e.Time * deltatimeScale;
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, GraficalConfig.AllowBloom ? _framebufferIdMain : 0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Baseplate.Prepare(e.Time);
            Baseplate.Draw(Camera.staticView, (GenericObjectRenderer) rendererList[rendererList["GeneralRenderer"]]);

            foreach (SMLayer layer in Scene.Current.DrawLayer.Values) layer.Order();
            Scene.Current.DrawLayer =
                Scene.Current.DrawLayer.OrderBy(a => a.Key).ToDictionary(a => a.Key, b => b.Value);

            foreach (KeyValuePair<int, SMLayer> pair in Scene.Current.DrawLayer)
            {
                pair.Value.ToList().ForEach(a => a.Prepare(time));
                pair.Value.ForEach(a =>
                    a.Draw(pair.Value.matrix, (GenericObjectRenderer) rendererList[pair.Value.renderer]));
            }

            Scene.Overlayer.Order();
            Scene.Overlayer.ToList().ForEach(a => a.Prepare(time));
            Scene.Overlayer.ForEach(a =>
                a.Draw(Scene.Overlayer.matrix, (GenericObjectRenderer) rendererList[Scene.Overlayer.renderer]));

            DownsampleFramebuffer();
            if (GraficalConfig.AllowBloom) ApplyBloom();

            SwapBuffers();
        }

        public DrawItem Baseplate;

        protected override void OnLoad(EventArgs e)
        {
            GeneralConfig.Renderer.ForEach(a =>
            {
                rendererList.Add((GenericRenderer) Activator.CreateInstance(a, this));
            });

            DataManager.C = GeneralConfig.UseDataManager == null
                ? DataManager.Create()
                : DataManager.Load(GeneralConfig.UseDataManager);

            Preload();

            Baseplate = new DrawItem
            {
                Color = GraficalConfig.ClearColor,
                connected = this,
                positionAnchor = "lu",
                purpose = "The base plate of the skyplane. Prevents the bloom-Effect to play crazy."
            };

            Scene.Current = Scene.Default = new Scene();
            Scene.Current.GenerateDrawLayer();

            ApplySize();

            TargetUpdateFrequency = GeneralConfig.UpdatePerSecond;

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);

            GL.ClearColor(Color.Black);

            base.OnLoad(e);

            if (ErrorAtLoading)
            {
                Console.WriteLine("Error detected. Check console output.");
                Console.WriteLine("Continue?");
            }

            Console.Clear();
        }

        public static void Preload()
        {
            Texture.CreateEmpty();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(ClientRectangle);

            ApplySize();

            InitializeFramebuffers(0);
        }

        private void ApplySize()
        {
            if (GeneralConfig.UseScale)
            {
                float aspect = (float) Width / Height;
                PxSize = new Vector2(GeneralConfig.Scale, GeneralConfig.Scale / aspect);
            }
            else
            {
                PxSize = new Vector2(Width, Height);
            }


            Camera.UpdateProjection(PxSize);
            //staticViewProjection = Matrix4.LookAt(pxSize.X/2, pxSize.Y / 2, 1, pxSize.X / 2, pxSize.Y / 2, 0, 0, 1, 0) * Matrix4.CreateOrthographicOffCenter(0, pxSize.X, pxSize.Y, 0, .1f, 100f);

            _modelMatrixBloom = Matrix4.CreateScale(Width, Height, 1) *
                                Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                                Matrix4.CreateOrthographic(PxSize.X, PxSize.Y, .1f, 100f);

            Baseplate.Size = PxSize;
            WindowRect = new Rectangle(0, 0, (int) PxSize.X, (int) PxSize.Y);
        }

        #endregion

        #region | Framebuffers |

        private int _framebufferIdMain = -1;
        private int _framebufferIdMainDownsampled = -1;
        private int _framebufferIdBloom1 = -1;
        private int _framebufferIdBloom2 = -1;

        private int _framebufferTextureMain = -1;
        private int _framebufferTextureMainDownsampled = -1;
        private int _framebufferTextureBloom = -1;
        private int _framebufferTextureBloom1 = -1;
        private int _framebufferTextureBloom2 = -1;
        private int _framebufferTextureBloomDownsampled = -1;

        private void InitializeFramebuffers(int fsaa)
        {
            DeleteFramebuffers();

            // Sometimes, frame buffer initialization fails
            // if the window gets resized too often.
            // I found no better way around this:
            Thread.Sleep(50);

            InitFramebufferMain(fsaa);
            InitFramebufferMainDownsampled();
            InitFramebufferBloom();
        }

        private void DeleteFramebuffers()
        {
            GL.DeleteTextures(6,
                new[]
                {
                    _framebufferTextureMain, _framebufferTextureMainDownsampled, _framebufferTextureBloom,
                    _framebufferTextureBloom1, _framebufferTextureBloom2, _framebufferTextureBloomDownsampled
                });
            GL.DeleteFramebuffers(4,
                new[] {_framebufferIdMain, _framebufferIdMainDownsampled, _framebufferIdBloom1, _framebufferIdBloom2});
        }

        private void InitFramebufferMain(int fsaa)
        {
            int framebufferId = -1;
            int renderedTexture = -1;
            int renderedTextureAttachment = -1;
            int renderbufferFSAA = -1;
            int renderbufferFSAA2 = -1;

            framebufferId = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferId);

            renderedTexture = GL.GenTexture();
            renderedTextureAttachment = GL.GenTexture();


            GL.DrawBuffers(2,
                new DrawBuffersEnum[2] {DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1});

            GL.BindTexture(TextureTarget.Texture2D, renderedTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8,
                Width, Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (float) TextureMinFilter.Nearest);


            renderbufferFSAA = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbufferFSAA);
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, fsaa, RenderbufferStorage.Rgba8, Width,
                Height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                RenderbufferTarget.Renderbuffer, renderbufferFSAA);

            GL.BindTexture(TextureTarget.Texture2D, renderedTextureAttachment);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8,
                Width, Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (float) TextureMinFilter.Nearest);

            renderbufferFSAA2 = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbufferFSAA2);
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, fsaa, RenderbufferStorage.Rgba8, Width,
                Height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1,
                RenderbufferTarget.Renderbuffer, renderbufferFSAA2);

            FramebufferErrorCode code = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (code != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("GL_FRAMEBUFFER_COMPLETE failed. Cannot use FrameBuffer object.");

            _framebufferIdMain = framebufferId;
            _framebufferTextureMain = renderedTexture;
            _framebufferTextureBloom = renderedTextureAttachment;
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void InitFramebufferMainDownsampled()
        {
            int framebufferId = -1;
            int renderedTexture = -1;
            int renderedTextureAttachment = -1;

            framebufferId = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferId);

            renderedTexture = GL.GenTexture();
            renderedTextureAttachment = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, renderedTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8,
                Width, Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (float) TextureParameterName.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (float) TextureParameterName.ClampToEdge);


            GL.BindTexture(TextureTarget.Texture2D, renderedTextureAttachment);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8,
                Width, Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (float) TextureParameterName.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (float) TextureParameterName.ClampToEdge);

            GL.DrawBuffers(2,
                new DrawBuffersEnum[2] {DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1});
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                renderedTexture, 0);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1,
                renderedTextureAttachment, 0);

            FramebufferErrorCode code = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (code != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("GL_FRAMEBUFFER_COMPLETE failed. Cannot use FrameBuffer object.");

            _framebufferIdMainDownsampled = framebufferId;
            _framebufferTextureMainDownsampled = renderedTexture;
            _framebufferTextureBloomDownsampled = renderedTextureAttachment;
        }

        private void InitFramebufferBloom()
        {
            int framebufferTempId = -1;
            int renderedTextureTemp = -1;

            // =========== TEMP #1 ===========
            framebufferTempId = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferTempId);

            renderedTextureTemp = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, renderedTextureTemp);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                Width, Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (float) TextureParameterName.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (float) TextureParameterName.ClampToEdge);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                renderedTextureTemp, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            FramebufferErrorCode code = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (code != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("GL_FRAMEBUFFER_COMPLETE failed. Cannot use FrameBuffer object.");

            _framebufferIdBloom1 = framebufferTempId;
            _framebufferTextureBloom1 = renderedTextureTemp;

            // =========== TEMP 2 ===========
            int framebufferId = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferId);

            int renderedTextureTemp2 = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, renderedTextureTemp2);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (float) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (float) TextureParameterName.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (float) TextureParameterName.ClampToEdge);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                renderedTextureTemp2, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            code = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (code != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("GL_FRAMEBUFFER_COMPLETE failed. Cannot use FrameBuffer object.");

            _framebufferIdBloom2 = framebufferId;
            _framebufferTextureBloom2 = renderedTextureTemp2;
        }

        private void DownsampleFramebufferDEBUG()
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, _framebufferIdMain);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);

            GL.ReadBuffer(ReadBufferMode.ColorAttachment1);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GL.BlitFramebuffer(0, 0, Width, Height, 0, 0, Width, Height, ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Linear);
        }

        private void DownsampleFramebuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, _framebufferIdMain);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, _framebufferIdMainDownsampled);

            GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GL.BlitFramebuffer(0, 0, Width, Height, 0, 0, Width, Height, ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Linear);

            GL.ReadBuffer(ReadBufferMode.ColorAttachment1);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment1);
            GL.BlitFramebuffer(0, 0, Width, Height, 0, 0, Width, Height, ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Linear);
        }

        #endregion
    }
}