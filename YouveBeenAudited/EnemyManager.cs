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

        private List<Vector2> _path;

        private List<Enemy> _enemies;
        private int _numOfEnemies;
        private int _killedEnemies;

        private int _totalWaves;
        private int _currentWave;
        private double _waveModifier;

        private double _timer;

        public bool enemyAtGoal;

        //Enemy Textures
        private Texture2D _auditorTexture;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets and sets path
        /// </summary>
        public List<Vector2> _Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Gets enemy list
        /// </summary>
        public List<Enemy> Enemies
        {
            get => _enemies;
        }

        /// <summary>
        /// Returns the number of enemies left in a wave
        /// </summary>
        public int RemainingEnemies
        {
            get { return _numOfEnemies - _killedEnemies; }
        }

        /// <summary>
        /// Gets and sets Current Wave
        /// </summary>
        public int CurrentWave { get => _currentWave; set { _currentWave = value; } }

        /// <summary>
        /// Gets and sets total waves
        /// </summary>
        public int TotalWaves { get => _totalWaves; set { _totalWaves = value; } }

        /// <summary>
        /// Gets Timer
        /// </summary>
        public double Timer { get => _timer; }


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
            _timer = 0;
            enemyAtGoal = false;
        }

        /// <summary>
        /// Creates a new EnemyManager with an empty enemy path
        /// </summary>
        public EnemyManager(int numOfEnemies, int numOfWaves, int waveModifier)
        {
            _numOfEnemies = numOfEnemies;
            _path = new List<Vector2>();
            _enemies = new List<Enemy>();
            _totalWaves = numOfWaves;
            _waveModifier = waveModifier;
            _currentWave = 1;
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
        /// Moves all enemies.
        /// </summary>
        public void UpdateEnemies(GameTime gt, Game1 game)
        {
            _timer += gt.ElapsedGameTime.TotalSeconds;
            if (_timer >= 3 && _enemies.Count + _killedEnemies < _numOfEnemies)
            {
                _enemies.Add(new Enemy((int)_path[0].X, (int)_path[0].Y, _auditorTexture, 150, _path));
                _timer = 0;
            }
            if (_killedEnemies == _numOfEnemies)
            {
                
                if (TotalWaves == CurrentWave)
                {
                    game.GameOver();
                }
                if (_timer >= 15)
                {
                    NextWave();
                }
            }
            for (int i = 0; i < _enemies.Count;)
            {
                if (_enemies[i].AtGoal == true)
                {
                    game.GameOver();
                }
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
        /// Starts the next wave
        /// </summary>
        public void NextWave()
        {
            CurrentWave++;
            _numOfEnemies = (int)(_numOfEnemies * _waveModifier);
            _killedEnemies = 0;
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