﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To contain generic information about
    /// every game object.
    /// </summary>
    internal class GameObject
    {
        #region Fields

        protected Rectangle _position;
        protected Texture2D _texture;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the rectangle position of an object.
        /// </summary>
        public Rectangle Position { get => _position; }

        /// <summary>
        /// Gets the texture for a game object.
        /// </summary>
        public Texture2D Texture { get => _texture; }

        #endregion Propertiesd
         
        #region Methods

        /// <summary>
        /// Create a new GameObject object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        public GameObject(int x, int y, Texture2D texture)
        {
            _texture = texture;
            _position = new Rectangle(x, y, texture.Width, texture.Height);
        }

        /// <summary>
        /// Creates a new GameObject object
        /// </summary>
        /// <param name="rect">position/dimensions of the gameObject</param>
        /// <param name="texture">texture of the GameObject</param>
        public GameObject(Rectangle rect, Texture2D texture)
        {
            _texture = texture;
            _position = rect;
        }

        /// <summary>
        /// Updates the objects information.
        /// </summary>
        /// <param name="gametime"></param>
        public virtual void Update(GameTime gametime)
        { }

        /// <summary>Draws this instance of an object.</summary>
        /// <param name="sb">The SpriteBatch.</param>
        public virtual void Draw(SpriteBatch sb)
        {
            // TODO: If needed change this so that it more aligns with our game
            sb.Draw(_texture, _position, Color.White);
        }

        /// <summary>Draws this instance of an object.</summary>
        /// <param name="sb">The SpriteBatch.</param>
        /// <param name="tint">The tint of texture to be drawn.</param>
        public virtual void Draw(SpriteBatch sb, Color tint)
        {
            sb.Draw(_texture, _position, tint);
        }

        #endregion Methods
    }
}