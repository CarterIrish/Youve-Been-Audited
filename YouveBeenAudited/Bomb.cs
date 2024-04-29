using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains all information specific to a bomb trap.
    /// </summary>
    /// <seealso cref="YouveBeenAudited.Trap" />
    internal class Bomb : Trap
    {
        #region Fields

        private double _fuseTime;
        private double _explosionTime;
        private bool _isLit;
        private bool _isExploding;
        private int _tileHeight;

        #endregion Fields

        #region Properties

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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Bomb"/> class.
        /// </summary>
        /// <param name="x">The x coord.</param>
        /// <param name="y">The y coord.</param>
        /// <param name="texture">The texture of trap.</param>
        /// <param name="cost">The cost of trap.</param>
        /// <param name="damageAmnt">The damage amnt of trap.</param>
        /// <param name="tileHeight">Height of map tile for scaling</param>
        public Bomb(int x, int y, Texture2D texture, int cost, int damageAmnt, int tileHeight) : base(x, y, texture, cost, damageAmnt, tileHeight)
        {
            _isExploding = false;
            _fuseTime = 3;
            _explosionTime = .1;
            _isLit = true;
            _tileHeight = tileHeight;
        }

        /// <summary>
        /// Does the effect of a trap on another object.
        /// </summary>
        /// <param name="e">The object to perform effect on.</param>
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
        /// <param name="gt">GameTime object</param>
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
        /// Expands the size of the bomb when it explodes
        /// </summary>
        public void Explode()
        {
            this._position.Inflate(_tileHeight, _tileHeight);
            _isExploding = true;
        }

        /// <summary>
        /// Draws this instance of an object.
        /// </summary>
        /// <param name="sb">The SpriteBatch.</param>
        public override void Draw(SpriteBatch sb)
        {
            if (!_isExploding)
            {
                if (_fuseTime < 1)
                {
                    sb.Draw(_texture, _position, Color.Red);
                }
                else if (_fuseTime < 2)
                {
                    sb.Draw(_texture, _position, Color.Orange);
                }
                else if (_fuseTime < 3)
                {
                    sb.Draw(_texture, _position, Color.Yellow);
                }
                else
                    base.Draw(sb);
            }
        }

        #endregion Methods
    }
}