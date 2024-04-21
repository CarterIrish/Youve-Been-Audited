using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace YouveBeenAudited
{
    internal class Glue : Trap
    {
        double _speedScalar;


        public Glue(int x, int y, Texture2D texture, int cost, int damageAmnt)
            : base(x, y, texture, cost, damageAmnt)
        {
            _speedScalar = .5;
        }

        public override void DoEffect(Enemy e)
        {
            if (!e.IsSlowed)
            {
                e.Speed = (int)(e.Speed * _speedScalar);
                e.IsSlowed = true;
            }
        }
    }
}
