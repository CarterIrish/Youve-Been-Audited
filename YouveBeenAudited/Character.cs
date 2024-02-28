using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: Holds character specific information.
    /// </summary>
    internal class Character : GameObject
    {
        // ------ Fields ------

        protected int _health;

        // ------ Properties ------

        /// <summary>
        /// Gets the health of a player.
        /// </summary>
        public int Health { get => _health; }


        // ------ Methods ------

        /// <summary>
        /// Create a new character object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        public Character(int x, int y, Texture2D texture, int health) : base(x, y, texture)
        {
            _health = health;
        }

        // TODO: Make any methods necessary for characters as a whole.

    }
}
