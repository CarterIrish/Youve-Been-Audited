using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShapeUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouveBeenAudited
{
    internal class Bomb : Trap
    {
        double _fuseTime;
        double _explosionTime;
        private bool _isLit;
        private bool _isExploding;

        /// <summary>
        /// determines if the bomb is currently lit
        /// </summary>
        public bool IsLit
        {
            get { return _isLit; }
        }

        /// <summary>
        /// determines if the bomb is currently exploding
        /// </summary>
        public bool IsExploding
        {
            get { return _isExploding; }
        }

        /// <summary>
        /// gets the time until the bomb is done exploding
        /// </summary>
        public double ExplosionTime
        {
            get { return _explosionTime; }
        }

        public Bomb(int x, int y, Texture2D texture, int cost, int damageAmnt) : base(x, y, texture, cost, damageAmnt)
        {
            _isExploding = false;
            _fuseTime = 3;
            _explosionTime = .1;
            _isLit = true;
        }

        public override void DoEffect(Character e)
        {
            if (!_isLit)
            {
                _isLit = true;
            }
            else if (_isExploding)
            {
                e.TakeDamage(_damageAmnt);
            }
        }

        /// <summary>
        /// Updates the time of either the fuse timer or the explosion timer
        /// </summary>
        /// <param name="gt"></param>
        public void UpdateTime(GameTime gt)
        {
            if (_isLit)
            {
                if (_fuseTime > 0)
                {
                    _fuseTime -= gt.ElapsedGameTime.TotalSeconds;
                }
                else if (!_isExploding)
                {
                    Explode();
                }
                else if (_explosionTime > 0)
                {
                    _explosionTime -= gt.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        /// <summary>
        /// expands the size of the bomb when it explodes
        /// </summary>
        public void Explode()
        {
            this._position.Inflate(100, 100);
            _isExploding = true;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!_isExploding)
            {
                base.Draw(sb);
            }
        }
    }
}
