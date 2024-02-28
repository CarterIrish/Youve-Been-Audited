using Microsoft.Xna.Framework;
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
        // ------ Fields ------
        private int _money;
        private List<Trap> _inventory;

        // ------ Properties ------

        /// <summary>
        /// Gets the current amount of money
        /// posessed.
        /// </summary>
        public int Money { get => _money; }

        // TODO: Review if inventory property need an indexer.
        /// <summary>
        /// Gets the inventory of player.
        /// </summary>
        public List<Trap> Inventory { get => _inventory; }


        // ------ Methods ------


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
        }

        /// <summary>
        /// Buys a trap for the user to use.
        /// </summary>
        /// <param name="t">The trap to be bought</param>
        private void Buy(Trap t)
        {
            // TODO: Create the buying logic.
        }
    }
}
