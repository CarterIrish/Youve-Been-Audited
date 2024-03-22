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
        // ------ Fields. ------
        private int _damageAmnt;
        private int _cost;
        private bool _isActive;


        // ------ Properties. ------

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

        // ------ Methods. ------


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
        /// <returns></returns>
        public bool CheckCollisions(GameObject obj)
        {
            if(obj is Enemy)
            {
                return Position.Intersects(obj.Position);
            }
            return false;
        }
    }
}
