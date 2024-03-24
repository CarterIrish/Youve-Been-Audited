using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace YouveBeenAudited
{
    /// <summary>
    /// Manage the traps on players screen.
    /// </summary>
    internal class TrapManager
    {
        private List<Trap> _traps;

        private Texture2D nailTexture;

        #region Properties

        /// <summary>
        /// Indexer for list of traps
        /// </summary>
        /// <param name="index">Index to get/set</param>
        /// <returns>Trap at a given index</returns>
        public Trap this[int index]
        {
            get
            {
                if(index < 0 || index >= _traps.Count)
                {
                    throw new IndexOutOfRangeException("Index is out of the bounds of the array");
                }
                return _traps[index];
            }
            set
            {
                if (index < 0 || index >= _traps.Count)
                {
                    throw new IndexOutOfRangeException("Index is out of the bounds of the array");
                }
                _traps[index] = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a TrapManager with an empty list of traps
        /// </summary>
        public TrapManager()
        {
            _traps = new List<Trap>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Adds a trap to trap list
        /// </summary>
        /// <param name="trap">Trap to add</param>
        public void AddTrap(Trap trap)
        {
            _traps.Add(trap);
        }
        #endregion

        public void LoadContent(ContentManager content)
        {
            nailTexture = content.Load<Texture2D>("Spikes");
        }
    }
}