using System;
using System.Drawing;
using System.Windows.Markup;
using OpenTK;
using SMRenderer.ManagerIntergration.Attributes;
using SMRenderer.Visual;

namespace SMRenderer.Data
{
    /// <summary>
    ///     A storage item, to prevent repeatedly loading the same texture
    /// </summary>
    [Serializable]
    public class TextureItem : Data
    {
        public static TextureItem empty;

        public static void CreateEmpty()
        {
            Bitmap bit = new Bitmap(1, 1);
            bit.SetPixel(0, 0, Color.White);
            empty = new TextureItem { bitmap = bit };
            empty.Load();
        }

        /// <summary>
        ///     The source data.
        /// </summary>
        [EditorField] public Bitmap bitmap;

        /// <summary>
        ///     Parameterless constructor for automatic instance creation
        /// </summary>
        public TextureItem()
        {
        }

        /// <summary>
        ///     Creates a TextureItem
        /// </summary>
        /// <param name="name">Reference name</param>
        /// <param name="bitmap">Image data</param>
        public TextureItem(string name, Bitmap bitmap)
        {
            refName = name;
            this.bitmap = bitmap;
            Category = "Textures";
            DataManager.C.Add(this);
        }
        /// <summary>
        ///     Creates a TextureItem with specific category
        /// </summary>
        /// <param name="name">Reference Name</param>
        /// <param name="category">Category name</param>
        /// <param name="bitmap">Image data</param>
        public TextureItem(string name, string category, Bitmap bitmap)
        {

            refName = name;
            this.bitmap = bitmap;
            Category = category;
            DataManager.C.Add(this);
        }

        /// <summary>
        /// The loaded texture
        /// </summary>
        [field: NonSerialized] public Texture texture { get; private set; }

        /// <summary>
        /// Returns the size of the texture
        /// </summary>
        public Vector2 Size => new Vector2(bitmap.Width, bitmap.Height);

        /// <inheritdoc />
        protected override bool IsLoaded => texture != null;

        /// <inheritdoc />
        public override void Load()
        {
            texture = new Texture(bitmap);
        }
    }
}