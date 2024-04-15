using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace YouveBeenAudited
{
    /// <summary>
    /// States of the game.
    /// </summary>
    internal enum GameStates
    {
        Menu,
        Game,
        Options,
        GameOver
    }

    internal enum TileType
    {
        Wall,
        Wood
    }

    internal delegate void BtnClickedDelegate(Button b);

    /// <summary>
    /// Authors: Carter I, Chase C, Jesse M & Jack M.
    /// Class: IGME 106.
    /// Date: 2/25/2024.
    /// Purpose: Group game project.
    /// Name: You've Been Audited.
    /// </summary>
    public class Game1 : Game
    {
        #region Game Fields

        //Managers
        private EnemyManager enemyManager;

        // Monogame fields
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // Window fields
        private Point _windowCenter;

        private Point _windowSize;

        private readonly Point _ReferenceWindow;
        public readonly double _UIscalar;

        // Game States
        private GameStates _gameState;

        // Player
        private Player _player;

        // Traps
        private List<Trap> _traps;

        //Animation
        public const double _secondsPerFrame = 6.5f / 60; //This is here for reference.

        private double _timeCount;

        private Texture2D _playerTexture;

        // SpriteFonts
        private SpriteFont _arial25;

        // Element lists
        private List<Button> _menuButtons;

        private List<Button> _gameButtons;
        private List<Button> _optionButtons;
        private List<Button> _gameOverButtons;

        // Button textures
        private Texture2D _optionsButtonTexture;

        private Texture2D _startButtonTexture;
        private Texture2D _exitButtonTexture;
        private Texture2D _resumeButtonTexture;
        private Texture2D _menuButtonTexture;

        //Title Textures
        private Texture2D _titleTexture;

        //Map Textures
        private Texture2D _woodFloorTexture;

        // Input sources
        private MouseState _mouseState;

        private KeyboardState _prevKbState;

        //Level Information
        private int _tileLength; // Dimensions of a square tile

        private TileType[,] _map; // 2D array representing the tile types of the map
        private int _mapWidth; // pixel width of the playable map
        private int _marginWidth; // pixel width of the side margins
        private List<GameObject> _wallList; // list of walls in the map

        #endregion Game Fields

        #region Pre GameLoop

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set window to borderless windowed as default
            Window.IsBorderless = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            // set up ui scaler
            _ReferenceWindow = new Point(_graphics.GraphicsDevice.DisplayMode.Width, _graphics.GraphicsDevice.DisplayMode.Height);
            _UIscalar = _graphics.PreferredBackBufferWidth / (double)_ReferenceWindow.Y;

            // initialize useful window measurements
            _windowCenter = new Point(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _windowSize = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            // Initialize key game fields.
            _menuButtons = new List<Button>();
            _gameButtons = new List<Button>();
            _optionButtons = new List<Button>();
            _gameOverButtons = new List<Button>();
            _gameState = GameStates.Menu;
            _traps = new List<Trap>();
            _wallList = new List<GameObject>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _playerTexture = Content.Load<Texture2D>("player_spritesheet");
            _player = new Player(50, 50, _playerTexture, 100, 100);
            _arial25 = Content.Load<SpriteFont>("Arial25");
            _menuButtonTexture = Content.Load<Texture2D>("MenuButton");
            _startButtonTexture = Content.Load<Texture2D>("StartButton");
            _exitButtonTexture = Content.Load<Texture2D>("ExitButton");
            _optionsButtonTexture = Content.Load<Texture2D>("OptionsButton");
            _resumeButtonTexture = Content.Load<Texture2D>("ResumeButton");
            _titleTexture = Content.Load<Texture2D>("Title");
            _woodFloorTexture = Content.Load<Texture2D>("tile_wood_floor");
            _player.LoadContent(Content);

            //Animation Setup
            _timeCount = 0;

            #region Button creation

            // Menu start button
            Button StartButton = new Button(
                _windowCenter.X - (int)(_startButtonTexture.Width * _UIscalar) / 2,
                _windowCenter.Y + (int)(40 * _UIscalar),
                _startButtonTexture,
                "StartButton",
                Color.White,
                _UIscalar);

            _menuButtons.Add(StartButton);
            StartButton.BtnClicked += ButtonCheck;

            // MenuExit game button
            Button ExitGameButton = new Button(
                _windowCenter.X - (int)(_exitButtonTexture.Width * _UIscalar) / 2,
                StartButton.Position.Y + (int)(_exitButtonTexture.Height * _UIscalar + 40),
                _exitButtonTexture,
                "ExitGameButton",
                Color.White,
                _UIscalar);
            _menuButtons.Add(ExitGameButton);
            ExitGameButton.BtnClicked += ButtonCheck;

            // resume game button
            Button ResumeGame = new Button(
                _windowCenter.X - (int)(_resumeButtonTexture.Width * _UIscalar) / 2,
                _windowCenter.Y - (int)(_resumeButtonTexture.Height * _UIscalar),
                _resumeButtonTexture,
                "ResumeGameButton",
                Color.White,
                _UIscalar);
            _optionButtons.Add(ResumeGame);
            ResumeGame.BtnClicked += ButtonCheck;

            // Options exit game button
            Button optionsExit = new Button(
                _windowCenter.X - (int)(_exitButtonTexture.Width * _UIscalar) / 2,
                _windowCenter.Y + (int)(_exitButtonTexture.Height * _UIscalar),
                _exitButtonTexture,
                "ExitGameButton",
                Color.White,
                _UIscalar);
            _optionButtons.Add(optionsExit);
            optionsExit.BtnClicked += ButtonCheck;

            // game over exit game
            Button gameOverExit = new Button(
                _windowCenter.X - (int)(_exitButtonTexture.Width * _UIscalar) / 2,
                _windowCenter.Y + (int)(_exitButtonTexture.Height * _UIscalar),
                _exitButtonTexture,
                "ExitGameButton",
                Color.White,
                _UIscalar);
            _gameOverButtons.Add(gameOverExit);
            gameOverExit.BtnClicked += ButtonCheck;

            Button gameOverMenu = new Button(
                _windowCenter.X - (int)(_menuButtonTexture.Width * _UIscalar) / 2,
                _windowCenter.Y - (int)(_menuButtonTexture.Height * _UIscalar),
                _menuButtonTexture,
                "MenuButton",
                Color.White,
                _UIscalar);
            _gameOverButtons.Add(gameOverMenu);
            gameOverMenu.BtnClicked += ButtonCheck;

            #endregion Button creation
        }

        #endregion Pre GameLoop

        #region GameLoop

        /// <summary>
        /// Updates the game elements.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            // TODO: remove this statement after menu UI functional

            // Get current input states
            _mouseState = Mouse.GetState();

            // Update according to game states.
            switch (_gameState)
            {
                // On  menu
                case GameStates.Menu:
                    // Check all menu buttons for clicks
                    foreach (Button b in _menuButtons)
                    {
                        b.CheckClick(_mouseState);
                    }
                    break;

                // Active game
                case GameStates.Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                    {
                        _gameState = GameStates.Options;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.F1) == true)
                    {
                        _gameState = GameStates.GameOver;
                    }
                    _timeCount += gameTime.ElapsedGameTime.TotalSeconds;
                    _player.Update(gameTime);
                    Trap trap;
                    if ((trap = _player.PlaceTrap()) != null)
                    {
                        _traps.Add(trap);
                    }
                    Collisions();
                    _timeCount = _player.UpdateAnimation(_timeCount);
                    enemyManager.UpdateEnemies(gameTime, this);
                    if (enemyManager.enemyAtGoal)
                    {
                        _gameState = GameStates.GameOver;
                    }

                    break;

                // Options screen / paused
                case GameStates.Options:
                    foreach (Button b in _optionButtons)
                    {
                        b.CheckClick(_mouseState);
                    }
                    break;

                // Game over
                case GameStates.GameOver:
                    foreach (Button b in _gameOverButtons)
                    {
                        b.CheckClick(_mouseState);
                    }
                    _traps.Clear();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>Draws the game elements to screen.</summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Designed for a 2560x1440p monitor - ref size

            GraphicsDevice.Clear(Color.GreenYellow);

            // Start the sprite batch for drawing all elements to screen
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);

            // Draw according to game state
            switch (_gameState)
            {
                // On menu
                case GameStates.Menu:
                    _spriteBatch.Draw(_titleTexture, new Rectangle((int)(_windowCenter.X - _titleTexture.Width / 2 * _UIscalar), (int)(_windowSize.Y / 100 * 2),
                        (int)((_titleTexture.Width) * _UIscalar), (int)((_titleTexture.Height) * _UIscalar)), Color.White);
                    foreach (Button b in _menuButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }

                    break;
                // Active game
                case GameStates.Game:
                    DrawLevel(_spriteBatch);
                    _spriteBatch.DrawString(_arial25, $"${_player.Money}", new Vector2(50, 50), Color.DarkGreen);
                    enemyManager.DrawEnemies(_spriteBatch);
                    foreach (Trap trap in _traps)
                    {
                        trap.Draw(_spriteBatch);
                    }
                    _player.Draw(_spriteBatch);
                    break;
                // Options/pause menu
                case GameStates.Options:
                    foreach (Button b in _optionButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    break;
                // Game over
                case GameStates.GameOver:
                    foreach (Button b in _gameOverButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion GameLoop

        #region Methods

        /// <summary>
        /// Sets game state to game over.
        /// </summary>
        public void GameOver()
        {
            _gameState = GameStates.GameOver;
        }

        /// <summary>Checks all buttons in provided list for click state, and performs actions based off that information.</summary>
        /// <param name="b">The button to be checked</param>
        private void ButtonCheck(Button b)
        {
            switch (b.Name)
            {
                // If its the start button
                case "StartButton":
                    System.Diagnostics.Debug.WriteLine("Change State ==> Game");
                    _gameState = GameStates.Game;
                    NextLevel("../../../../actualTestingFile.level");
                    break;
                // If its the exit game button
                case "ExitGameButton":
                    System.Diagnostics.Debug.WriteLine("ChangeState ==> QuitGame");
                    Exit();
                    break;

                case "ResumeGameButton":
                    _gameState = GameStates.Game;
                    break;

                case "MenuButton":
                    _gameState = GameStates.Menu;
                    break;
            }
        }

        /// <summary>
        /// Creates the next level.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void NextLevel(string fileName)
        {
            enemyManager = new EnemyManager(3, 3, 1);
            enemyManager.LoadContent(Content);
            ReadFile(fileName);
        }

        /// <summary>
        /// Reads a file into the game
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadFile(string fileName)
        {
            StreamReader input = new StreamReader(fileName);
            string[] dimensions;

            //reads the dimensions
            dimensions = input.ReadLine().Split(",");
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);
            _map = new TileType[height, width];

            _tileLength = _graphics.PreferredBackBufferHeight / _map.GetLength(0);
            int mapWidth = _tileLength * _map.GetLength(1);
            _marginWidth = (_graphics.PreferredBackBufferWidth - mapWidth) / 2;

            //reads in the floors/walls
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    switch (input.Read())
                    {
                        case 'w':
                            break;

                        case 'g':
                            _map[i, k] = TileType.Wood;
                            break;

                        case 'b':
                            _map[i, k] = TileType.Wall;
                            _wallList.Add(new GameObject(new Rectangle(_marginWidth + (k * _tileLength), (i * _tileLength),
                                _tileLength, _tileLength), _woodFloorTexture));
                            break;

                        default:
                            break;
                    }
                }
                input.ReadLine();
            }

            //adds the vectors to the enemy path List
            string[] points;
            enemyManager._Path.Clear();
            points = input.ReadToEnd().Split('|');
            foreach (string p in points)
            {
                if (!p.Equals(""))
                {
                    string[] coordinates = p.Split(",");
                    int x = (int.Parse(coordinates[0]) * _tileLength) + _marginWidth + (_tileLength / 2);
                    int y = (int.Parse(coordinates[1]) * _tileLength) + (_tileLength / 2);
                    enemyManager._Path.Add(new Vector2((float)x, (float)y));
                }
            }
        }

        /// <summary>
        /// Draws the level
        /// </summary>
        /// <param name="sb"></param>
        private void DrawLevel(SpriteBatch sb)
        {
            int height = _graphics.PreferredBackBufferHeight;
            int width = _graphics.PreferredBackBufferWidth;

            _tileLength = height / _map.GetLength(0);

            int mapWidth = _tileLength * _map.GetLength(1);
            int MarginWidth = (width - mapWidth) / 2;

            //Drawing the  map
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int k = 0; k < _map.GetLength(1); k++)
                {
                    switch (_map[i, k])
                    {
                        case TileType.Wall:
                            sb.Draw(_woodFloorTexture, new Rectangle(MarginWidth + (k * _tileLength), (i * _tileLength),
                                _tileLength, _tileLength), Color.Black);
                            break;

                        case TileType.Wood:
                            sb.Draw(_woodFloorTexture, new Rectangle(MarginWidth + (k * _tileLength), (i * _tileLength),
                                _tileLength, _tileLength), Color.White);
                            break;

                        default:
                            break;
                    }
                }
            }

            foreach (Vector2 p in enemyManager._Path)
            {
                float x = p.X;
                float y = p.Y;
                Rectangle pointRect = new Rectangle((int)(x - 5), (int)(y - 5), 10, 10);
                sb.Draw(_woodFloorTexture, pointRect, Color.Blue);
            }
        }

        public void Collisions()
        {
            // Checks trap collisions against
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                for (int i = 0; i < _traps.Count;)
                {
                    if (_traps[i].CheckCollisions(enemy))
                    {
                        enemy.TakeDamage(_traps[i].DamageAmnt);
                        _traps.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            // Check collisions with walls
            _player.ResolveCollisions(_wallList);
        }
    }

    #endregion Methods
}