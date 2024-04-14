using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: Manage the enemies on players screen.
    /// </summary>
    internal class EnemyManager
    {
        #region Fields

        private List<Enemy> _enemies;
        private List<Vector2> _path;
        private int _numOfEnemies;
        private int _killedEnemies;
        private double _timer;

        public bool enemyAtGoal;

        //Enemy Textures
        private Texture2D _auditorTexture;

        #endregion Fields

        #region Properties

        public List<Vector2> _Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public List<Enemy> Enemies
        {
            get => _enemies;
        }
        

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates an EnemyManager.
        /// </summary>
        /// <param name="numOfEnemies"> Number of desired enemies. </param>
        /// <param name="path"> List of points for the enemies to follow. </param>
        public EnemyManager(int numOfEnemies, List<Vector2> path)
        {
            _numOfEnemies = numOfEnemies;
            _path = path;
            _enemies = new List<Enemy>();
            _killedEnemies = 0;
            enemyAtGoal = false;
            _timer = 0;
        }

        /// <summary>
        /// Creates a new EnemyManager with an empty enemy path
        /// </summary>
        public EnemyManager(int numOfEnemies)
        {
            _numOfEnemies = numOfEnemies;
            _path = new List<Vector2>();
            _enemies = new List<Enemy>();
            _killedEnemies = 0;
            enemyAtGoal = false;
            _timer = 0;
        }

        /// <summary>
        /// Loads Enemy Textures
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            _auditorTexture = content.Load<Texture2D>("auditor_spritesheet");
        }

        /// <summary>
        /// Creates the list of enemies.
        /// </summary>
        public void CreateEnemies()
        {
            for (int i = 0; i < _numOfEnemies; i++)
            {
                //_enemies.Add(new Enemy((int)_path[0].X, (int)_path[0].Y, _auditorTexture, 150, _path));
            }
        }

        /// <summary>
        /// Moves all enemies.
        /// </summary>
        public void UpdateEnemies(GameTime gt)
        {
            _timer += gt.ElapsedGameTime.TotalSeconds;
            
            if(_timer >= 3 && _enemies.Count+_killedEnemies < _numOfEnemies)
            {
                _enemies.Add(new Enemy((int)_path[0].X, (int)_path[0].Y, _auditorTexture, 150, _path));
                _timer = 0;
            }
            for (int i = 0; i < _enemies.Count;)
            {
                _enemies[i].Update(gt);
                if (_enemies[i].Health <= 0)
                {
                    _enemies.RemoveAt(i);
                    _killedEnemies++;
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Draws all of the enemies.
        /// </summary>
        /// <param name="sb"></param>
        public void DrawEnemies(SpriteBatch sb)
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Draw(sb);
            }
        }

        #endregion Methods
    }
}