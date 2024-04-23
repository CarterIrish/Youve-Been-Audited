using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: To contain information specific to the enemy object.
    /// </summary>
    internal class Enemy : Character, IDamageable
    {
        #region Fields

        private int _currentFrame;
        private bool _atGoal;
        private int _currentPoint;
        private List<Vector2> _path;
        private Rectangle _destinationRectangle;
        private Point _spriteSize;
        private double _timeCount;
        private bool _isSlowed;
        private List<Trap> _steppedOn;
        private int _tileHeight;

        #endregion Fields

        #region Properties

        public List<Trap> SteppedOn
        {
            get { return _steppedOn; }
        }

        /// <summary>
        /// Gets the current frame of enemy animation.
        /// </summary>
        /// <value>
        /// The current frame.
        /// </value>
        public int CurrentFrame { get => _currentFrame; }

        /// <summary>
        /// Gets whether or not enemy is at goal.
        /// </summary>
        public bool AtGoal
        {
            get
            {
                return _atGoal;
            }
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

        public bool IsSlowed
        {
            get { return _isSlowed; }
            set { _isSlowed = value; }
        }

        /// <summary>
        /// Gets current point in path.
        /// </summary>
        public int CurrentPoint { get => _currentPoint; }

        /// <summary>
        /// Gets path list or sets new path and resets progress.
        /// </summary>
        public List<Vector2> Path
        {
            get => _path;
            set
            {
                _path = value;
                _currentPoint = 1;
            }
        }

        /// <summary>
        /// Gets the time count of animation.
        /// </summary>
        /// <value>
        /// The time count.
        /// </value>
        public double TimeCount { get => _timeCount; }

        /// <summary>
        /// Gets the size of the sprite.
        /// </summary>
        /// <value>
        /// The size of the sprite.
        /// </value>
        public Point SpriteSize { get => _spriteSize; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="health">The health of enemy.</param>
        /// <param name="speed">The speed of enemy. </param>
        /// <param name="texture">The texture of enemy.</param>
        /// <param name="path">The path of enemy.</param>
        public Enemy(int x, int y, int health, int speed, int tileHeight, Texture2D texture, List<Vector2> path) : base(x, y, texture, health, speed)
        {
            _path = path;
            base._position.X = x;
            base._position.Y = y;
            base._health = health;
            base._texture = texture;
            _atGoal = false;
            _currentPoint = 1;
            _currentFrame = 0;
            _currentState = CharacterStates.Right;
            _spriteSize = new Point(55, 100);
            int scalar = (_spriteSize.X * (tileHeight)) / _spriteSize.Y; ;
            _destinationRectangle = new Rectangle(_position.X, _position.Y, scalar, tileHeight);
            _position = _destinationRectangle;
            _isSlowed = false;
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

        /// <summary>
        /// Moves the enemy to next point in path. If the end is reached, do nothing.
        /// </summary>
        public override void Update(GameTime gt)
        {
            _timeCount += gt.ElapsedGameTime.TotalSeconds;
            _timeCount = UpdateAnimation(_timeCount);
            if (_currentPoint < _path.Count)
            {
                Vector2 direction = _path[_currentPoint] - new Vector2(_position.X, _position.Y);

                direction = Vector2.Normalize(direction);


                if(direction.X < 0)
                {
                    _position.X += (int)((direction.X - .5) * _speed);
                }
                else
                {
                    _position.X += (int)((direction.X + .5) * _speed);
                }
                if(direction.Y < 0)
                {
                    _position.Y += (int)((direction.Y - .5) * _speed);
                }
                else
                {
                    _position.Y += (int)((direction.Y + .5) * _speed);
                }
                _destinationRectangle.X = _position.X;
                _destinationRectangle.Y = _position.Y;


                if ((_position.X < _path[_currentPoint].X + _speed && _position.X > _path[_currentPoint].X - _speed) &&
                _position.Y < _path[_currentPoint].Y + _speed && _position.Y > _path[_currentPoint].Y - _speed)
                {
                    _currentPoint++;
                }

                //Animation Movement Update
                if (direction.X > 0)
                {
                    _currentState = CharacterStates.Right;
                }
                else if (direction.X < 0)
                {
                    _currentState = CharacterStates.Left;
                }

                if (_currentPoint == _path.Count)
                {
                    _atGoal = true;
                }
            }
        }

        /// <summary>
        /// Updates the enemy animation.
        /// </summary>
        /// <param name="gameTime">Game time information</param>
        public double UpdateAnimation(double _timeCount)
        {
            if (_timeCount >= 6.5f / 60.0)
            {
                // Update the frame and wrap
                _currentFrame++;
                if (_currentFrame >= 4) _currentFrame = 1;

                // Remove one "frame" worth of time
                _timeCount -= (6.5f / 60.0);
            }
            return _timeCount;
        }

        /// <summary>
        /// Draws enemy according to current state.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        public override void Draw(SpriteBatch sb)
        {
            switch (_currentState)
            {
                case CharacterStates.Left:
                    sb.Draw(
                Texture,
                _destinationRectangle,
                new Rectangle(((_spriteSize.X + 25) * _currentFrame) + 560, 0, _spriteSize.X, _spriteSize.Y),
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.FlipHorizontally,
                0.0f);
                    break;

                case CharacterStates.Right:
                    sb.Draw(
                Texture,
                _destinationRectangle,
                new Rectangle(((_spriteSize.X + 25) * _currentFrame) + 560, 0, _spriteSize.X, _spriteSize.Y),
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.0f);
                    break;

                default:
                    sb.Draw(Texture, _destinationRectangle, new Rectangle(400, 0, _spriteSize.X, _spriteSize.Y), Color.White);
                    break;
            }
        }

        #endregion Methods
    }
}