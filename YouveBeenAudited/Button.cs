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

        /// <summary>
        /// Creates a new button object.
        /// </summary>
        /// <param name="x">X coord of button.</param>
        /// <param name="y">Y coord of button.</param>
        /// <param name="texture">The buttons texture.</param>
        public Button(int x, int y, Texture2D texture) : base(x, y, texture)
        {
            // TODO: Create button constructor.
        }
    }
}
