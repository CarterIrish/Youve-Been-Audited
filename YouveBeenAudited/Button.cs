using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains all information for a button.
    /// </summary>
    internal class Button
    {
        #region Fields

        // Button clicked event
        public event BtnClickedDelegate BtnClicked;

        private string _buttonName;

        private bool _isActive;

        private Color _color;

        private Rectangle _boundingBox;
        private Texture2D _texture;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the name of Button.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get => _buttonName; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get => _isActive; set => _isActive = value; }

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color { get => _color; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Rectangle Position { get => _boundingBox; }

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
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="buttonName">Name of the button.</param>
        /// <param name="color">The color.</param>
        /// <param name="scalar">The scalar.</param>
        public Button(int x, int y, Texture2D texture, string buttonName, Color color, double scalar)
        {
            // TODO: Create button constructor.
            _buttonName = buttonName;
            _color = color;
            _texture = texture;
            _boundingBox = new Rectangle(x, y, (int)(texture.Width * scalar), (int)(texture.Height * scalar));
        }

        /// <summary>
        /// Checks if the mouse cursor is within the bounds of the button
        /// </summary>
        /// <param name="mouse">Mouse to keep track of</param>
        /// <returns>True if mouse is within the bounds of the button</returns>
        public bool ButtonHover(MouseState mouse)
        {
            if (mouse.Position.X < _boundingBox.X + _boundingBox.Width &&
               mouse.Position.X > _boundingBox.X &&
               mouse.Position.Y < _boundingBox.Y + _boundingBox.Height &&
               mouse.Position.Y > _boundingBox.Y
               )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the mouse is hovering over, and clicks the button
        /// </summary>
        /// <param name="mouse">Mouse to keep track of</param>
        /// <returns>True if mouse clicks while hovering over button</returns>
        public void CheckClick(MouseState mouse)
        {
            if (ButtonHover(mouse))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    BtnClicked(this);
                }
            }
        }

        /// <summary>Draws this instance of an object.</summary>
        /// <param name="sb">The SpriteBatch.</param>
        public virtual void Draw(SpriteBatch sb, Color tint)
        {
            // TODO: If needed change this so that it more aligns with our game
            sb.Draw(_texture, _boundingBox, tint);
        }

        #endregion Methods
    }
}