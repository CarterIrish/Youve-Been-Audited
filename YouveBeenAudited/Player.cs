using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains all information specific to the Player character.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.Character" />
    /// <seealso cref="YouveBeenAudited.IDamageable" />
    internal class Player : Character, IDamageable
    {
        #region Fields

        private int _money;

        private int _currentFrame;

        private Point _spriteSize;

        private SpriteFont _font;

        private Rectangle _destinationRectangle;

        private KeyboardState _prevKB;

        private CharacterStates _prevState;

        private bool _steppedOffSpikes;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [stepped off spikes].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [stepped off spikes]; otherwise, <c>false</c>.
        /// </value>
        public bool SteppedOffSpikes { get => _steppedOffSpikes; set => _steppedOffSpikes = value; }

        /// <summary>
        /// Gets or sets the money.
        /// </summary>
        /// <value>
        /// The money.
        /// </value>
        public int Money
        { get => _money; set { _money = value; } }

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
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="health">The health.</param>
        /// <param name="startingMoney">The starting money.</param>
        /// <param name="tileHeight">Height of the tile.</param>
        /// <param name="speed">The speed.</param>
        public Player(int x, int y, Texture2D texture, int health, int startingMoney, int tileHeight, int speed) : base(x, y, texture, health, speed)
        {
            _money = startingMoney;
            _spriteSize = new Point(55, 125);
            int scalar = (_spriteSize.X * (tileHeight)) / _spriteSize.Y;
            _destinationRectangle = new Rectangle(_position.X, _position.Y, scalar, tileHeight);
            _position = _destinationRectangle;
            _currentFrame = 0;
        }
        
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content manager.</param>
        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("Arial25");
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        public override void Update(GameTime gametime)
        {
            Move();
            base.Update(gametime);
            if (_currentState != CharacterStates.Idle)
            {
                _prevState = _currentState;
            }
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public void Move()
        {
            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.W))
            {
                if (_currentState == CharacterStates.Idle)
                {
                    _currentState = _prevState;
                    if (_currentState == CharacterStates.Idle)
                    {
                        _currentState = CharacterStates.Right;
                    }
                }
                _position.Y -= _speed;
                _destinationRectangle.Y = _position.Y;
            }
            if (kbs.IsKeyDown(Keys.A))
            {
                _position.X -= _speed;
                _currentState = CharacterStates.Left;
                _destinationRectangle.X = _position.X;
            }
            if (kbs.IsKeyDown(Keys.S))
            {
                if (_currentState == CharacterStates.Idle)
                {
                    _currentState = _prevState;
                    if (_currentState == CharacterStates.Idle)
                    {
                        _currentState = CharacterStates.Right;
                    }
                }
                _position.Y += _speed;
                _destinationRectangle.Y = _position.Y;
            }
            if (kbs.IsKeyDown(Keys.D))
            {
                _position.X += _speed;
                _currentState = CharacterStates.Right;
                _destinationRectangle.X = _position.X;
            }
            if (kbs.IsKeyUp(Keys.W) && kbs.IsKeyUp(Keys.A) && kbs.IsKeyUp(Keys.S) && kbs.IsKeyUp(Keys.D))
            {
                _currentState = CharacterStates.Idle;
            }
        }

        /// <summary>
        /// Draws this instance of an object.
        /// </summary>
        /// <param name="sb">The sb.</param>
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

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="_timeCount">The time count.</param>
        /// <returns>Updates time count</returns>
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
        /// Resolves the collisions.
        /// </summary>
        /// <param name="walls">The walls.</param>
        public void ResolveCollisions(List<GameObject> walls)
        {
            List<Rectangle> intersections = new List<Rectangle>();
            Rectangle playerRect = new Rectangle(Position.X, Position.Y, Position.Width, Position.Height);
            Rectangle overlapRect;

            // Find the collisions
            foreach (GameObject wall in walls)
            {
                if (playerRect.Intersects(wall.Position))
                {
                    intersections.Add(wall.Position);
                }
            }

            // X collisions
            foreach (Rectangle r in intersections)
            {
                overlapRect = Rectangle.Intersect(playerRect, r);
                if (overlapRect.Height > overlapRect.Width)
                {
                    int xdiff = Math.Sign(playerRect.X - r.X);
                    playerRect.X += (xdiff * overlapRect.Width);
                }
            }

            // Y collisions
            foreach (Rectangle r in intersections)
            {
                overlapRect = Rectangle.Intersect(playerRect, r);
                if (overlapRect.Height < overlapRect.Width)
                {
                    int ydiff = Math.Sign(playerRect.Y - r.Y);
                    playerRect.Y += (ydiff * overlapRect.Height);
                }
            }

            _position.X = playerRect.X;
            _position.Y = playerRect.Y;
        }

        #endregion Methods
    }
}