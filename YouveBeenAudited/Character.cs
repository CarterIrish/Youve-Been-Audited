using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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
    /// Contains core information for all characters.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.GameObject" />
    /// <seealso cref="YouveBeenAudited.IDamageable" />
    internal class Character : GameObject, IDamageable
    {
        #region Fields

        protected int _health;
        protected int _speed;
        protected bool _isDead;
        protected CharacterStates _currentState;
        protected List<Trap> _steppedOn;
        protected bool _isSlowed;
        protected double _damageTime;
        protected Color _currentTint;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is slowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is slowed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSlowed { get => _isSlowed; set => _isSlowed = value; }

        /// <summary>
        /// Gets a value indicating whether [on glue].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [on glue]; otherwise, <c>false</c>.
        /// </value>
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
        /// Gets a value indicating whether this instance is dead.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dead; otherwise, <c>false</c>.
        /// </value>
        public bool IsDead { get => _isDead; }

        /// <summary>
        /// Gets or sets the stepped on.
        /// </summary>
        /// <value>
        /// The stepped on.
        /// </value>
        public List<Trap> SteppedOn { get => _steppedOn; set => _steppedOn = value; }

        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
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
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public int Speed { get => _speed; set => _speed = value; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public CharacterStates CurrentState { get => _currentState; }

        /// <summary>
        /// Gets/sets the time left the object should spend being drawn red to signify taking damage
        /// </summary>
        public double DamageTime
        {
            get { return _damageTime; }
            set { _damageTime = value; }
        }

        public Point SpriteSize => throw new System.NotImplementedException();


        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="health">The health.</param>
        /// <param name="speed">The speed.</param>
        public Character(int x, int y, Texture2D texture, int health, int speed) : base(x, y, texture)
        {
            _health = health;
            _speed = speed;
            _currentState = CharacterStates.Idle;
            _steppedOn = new List<Trap>();
            _currentTint = Color.White;
            _damageTime = .1;
        }

        /// <summary>
        /// Method for making the object take damage based off given amount.
        /// </summary>
        /// <param name="amount">Amount of damage to take.</param>
        public void TakeDamage(int amount)
        {
            this.Health -= amount;

            if (amount > 0)
            {
                _currentTint = Color.Red;
            }
        }

        /// <summary>
        /// Updates the character
        /// </summary>
        /// <param name="gametime"></param>
        public override void Update(GameTime gametime)
        {
            if (_currentTint == Color.Red)
            {
                _damageTime -= gametime.ElapsedGameTime.TotalSeconds;
                if(_damageTime <= 0)
                {
                    _currentTint = Color.White;
                    _damageTime = .1;
                }
            }
        }

        #endregion Methods
    }
}