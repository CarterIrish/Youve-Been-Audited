using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains high level information for all game objects.
    /// </summary>
    internal class GameObject
    {
        #region Fields

        protected Rectangle _position;
        protected Texture2D _texture;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Rectangle Position { get => _position; }

        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
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
        /// <param name="rect">The rect.</param>
        /// <param name="texture">The texture.</param>
        public GameObject(Rectangle rect, Texture2D texture)
        {
            _texture = texture;
            _position = rect;
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        public virtual void Update(GameTime gametime)
        { }

        /// <summary>
        /// Draws this instance of an object.
        /// </summary>
        /// <param name="sb">The sb.</param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, _position, Color.White);
        }

        /// <summary>
        /// Draws the this instance of an object.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="tint">The tint.</param>
        public virtual void Draw(SpriteBatch sb, Color tint)
        {
            sb.Draw(_texture, _position, tint);
        }

        #endregion Methods
    }
}