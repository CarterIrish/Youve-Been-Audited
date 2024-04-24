using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>States of a character</summary>
    internal enum CharacterStates
    {
        Idle,
        Left,
        Right,
        FaceLeft,
        FaceRight
    }

    /// <summary>
    /// Purpose: Holds character specific information.
    /// </summary>
    internal class Character : GameObject, IDamageable
    {
        #region Fields

        protected int _health;
        protected int _speed;
        protected bool _isDead;
        protected CharacterStates _currentState;
        protected List<Trap> _steppedOn;
        protected bool _isSlowed;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Determines of a character is slowed by glue
        /// </summary>
        public bool IsSlowed
        {
            get { return _isSlowed; }
            set { _isSlowed = value; }
        }

        /// <summary>
        /// Determines if the enemy is currently standing on glue
        /// </summary>
        public bool OnGlue
        {
            get
            {
                foreach (Trap t in SteppedOn)
                {
                    if (t is Glue)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

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

        public List<Trap> SteppedOn
        {
            set { _steppedOn = value; }
            get { return _steppedOn; }
        }


        /// <summary>
        /// Gets or sets the health of a player. If health is below or at 0, set to dead.
        /// </summary>
        public int Health
        {
            get => _health;
            protected set
            {
                // do assignment and if less than zero, lock at 0hp and set to dead.
                if ((_health = value) <= 0)
                {
                    _isDead = true;
                    _health = 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the speed of Character.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public int Speed
        {
            set => _speed = value;
            get => _speed;
        }

        /// <summary>
        /// Gets the current state of character.
        /// </summary>
        /// <value>
        /// The state of the player.
        /// </value>
        public CharacterStates CurrentState { get => _currentState; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Create a new character object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        public Character(int x, int y, Texture2D texture, int health, int speed) : base(x, y, texture)
        {
            _health = health;
            _speed = speed;
            _currentState = CharacterStates.Idle;
            _steppedOn = new List<Trap>();
        }

        /// <summary>
        /// Makes the enemy object take damage.
        /// </summary>
        /// <param name="amount">amount of damage to take.</param>
        public void TakeDamage(int amount)
        {
            this.Health -= amount;
        }

        #endregion Methods
    }
}