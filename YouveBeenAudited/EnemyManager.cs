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

        // Enemy Stuff
        private List<Enemy> _enemies;

        private int _numOfEnemies;
        private int _killedEnemies;
        private bool _enemyAtGoal;

        // Wave Stuff
        private int _totalWaves;

        private int _currentWave;
        private double _waveModifier;

        // Timer
        private double _timer;

        private bool _resetTimer;

        //Enemy Textures
        private Texture2D _auditorTexture;
        private int _tileHeight;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets and sets path
        /// </summary>
        public List<Vector2> Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Gets the enemy list
        /// </summary>
        public List<Enemy> Enemies
        {
            get => _enemies;
        }

        /// <summary>
        /// Gets and sets the number of enemies per wave
        /// </summary>
        public int NumOfEnemies
        {
            set { _numOfEnemies = value; }
        }

        /// <summary>
        /// Gets the number of enemies left in a wave
        /// </summary>
        public int RemainingEnemies
        {
            get { return _numOfEnemies - _killedEnemies; }
        }

        /// <summary>
        /// Gets whether an enemy has reached the goal
        /// </summary>
        public bool EnemyAtGoal
        {
            get => _enemyAtGoal;
        }

        /// <summary>
        /// Gets and sets the wave modifier
        /// </summary>
        public double WaveModifier
        {
            get => _waveModifier;
            set { _waveModifier = value; }
        }

        /// <summary>
        /// Gets and sets the current wave number
        /// </summary>
        public int CurrentWave
        { get => _currentWave; set { _currentWave = value; } }

        /// <summary>
        /// Gets and sets the total wave number
        /// </summary>
        public int TotalWaves
        { get => _totalWaves; set { _totalWaves = value; } }

        /// <summary>
        /// Gets the timer
        /// </summary>
        public double Timer { get => _timer; }

        public int TileHeight { get => _tileHeight; set { _tileHeight = value; } }
        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyManager"/> class.
        /// </summary>
        /// <param name="numOfEnemies">The number of enemies.</param>
        /// <param name="path">The path.</param>
        public EnemyManager(int numOfEnemies, List<Vector2> path)
        {
            _numOfEnemies = numOfEnemies;

            _path = path;
            _enemies = new List<Enemy>();
            _killedEnemies = 0;
            _timer = 0;
            _enemyAtGoal = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyManager"/> class.
        /// </summary>
        /// <param name="numOfEnemies">The number of enemies.</param>
        /// <param name="numOfWaves">The number of waves.</param>
        /// <param name="waveModifier">The wave modifier.</param>
        public EnemyManager(int numOfEnemies, int numOfWaves, double waveModifier)
        {
            _numOfEnemies = numOfEnemies;
            _path = new List<Vector2>();
            _enemies = new List<Enemy>();
            _totalWaves = numOfWaves;
            _waveModifier = waveModifier;
            _currentWave = 1;
            _killedEnemies = 0;
            _enemyAtGoal = false;
            _timer = 0;
            _resetTimer = false;
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
        /// Updates all enemies on the field.
        /// </summary>
        public void UpdateEnemies(GameTime gt, Game1 game)
        {
            _timer += gt.ElapsedGameTime.TotalSeconds;

            // Spawns enemies delayed depending on the current wave
            if (_timer >= ((double)3 / _currentWave) && _enemies.Count + _killedEnemies < _numOfEnemies)
            {
                _enemies.Add(new Enemy((int)_path[0].X, (int)_path[0].Y, 150, (int)(2 * (_currentWave)), _tileHeight, _auditorTexture, _path));
                _timer = 0;
            }

            // If the enemies were killed, wait until 15 seconds and start
            // the next wave. OR call GameOver if the final wave was beat
            if (_killedEnemies == _numOfEnemies)
            {
                if (!_resetTimer)
                {
                    _timer = 0;
                    _resetTimer = true;
                }
                if (_totalWaves == _currentWave)
                {
                    game.GameOver();
                }
                if (_timer >= 15)
                {
                    NextWave();
                }
            }

            // Removes enemies from enemy list if they died, or call GameOver if
            // enemies reached the goal
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
            _resetTimer = false;
            _currentWave++;
            _numOfEnemies = (int)(_numOfEnemies * _waveModifier);
            _killedEnemies = 0;
        }

        /// <summary>
        /// Draws all of the enemies.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
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