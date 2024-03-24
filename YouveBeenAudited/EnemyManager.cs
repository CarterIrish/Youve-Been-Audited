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

        //Enemy Textures
        private Texture2D _auditorTexture;

        /// <summary>
        /// Creates an EnemyManager.
        /// </summary>
        /// <param name="numOfEnemies"> Number of desired enemies. </param>
        /// <param name="path"> List of points for the enemies to follow. </param>
        public EnemyManager(int numOfEnemies, List<Point> path)
        {
            _numOfEnemies = numOfEnemies;
            _path = path;
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
            for (int i = 0 ; i < _numOfEnemies; i++)
            {
                _enemies.Add(new Enemy(_path[0].X, _path[0].Y, _auditorTexture, 150, _path));
            }
        }

        /// <summary>
        /// Moves all enemies.
        /// </summary>
        public void MoveEnemies()
        {
            foreach (Enemy goober in _enemies) 
            {
                goober.Move();
            }
        }

        public void DrawEnemies()
        {

        }
    }
}