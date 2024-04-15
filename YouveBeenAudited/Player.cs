using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: Store information specific to the player.
    /// </summary>
    internal class Player : Character
    {
        #region Fields

        private int _money;

        private int _currentFrame;

        private Point _spriteSize;

        private Texture2D _nailTexture;

        private SpriteFont _font;

        private Rectangle _destinationRectangle;

        private KeyboardState _prevKB;

        private CharacterStates _prevState;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current amount of money
        /// posessed.
        /// </summary>
        public int Money { get => _money; }

        public Point SpriteSize { get => _spriteSize; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Create a new player object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="startingMoney"></param>
        /// <param name="health"></param>
        public Player(int x, int y, Texture2D texture, int health, int startingMoney) : base(x, y, texture, health, 10)
        {
            _money = startingMoney;
            _spriteSize = new Point(55, 125);
            _destinationRectangle = new Rectangle(_position.X, _position.Y, _spriteSize.X, _spriteSize.Y);
            _speed = 6;
            _currentFrame = 0;
        }

        /// <summary>
        /// Loads necessary textures
        /// </summary>
        /// <param name="content">ContentManager to load from</param>
        public void LoadContent(ContentManager content)
        {
            _nailTexture = content.Load<Texture2D>("spikes");
            _font = content.Load<SpriteFont>("Arial25");
        }

        /// <summary>Updates the player objects information.</summary>
        /// <param name="gametime">GameTime from Game1</param>
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
        /// Place a trap based on input
        /// </summary>
        public Trap PlaceTrap()
        {
            Trap trap = null;
            if (SingleKeyPress(Keys.Space) && _money >= 20)
            {
                _money -= 20;
                trap = new Trap(_position.X - 10, _position.Y + Position.Height / 6, _nailTexture, 20, 100);
            }
            else if (SingleKeyPress(Keys.D1))
            {
                _money += 100;
            }
            else if (SingleKeyPress(Keys.D2))
            {
            }
            _prevKB = Keyboard.GetState();
            return trap;
        }

        /// <summary>
        /// Changes the players position based on WASD input
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
        /// Checks if a key has been pressed only this frame and not the previous
        /// </summary>
        /// <param name="key">key to check for a single press</param>
        /// <returns>True if the key was pressed only this frame</returns>
        public bool SingleKeyPress(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && _prevKB.IsKeyUp(key);
        }

        /// <summary>
        /// Draws player, traps, and money
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            switch (_currentState)
            {
                case CharacterStates.Left:
                    sb.Draw(
                Texture,
                new Vector2(_position.X, _position.Y),
                new Rectangle(((_spriteSize.X + 25) * _currentFrame) + 560, 0, _spriteSize.X, _spriteSize.Y),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.FlipHorizontally,
                0.0f);
                    break;

                case CharacterStates.Right:
                    sb.Draw(
                Texture,
                new Vector2(_position.X, _position.Y),
                new Rectangle(((_spriteSize.X + 25) * _currentFrame) + 560, 0, _spriteSize.X, _spriteSize.Y),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f);
                    break;

                default:
                    sb.Draw(Texture, _destinationRectangle, new Rectangle(400, 0, _spriteSize.X, _spriteSize.Y), Color.White);
                    break;
            }
        }

        /// <summary>
        /// Updates the animation for the player.
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

        public void ResolveCollisions(List<GameObject> walls)
        {
            List<Rectangle> intersections = new List<Rectangle>();
            Rectangle playerRect = new Rectangle(Position.X, Position.Y, SpriteSize.X, SpriteSize.Y);
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