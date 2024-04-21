using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    public enum TrapType
    {
        Spikes,
        Glue,
        Bomb
    }

    /// <summary>
    /// Purpose: To hold generic information that all traps have.
    /// </summary>
    internal class Trap : GameObject
    {
        #region Fields

        protected TrapType _type;

        protected int _damageAmnt;

        protected int _cost;
        protected bool _isActive;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the damage amount of trap.
        /// </summary>
        public int DamageAmnt { get => _damageAmnt; }

        /// <summary>
        /// Gets the cost of trap.
        /// </summary>
        public int Cost { get => _cost; }

        /// <summary>
        /// Gets or sets the active state of trap.
        /// </summary>
        public bool IsActive { get => _isActive; set => _isActive = value; }

        /// <summary>
        /// Gets the type of trap.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TrapType Type { get => _type; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Trap"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture of trap.</param>
        /// <param name="cost">The cost of trap.</param>
        /// <param name="damageAmnt">The damage amnt of trap.</param>
        public Trap(int x, int y, Texture2D texture, int cost, int damageAmnt) : base(x, y, texture)
        {
            _damageAmnt = damageAmnt;
            _cost = cost;
        }

        /// <summary>
        /// Checks the collision with another game object
        /// </summary>
        /// <param name="obj">Object to check collisions with</param>
        /// <returns>True if collision detected</returns>
        public bool CheckCollisions(GameObject obj)
        {
            if (obj is IDamageable && Position.Intersects(new Rectangle(obj.Position.X, obj.Position.Y, 55, 100)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Does the effect of a trap on another object.
        /// </summary>
        /// <param name="e">The object to perform effect on.</param>
        public virtual void DoEffect(Enemy e)
        { }

        #endregion Methods
    }
}