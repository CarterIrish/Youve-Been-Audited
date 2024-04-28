using Microsoft.Xna.Framework;

namespace YouveBeenAudited
{
    /// <summary>
    /// Enforces that any damageable object has a method to take damage
    /// </summary>
    internal interface IDamageable
    {
        /// <summary>
        /// Gets the sprite size of IDamageable objects
        /// </summary>
        public Point SpriteSize { get; }

        /// <summary>
        /// Gets the position of IDamageable objects
        /// </summary>
        public Rectangle Position { get; }

        /// <summary>
        /// Method for making the object take damage based off given amount.
        /// </summary>
        /// <param name="amount">Amount of damage to take.</param>
        void TakeDamage(int amount);
    }
}