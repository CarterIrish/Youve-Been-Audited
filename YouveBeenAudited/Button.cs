using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To represent a UI/X button on the screen.
    /// </summary>
    internal class Button : GameObject
    {
        // TODO: Create or insert button class.

        private string _buttonName;
        private bool _isActive;

        public string Name { get => _buttonName; }
        public bool IsActive { get => _isActive; set => _isActive = value; }

        /// <summary>
        /// Creates a new button object.
        /// </summary>
        /// <param name="x">X coord of button.</param>
        /// <param name="y">Y coord of button.</param>
        /// <param name="texture">The buttons texture.</param>
        public Button(int x, int y, Texture2D texture, string buttonName) : base(x, y, texture)
        {
            // TODO: Create button constructor.
            _buttonName = buttonName;
        }

        /// <summary>
        /// Checks if the mouse cursor is within the bounds of the button
        /// </summary>
        /// <param name="mouse">Mouse to keep track of</param>
        /// <returns>True if mouse is within the bounds of the button</returns>
        public bool ButtonHover(MouseState mouse)
        {
            if (mouse.Position.X < Position.X + _position.Width &&
               mouse.Position.X > Position.X &&
               mouse.Position.Y < Position.Y + _position.Height &&
               mouse.Position.Y > Position.Y
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
        public bool ButtonClick(MouseState mouse)
        {
            if (ButtonHover(mouse))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
