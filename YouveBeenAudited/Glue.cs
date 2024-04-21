using Microsoft.Xna.Framework.Graphics;

namespace YouveBeenAudited
{
    internal class Glue : Trap
    {
        private double _speedScalar;

        public Glue(int x, int y, Texture2D texture, int cost, int damageAmnt)
            : base(x, y, texture, cost, damageAmnt)
        {
            _speedScalar = .5;
        }

        public override void DoEffect(Enemy e)
        {
            if (e.IsSlowed == false)
            {
                System.Diagnostics.Debug.WriteLine("Slowing enemy");
                e.Speed = (int)(e.Speed * _speedScalar);
                e.IsSlowed = true;
            }
        }
    }
}