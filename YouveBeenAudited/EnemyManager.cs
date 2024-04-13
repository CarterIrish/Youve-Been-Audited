﻿using Microsoft.Xna.Framework;
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
        #region Fields

        private List<Enemy> _enemies;
        private List<Point> _path;
        private int _numOfEnemies;

        public bool enemyAtGoal;

        private readonly double _UIscalar;

        //Enemy Textures
        private Texture2D _auditorTexture;

        #endregion Fields

        #region Properties

        public List<Point> _Path
        {
            get { return _path; }
            set { _path = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates an EnemyManager.
        /// </summary>
        /// <param name="numOfEnemies"> Number of desired enemies. </param>
        /// <param name="path"> List of points for the enemies to follow. </param>
        public EnemyManager(int numOfEnemies, List<Point> path)
        {
            _numOfEnemies = numOfEnemies;
            _path = path;
            _enemies = new List<Enemy>();
            enemyAtGoal = false;
        }

        /// <summary>
        /// Creates a new EnemyManager with an empty enemy path
        /// </summary>
        public EnemyManager(int numOfEnemies, double scalar)
        {
            _numOfEnemies = numOfEnemies;
            _path = new List<Point>();
            _enemies = new List<Enemy>();
            enemyAtGoal = false;
            _UIscalar = scalar;
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
                _enemies.Add(new Enemy(_path[0].X, _path[0].Y, _auditorTexture, 150, _path, _UIscalar));
            }
        }

        /// <summary>
        /// Moves all enemies.
        /// </summary>
        public void UpdateEnemies(GameTime gt)
        {
            foreach (Enemy goober in _enemies)
            {
                goober.Update(gt);
                //if (goober.AtGoal)
                //{
                //    enemyAtGoal = true;
                //}
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