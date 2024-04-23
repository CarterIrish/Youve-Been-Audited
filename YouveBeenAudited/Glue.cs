using Microsoft.Xna.Framework.Graphics;

namespace YouveBeenAudited
{
    internal class Glue : Trap
    {
        private double _speedScalar;

        /// <summary>
        /// Creates a new instance of a glue trap
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="texture">glue texture</param>
        /// <param name="cost">cost of trap</param>
        /// <param name="damageAmnt"></param>
        public Glue(int x, int y, Texture2D texture, int cost, int damageAmnt)
            : base(x, y, texture, cost, damageAmnt)
        {
            _speedScalar = .5;
        }

        public override void DoEffect(Enemy e)
        {
            if (e.IsSlowed == false)
            {
                e.Speed = (int)(e.Speed * _speedScalar);
                e.IsSlowed = true;
                e.TakeDamage(DamageAmnt);
            }
        }
    }
}