using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains all information for an enemy.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.Character" />
    /// <seealso cref="YouveBeenAudited.IDamageable" />
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

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        /// <value>
        /// The current frame.
        /// </value>
        public int CurrentFrame { get => _currentFrame; }

        /// <summary>
        /// Gets a value indicating whether [at goal].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [at goal]; otherwise, <c>false</c>.
        /// </value>
        public bool AtGoal { get => _atGoal; }

        /// <summary>
        /// Gets the current point.
        /// </summary>
        /// <value>
        /// The current point.
        /// </value>
        public int CurrentPoint { get => _currentPoint; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
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
        /// <param name="health">The health.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="tileHeight">Height of the tile.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="path">The path.</param>
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
        }

        /// <summary>
        /// Updates the objects information.
        /// </summary>
        /// <param name="gt">GameTime object</param>
        public override void Update(GameTime gt)
        {
            base.Update(gt);
            _timeCount += gt.ElapsedGameTime.TotalSeconds;
            _timeCount = UpdateAnimation(_timeCount);

            if (_currentPoint < _path.Count)
            {
                Vector2 alteredPoint = new Vector2(_path[_currentPoint].X - _position.Width / 2, _path[_currentPoint].Y - _position.Height / 2);
                Vector2 direction = alteredPoint - new Vector2(_position.X, _position.Y);
                direction = Vector2.Normalize(direction);

                if (direction.X < 0)
                {
                    _position.X += (int)((direction.X - .5) * _speed);
                }
                else
                {
                    _position.X += (int)((direction.X + .5) * _speed);
                }
                if (direction.Y < 0)
                {
                    _position.Y += (int)((direction.Y - .5) * _speed);
                }
                else
                {
                    _position.Y += (int)((direction.Y + .5) * _speed);
                }
                _destinationRectangle.X = _position.X;
                _destinationRectangle.Y = _position.Y;

                if ((_position.X < _path[_currentPoint].X - (_position.Width / 2) + _speed && _position.X > _path[_currentPoint].X - (_position.Width / 2) - _speed) &&
                _position.Y < _path[_currentPoint].Y - (_position.Height / 2) + _speed && _position.Y > _path[_currentPoint].Y - (_position.Height / 2) - _speed)
                {
                    _currentPoint++;
                }

                if (_currentPoint == _path.Count)
                {
                    _atGoal = true;
                    return;
                }

                //Animation Movement Update
                if (_path[_currentPoint].X > _position.X)
                {
                    _currentState = CharacterStates.Right;
                }
                else
                {
                    _currentState = CharacterStates.Left;
                }
            }
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="_timeCount">The time count.</param>
        /// <returns>Return the time count of animation</returns>
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
        /// Draws this instance of an object.
        /// </summary>
        /// <param name="sb">The SpriteBatch.</param>
        public override void Draw(SpriteBatch sb)
        {
            switch (_currentState)
            {
                case CharacterStates.Left:
                    sb.Draw(
                Texture,
                _destinationRectangle,
                new Rectangle(((_spriteSize.X + 25) * _currentFrame) + 560, 0, _spriteSize.X, _spriteSize.Y),
                _currentTint,
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
                _currentTint,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.0f);
                    break;

                default:
                    sb.Draw(Texture, _destinationRectangle, new Rectangle(400, 0, _spriteSize.X, _spriteSize.Y), _currentTint);
                    break;
            }
        }

        #endregion Methods
    }
}