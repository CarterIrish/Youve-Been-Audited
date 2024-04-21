using Microsoft.Xna.Framework;
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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture.</param>
        public GameObject(int x, int y, Texture2D texture)
        {
            _texture = texture;
            _position = new Rectangle(x, y, texture.Width, texture.Height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="rect">The object Bounding Box.</param>
        /// <param name="texture">The texture.</param>
        public GameObject(Rectangle rect, Texture2D texture)
        {
            _texture = texture;
            _position = rect;
        }

        /// <summary>
        /// Updates the objects information.
        /// </summary>
        /// <param name="gametime">GameTime object</param>
        public virtual void Update(GameTime gametime)
        { }

        /// <summary>Draws this instance of an object.</summary>
        /// <param name="sb">The SpriteBatch.</param>
        public virtual void Draw(SpriteBatch sb)
        {
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