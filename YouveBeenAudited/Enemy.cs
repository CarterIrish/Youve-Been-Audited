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
        #region Fields

        private bool _atGoal;
        private int _currentPoint;
        private List<Point> _path;

        public event EnemyAtGoal EnemyAtGoal;

        #endregion Fields

        #region Properties

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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new enemy object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="health"></param>
        public Enemy(int x, int y, Texture2D texture, int health, List<Point> path) : base(x, y, texture, health)
        {
            Path = path;
            base._position.X = x;
            base._position.Y = y;
            base._health = health;
            base._texture = texture;
            _atGoal = false;
        }

        /// <summary>
        /// Makes the enemy object take damage.
        /// </summary>
        /// <param name="amount">amount of damage to take.</param>
        public void TakeDamage(int amount)
        {
            this.Health -= amount;
        }

        /// <summary>
        /// Moves the enemy to next point in path. If the end is reached, do nothing.
        /// </summary>
        public override void Update(GameTime gt)
        {
            if (_path.Count - 1 == _currentPoint) //Checks if enemy is at the end of the path.
            {
                _atGoal = true;
            }
            _currentPoint++;
        }

        /// <summary>
        /// Draws an enemy.
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(_position.X, _position.Y, 55, 100), new Rectangle(400, 0, 55, 100), Color.White);
        }


        #endregion Methods
    }
}