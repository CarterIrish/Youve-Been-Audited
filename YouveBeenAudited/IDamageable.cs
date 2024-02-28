using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To ensure that any damageable object in the game
    /// has required methods to take damage.
    /// </summary>
    internal interface IDamageable
    {
        /// <summary>
        /// Method for making the object take damage based off given amount.
        /// </summary>
        /// <param name="amount">Amount of damage to take.</param>
        void TakeDamage(int amount);
    }
}
