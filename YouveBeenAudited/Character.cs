using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{

    /// <summary>States of a character</summary>
    enum CharacterStates
    {
        Idle,
        Left,
        Right,
        Up,
        Down
    }


    /// <summary>
    /// Purpose: Holds character specific information.
    /// </summary>
    internal class Character : GameObject
    {
        // ------ Fields ------

        private int _health;
        private bool _isDead;

        /// <summary>
        /// Gets whether or not the character is dead.
        /// </summary>
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        // ------ Properties ------

        /// <summary>
        /// Gets or sets the health of a player. If health is below or at 0, set to dead.
        /// </summary>
        public int Health
        {
            get => _health;
            protected set
            {
                // do assignment and if less than zero, lock at 0hp and set to dead.
                if((_health = value) <= 0)
                {
                    _isDead = true;
                    _health = 0;
                }

            }
        }

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
