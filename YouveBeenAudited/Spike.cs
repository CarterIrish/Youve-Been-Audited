using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouveBeenAudited
{
    internal class Spike : Trap
    {
        int _health;

        /// <summary>
        /// Gets the health of the trap
        /// </summary>
        public int Health
        {
            get { return _health; }
        }

        public Spike(int x, int y, Texture2D texture, int cost, int damageAmnt) : base(x, y, texture, cost, damageAmnt)
        {
            _health = 2;
        }

        public override void DoEffect(Character e)
        {
            e.TakeDamage(_damageAmnt);
            _health--;
        }
    }
}
