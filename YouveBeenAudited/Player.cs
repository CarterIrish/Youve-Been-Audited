using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private List<Trap> _inventory;

        private List<Trap> _traps;

        private Texture2D _nailTexture;

        private KeyboardState _prevKB;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current amount of money
        /// posessed.
        /// </summary>
        public int Money { get => _money; }

        /// <summary>
        /// Gets the inventory of player.
        /// </summary>
        public List<Trap> Inventory { get => _inventory; }


        /// <summary>
        /// Gets the placed traps
        /// </summary>
        public List<Trap> Traps { get => _inventory; }

        #endregion Properties

        /// <summary>
        /// Create a new player object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="startingMoney"></param>
        /// <param name="health"></param>
        public Player(int x, int y, Texture2D texture, int health, int startingMoney) : base(x, y, texture, health)
        {
            _money = startingMoney;
            _traps = new List<Trap>();
        }

        #region Methods

        public void LoadContent(ContentManager content)
        {
            _nailTexture = content.Load<Texture2D>("Spikes");
        }

        /// <summary>Updates the player objects information.</summary>
        /// <param name="gametime">GameTime from Game1</param>
        public override void Update(GameTime gametime)
        {
            Move();
            PlaceTrap();
            base.Update(gametime);
        }


        /// <summary>
        /// Buys a trap for the user to use.
        /// </summary>
        /// <param name="t">The trap to be bought</param>
        /// <returns>Returns true if successfully bought false otherwise.</returns>
        private bool Buy(Trap t)
        {
            // TODO: Should we use an array of lists to store trap types?
            if (t.Cost < _money)
            {
                _inventory.Add(t);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Place a trap based on input
        /// </summary>
        public void PlaceTrap()
        {

            if(SingleKeyPress(Keys.D1))
            {
                _traps.Add(new Trap(_position.X, Position.Y, _nailTexture, 20, 100));
            }
            else if(SingleKeyPress(Keys.D2))
            {

            }
            else if(SingleKeyPress(Keys.D2))
            {

            }

            _prevKB = Keyboard.GetState();
        }
        
        /// <summary>
        /// Changes the players position based on WASD input
        /// </summary>
        public void Move()
        {
            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.W))
            {
                _position.Y -= _speed;
            }
            if (kbs.IsKeyDown(Keys.A))
            {
                _position.X -= _speed;
            }
            if (kbs.IsKeyDown(Keys.S))
            {
                _position.Y += _speed;
            }
            if (kbs.IsKeyDown(Keys.D))
            {
                _position.X += _speed;
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

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            foreach (Trap trap in _traps)
            {
                trap.Draw(sb);
            }
        }

        #endregion Methods
    }
}