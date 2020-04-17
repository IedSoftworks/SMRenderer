using System.IO.Ports;
using System.Windows;
using OpenTK;
using SMRenderer.Data;
using SMRenderer.Visual;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Animate a texture. Should be used as TextureHandler.
    /// </summary>
    public class TextureAnimation : TextureHandler
    {
        /// <summary>
        /// The current time. Resets any frame.
        /// </summary>
        private double _currentTime;
        /// <summary>
        /// Determinant if the animation is active.
        /// </summary>
        private bool _active;

        /// <summary>
        /// Returns the current frame.
        /// </summary>
        public int CurrentFrame { get; private set; } = 0;
        /// <summary>
        /// Sets the maximum of frames.
        /// <para>If not set or too much set, it continues up to the end of the texture and stops there. </para>
        /// </summary>
        public int MaximalFrames = 0;
        /// <summary>
        /// The width of the source texture
        /// </summary>
        public int Width;
        /// <summary>
        /// The height of the source texture
        /// </summary>
        public int Height;
        /// <summary>
        /// Sets how fast the animation should be. 
        /// </summary>
        public int FramesPerSecond;

        /// <summary>
        /// Repeats the animation
        /// </summary>
        public bool Repeat = true;

        /// <inheritdoc />
        public TextureAnimation()
        {
        }

        /// <inheritdoc />
        public TextureAnimation(TextureItem item, Vector2 size, int framesPerSecond, int maximalFrames) : base(item)
        {
            TexSize = size;

            Width = (int)item.Size.X;
            Height = (int)item.Size.Y;

            FramesPerSecond = framesPerSecond;
            MaximalFrames = maximalFrames;
        }
        /// <summary>
        /// Starts the animation
        /// </summary>
        public void Start()
        {
            _currentTime = 0;
            _active = true;

            TexPosition = Vector2.Zero;
        }
        /// <summary>
        /// Updates the animation
        /// </summary>
        /// <param name="deltatime">the deltatime</param>
        public void Update(double deltatime)
        {
            if (!_active) return;

            _currentTime += deltatime;
            if (_currentTime > 1 / FramesPerSecond)
            {
                TexPosition.X += TexSize.X;
                if (TexPosition.X >= Width)
                {
                    TexPosition.X = 0;
                    TexPosition.Y += TexSize.Y;
                }
                if (TexPosition.Y >= Height && TexPosition.X >= Width)
                {
                    Stop();
                    return;
                }

                if (CurrentFrame > MaximalFrames)
                {
                    if (Repeat)
                    {
                        CurrentFrame = 0;
                        TexPosition = Vector2.Zero;
                    }
                    else Stop();

                    return;
                }
                CurrentFrame++;

                _currentTime = 0;
            }
        }
        /// <summary>
        /// Stops the Animation.
        /// </summary>
        public void Stop()
        {
            TexPosition = Vector2.Zero;
            _active = false;

        }
    }
}