using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>
    /// Contains high level information about enemies & interfaces with another class to perform core
    /// updates of an enemy.
    /// </summary>
    internal class EnemyManager
    {
        #region Fields

        // List of points on an enemies path
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
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public List<Vector2> Path { get => _path; set => _path = value; }

        /// <summary>
        /// Gets the enemies.
        /// </summary>
        /// <value>
        /// The enemies.
        /// </value>
        public List<Enemy> Enemies { get => _enemies; }

        /// <summary>
        /// Gets or sets the number of enemies.
        /// </summary>
        /// <value>
        /// The number of enemies.
        /// </value>
        public int NumOfEnemies { get => _numOfEnemies; set => _numOfEnemies = value; }

        /// <summary>
        /// Gets the remaining enemies.
        /// </summary>
        /// <value>
        /// The remaining enemies.
        /// </value>
        public int RemainingEnemies { get => _numOfEnemies - _killedEnemies; }

        /// <summary>
        /// Gets a value indicating whether [enemy at goal].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enemy at goal]; otherwise, <c>false</c>.
        /// </value>
        public bool EnemyAtGoal { get => _enemyAtGoal; }

        /// <summary>
        /// Gets or sets the wave modifier.
        /// </summary>
        /// <value>
        /// The wave modifier.
        /// </value>
        public double WaveModifier { get => _waveModifier; set => _waveModifier = value; }

        /// <summary>
        /// Gets or sets the current wave.
        /// </summary>
        /// <value>
        /// The current wave.
        /// </value>
        public int CurrentWave
        { get => _currentWave; set { _currentWave = value; } }

        /// <summary>
        /// Gets or sets the total waves.
        /// </summary>
        /// <value>
        /// The total waves.
        /// </value>
        public int TotalWaves
        { get => _totalWaves; set { _totalWaves = value; } }

        /// <summary>
        /// Gets the timer.
        /// </summary>
        /// <value>
        /// The timer.
        /// </value>
        public double Timer { get => _timer; }

        /// <summary>
        /// Gets or sets the height of the tile.
        /// </summary>
        /// <value>
        /// The height of the tile.
        /// </value>
        public int TileHeight
        { get => _tileHeight; set { _tileHeight = value; } }

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
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public void LoadContent(ContentManager content)
        {
            _auditorTexture = content.Load<Texture2D>("auditor_spritesheet");
        }

        /// <summary>
        /// Updates the enemies.
        /// </summary>
        /// <param name="gt">The GameTime.</param>
        /// <param name="game">The game.</param>
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
                _enemies[i].Update(gt);
                if (_enemies[i].AtGoal == true)
                {
                    _enemies.RemoveAt(i);
                    _killedEnemies++;
                    game.TakeSafeDamage();
                }
                else if (_enemies[i].Health <= 0)
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
        /// Sets up next wave.
        /// </summary>
        public void NextWave()
        {
            _resetTimer = false;
            _currentWave++;
            _numOfEnemies = (int)(_numOfEnemies * _waveModifier);
            _killedEnemies = 0;
        }

        /// <summary>
        /// Draws the enemies.
        /// </summary>
        /// <param name="sb">The sb.</param>
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