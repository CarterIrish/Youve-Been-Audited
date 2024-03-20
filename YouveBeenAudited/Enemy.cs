using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To contain information specific to the enemy object.
    /// </summary>
    internal class Enemy : Character, IDamageable
    {
        // ------ Fields ------

        private bool _isDead;
        private bool _atGoal;
        private int _currentPoint;
        private List<Point> _path;
        
        // ------ Properties -------

        /// <summary>
        /// Gets whether or not enemy is dead.
        /// </summary>
        public bool IsDead { get => _isDead; }

        /// <summary>
        /// Gets whether or not enemy is at goal.
        /// </summary>
        public bool AtGoal { get => _atGoal; }

        /// <summary>
        /// Gets current point in path.
        /// </summary>
        public int CurrentPoint { get => _currentPoint; }

        /// <summary>
        /// Gets path list or sets new path and resets progress.
        /// </summary>
        public List<Point> Path
        {
            get => _path;
            set 
            { 
                _path = value;
                _currentPoint = 0;
            }
        }
        
        // ------ Methods ------

        /// <summary>
        /// Creates a new enemy object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="health"></param>
        public Enemy(int x, int y, Texture2D texture, int health) : base(x, y, texture, health)
        {
            // TODO: Create logic for object creation. also, review method parameters.
        }

        /// <summary>
        /// Makes the enemy object take damage.
        /// </summary>
        /// <param name="amount">amount of damage to take.</param>
        public void TakeDamage(int amount)
        {
            _health = _health - amount;
            if (_health <= 0)
            {
                _isDead = true;
            }
        }

        /// <summary>
        /// Moves the enemy to next point in path. If the end is reached, do nothing.
        /// </summary>
        public void Move()
        {
            if (_path.Count - 1 == _currentPoint) //Checks if enemy is at the end of the path.
            {
                //Trigger end of game
                return;
            }
            _currentPoint++;
        }

    }
}
