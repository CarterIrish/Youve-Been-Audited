namespace YouveBeenAudited
{
    /// <summary>
    /// Enforces that any damageable object has a method to take damage
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