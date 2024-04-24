using Microsoft.Xna.Framework.Graphics;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains information for an instance of a glue trap.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.Trap" />
    internal class Glue : Trap
    {
        #region Fields

        private double _speedScalar;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Glue"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture of trap.</param>
        /// <param name="cost">The cost of trap.</param>
        /// <param name="damageAmnt">The damage amnt of trap.</param>
        /// <param name="tileHeight"></param>
        public Glue(int x, int y, Texture2D texture, int cost, int damageAmnt, int tileHeight)
            : base(x, y, texture, cost, damageAmnt, tileHeight)
        {
            _speedScalar = .5;
        }

        /// <summary>
        /// Does the effect of a trap on another object.
        /// </summary>
        /// <param name="e">The object to perform effect on.</param>
        public override void DoEffect(Character e)
        {
            if (e.IsSlowed == false)
            {
                e.Speed = (int)(e.Speed * _speedScalar);
                e.IsSlowed = true;
                e.TakeDamage(DamageAmnt);
            }
        }

        #endregion Methods
    }
}