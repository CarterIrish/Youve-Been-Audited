using Microsoft.Xna.Framework.Graphics;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains information specific to spike traps.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.Trap" />
    internal class Spike : Trap
    {
        #region Fields

        private int _health;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the health of the trap
        /// </summary>
        public int Health
        {
            get { return _health; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Spike"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture of trap.</param>
        /// <param name="cost">The cost of trap.</param>
        /// <param name="damageAmnt">The damage amnt of trap.</param>
        /// <param name="tileHeight"></param>
        public Spike(int x, int y, Texture2D texture, int cost, int damageAmnt, int tileHeight) : base(x, y, texture, cost, damageAmnt, tileHeight)
        {
            _health = 2;
        }

        /// <summary>
        /// Does the effect of trap on another object.
        /// </summary>
        /// <param name="e">The object to perform effect on.</param>
        public override void DoEffect(Character e)
        {
            e.TakeDamage(_damageAmnt);
            _health--;
        }

        #endregion Methods
    }
}