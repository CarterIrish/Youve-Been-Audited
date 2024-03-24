using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>
    /// Purpose: Manage the enemies on players screen.
    /// </summary>
    internal class EnemyManager
    {
        private List<Enemy> _enemies;
        private List<Point> _path;
        private int _numOfEnemies;
        private Game1 _game1;

        //Enemy Textures
        private Texture2D _auditorTexture;

        public List<Point> _Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Creates an EnemyManager.
        /// </summary>
        /// <param name="numOfEnemies"> Number of desired enemies. </param>
        /// <param name="path"> List of points for the enemies to follow. </param>
        public EnemyManager(int numOfEnemies, List<Point> path, Game1 game1)
        {
            _numOfEnemies = numOfEnemies;
            _path = path;
            _enemies = new List<Enemy>();
            _game1 = game1;
        }

        /// <summary>
        /// Creates a new EnemyManager with an empty enemy path
        /// </summary>
        public EnemyManager(int numOfEnemies, Game1 game1)
        {
            _numOfEnemies = numOfEnemies;
            _path = new List<Point>();
            _enemies = new List<Enemy>();
            _game1 = game1;
        }

        /// <summary>
        /// Loads Enemy Textures
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            _auditorTexture = content.Load<Texture2D>("auditor");
        }

        /// <summary>
        /// Creates the list of enemies.
        /// </summary>
        public void CreateEnemies()
        {
            for (int i = 0; i < _numOfEnemies; i++)
            {
                Enemy em = new Enemy(_path[0].X, _path[0].Y, _auditorTexture, 150, _path);
                em.EnemyAtGoal += _game1.GameOver;
                _enemies.Add(em);
            }
        }

        /// <summary>
        /// Moves all enemies.
        /// </summary>
        public void UpdateEnemies(GameTime gt, GameStates gamestate)
        {
            foreach (Enemy goober in _enemies)
            {
                goober.Update(gt);
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
    }
}