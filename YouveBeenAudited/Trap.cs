using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To hold generic information that all traps have.
    /// </summary>
    internal class Trap : GameObject
    {
        #region Fields

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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new trap object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="cost"></param>
        /// <param name="damageAmnt"></param>
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
            if (obj is IDamageable && Position.Intersects(new Rectangle(obj.Position.X,obj.Position.Y, 55, 100)))
            {
                return true;
            }
            return false;
        }

        #endregion Methods
    }
}