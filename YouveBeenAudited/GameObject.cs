﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To contain generic information about 
    /// every game object.
    /// </summary>
    internal class GameObject
    {
        // ------ Fields ------

        private Rectangle _position;
        private Texture2D _texture;

        // ------ Properties ------


        // ------ Methods ------

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
        /// Updates the objects information.
        /// </summary>
        /// <param name="gametime"></param>
        public virtual void Update(GameTime gametime) { }

        /// <summary>
        /// Draws the object to screen.
        /// </summary>
        /// <param name="sb">Spritebatch object for drawing.</param>
        public virtual void Draw(SpriteBatch sb)
        {
            // TODO: If needed change this so that it more aligns with our game
            sb.Draw(_texture, _position, Color.White);
        }
    }
}