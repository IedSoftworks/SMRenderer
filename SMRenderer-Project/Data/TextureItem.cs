﻿using System;
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
        [EditorField] public Bitmap bitmap;

        public TextureItem()
        {
        }

        /// <summary>
        ///     Creates a TextureItem
        /// </summary>
        /// <param name="bitmap"></param>
        public TextureItem(string name, Bitmap bitmap)
        {
            refName = name;
            this.bitmap = bitmap;
            Category = "Textures";
            DataManager.C.Add(this);
        }

        public TextureItem(string name, string category, Bitmap bitmap)
        {

            refName = name;
            this.bitmap = bitmap;
            Category = category;
            DataManager.C.Add(this);
        }

        [field: NonSerialized] public Texture texture { get; private set; }

        public Vector2 Size => new Vector2(texture.Width, texture.Height);
        protected override bool IsLoaded => texture != null;

        public override void Load()
        {
            texture = new Texture(bitmap);
        }
    }
}