using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ShapeUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace YouveBeenAudited
{
    /// <summary>
    /// States of the game.
    /// </summary>
    internal enum GameStates
    {
        Menu,
        LevelSelect,
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
        private EnemyManager _enemyManager;

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

        // Music
        private Song _moonlightSonata;

        private Song _appassionata;

        //Animation
        //public const double _secondsPerFrame = 6.5f / 60; //This is here for reference.

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

        //Menu Textures
        private Texture2D _titleTexture;

        private Texture2D _pauseTexture;

        //Map Textures
        private Texture2D _woodFloorTexture;

        private Texture2D _wallFloralTexture;
        private Texture2D _grassFloorTexture;
        private Texture2D _nailTexture;
        private Texture2D _glueTexture;

        // Input sources
        private MouseState _mouseState;

        private KeyboardState _prevKbState;

        //Level Information
        private int _tileLength;    // Dimensions of a square tile

        private TileType[,] _map;   // 2D array representing the tile types of the map
        private int _marginWidth;   // pixel width of the side margins
        private List<GameObject> _wallList; // list of walls in the map

        //Debugger
        private bool _debug;

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

            // turns debugger on/off
            _debug = true;
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

            _playerTexture = Content.Load<Texture2D>("player_spritesheet");
            _arial25 = Content.Load<SpriteFont>("Arial25");
            _menuButtonTexture = Content.Load<Texture2D>("MenuButton");
            _startButtonTexture = Content.Load<Texture2D>("StartButton");
            _exitButtonTexture = Content.Load<Texture2D>("ExitButton");
            _optionsButtonTexture = Content.Load<Texture2D>("OptionsButton");
            _resumeButtonTexture = Content.Load<Texture2D>("ResumeButton");
            _titleTexture = Content.Load<Texture2D>("Title");
            _pauseTexture = Content.Load<Texture2D>("pause_screen");
            _woodFloorTexture = Content.Load<Texture2D>("tile_wood_floor");
            _wallFloralTexture = Content.Load<Texture2D>("tile_floral_wall");
            _grassFloorTexture = Content.Load<Texture2D>("tile_grass_large");
            _nailTexture = Content.Load<Texture2D>("spikes");
            _glueTexture = Content.Load<Texture2D>("glue");
            _player = new Player(999, 999, _playerTexture, 999, 999, 999, 999);
            _player.LoadContent(Content);
            _appassionata = (Content.Load<Song>("Appassionata"));
            _moonlightSonata = (Content.Load<Song>("Moonlight Sonata"));

            //MediaPlayer.Play(_appassionata);
            //MediaPlayer.IsRepeating = true;

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

                // Level Select (Between Menu And Game)
                case GameStates.LevelSelect:

                    break;

                // Active game
                case GameStates.Game:
                    // Switches Game States if conditions met
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                    {
                        _gameState = GameStates.Options;
                    }
                    int currentEnemies = _enemyManager.RemainingEnemies;
                    _timeCount += gameTime.ElapsedGameTime.TotalSeconds;

                    // Deals with player and trap interactions
                    _player.Update(gameTime);
                    Trap trap;
                    if ((trap = PlaceTrap()) != null)
                    {
                        _traps.Add(trap);
                    }
                    ResolveCollisions();

                    _timeCount = _player.UpdateAnimation(_timeCount);

                    _enemyManager.UpdateEnemies(gameTime, this);
                    if (currentEnemies > _enemyManager.RemainingEnemies)
                    {
                        _player.Money += 100 * (currentEnemies - _enemyManager.RemainingEnemies); // Player gets money with each kill
                    }
                    if (_enemyManager.EnemyAtGoal)
                    {
                        _gameState = GameStates.GameOver;
                    }

                    DebugInputs();

                    break;

                // Options screen / paused
                case GameStates.Options:
                    MediaPlayer.Pause();

                    foreach (Button b in _optionButtons)
                    {
                        b.CheckClick(_mouseState);
                    }
                    break;

                // Game over
                case GameStates.GameOver:
                    MediaPlayer.Stop();
                    foreach (Button b in _gameOverButtons)
                    {
                        b.CheckClick(_mouseState);
                    }
                    _traps.Clear();
                    break;
            }
            _prevKbState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>Draws the game elements to screen.</summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Designed for a 2560x1440p monitor - ref size

            GraphicsDevice.Clear(Color.Bisque);

            // Start the sprite batch for drawing all elements to screen
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null);

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
                    _spriteBatch.End();
                    break;

                //On Level Select
                case GameStates.LevelSelect:

                    break;

                // Active game
                case GameStates.Game:
                    _spriteBatch.Draw(_grassFloorTexture, new Rectangle(0, 0, _grassFloorTexture.Width * 3 * (int)_UIscalar, _grassFloorTexture.Height * 3 * (int)_UIscalar), Color.White);
                    DrawLevel(_spriteBatch);
                    // Handles Text UI
                    _spriteBatch.DrawString(_arial25, $"${_player.Money}", new Vector2(50, 50), Color.DarkGreen, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(_arial25, $"Wave {_enemyManager.CurrentWave}/{_enemyManager.TotalWaves}", new Vector2(_windowCenter.X - 150, 50), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    if (_enemyManager.RemainingEnemies == 0)
                    {
                        _spriteBatch.DrawString(_arial25, $"Time Till Next Wave:" + string.Format("{0:0.00}", 15 - _enemyManager.Timer), new Vector2(_windowCenter.X - 350, 150), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }
                    else
                    {
                        _spriteBatch.DrawString(_arial25, $"Enemies Left in Wave: {_enemyManager.RemainingEnemies}", new Vector2(_windowCenter.X - 350, 150), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }
                    

                    foreach (Trap trap in _traps)
                    {
                        trap.Draw(_spriteBatch);
                    }
                    _player.Draw(_spriteBatch);
                    _enemyManager.DrawEnemies(_spriteBatch);

                    //debug shit
                    if (_debug)
                    {
                        _spriteBatch.DrawString(_arial25, "Debug : ON", new Vector2(50, 100), Color.Blue, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }
                    if (!_debug)
                    {
                        _spriteBatch.DrawString(_arial25, "Debug : OFF", new Vector2(50, 100), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }

                    _spriteBatch.End();

                    DrawDebug(_spriteBatch);

                    break;
                // Options/pause menu
                case GameStates.Options:
                    foreach (Button b in _optionButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    _spriteBatch.End();
                    break;
                // Game over
                case GameStates.GameOver:
                    foreach (Button b in _gameOverButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    _spriteBatch.End();
                    break;
            }

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
                    System.Diagnostics.Debug.WriteLine("Change State ==> Level Select");
                    _gameState = GameStates.LevelSelect;

                    _gameState = GameStates.Game; //Place this line and code below it somewhere else once you are ready to work on level select!
                    NextLevel("../../../../Level2.level");
                    //MediaPlayer.Stop();
                    //MediaPlayer.Play(_moonlightSonata);
                    //MediaPlayer.IsRepeating = true;
                    break;
                // If its the exit game button
                case "ExitGameButton":
                    System.Diagnostics.Debug.WriteLine("ChangeState ==> QuitGame");
                    Exit();
                    break;

                case "ResumeGameButton":
                    _gameState = GameStates.Game;
                    MediaPlayer.Resume();
                    break;

                case "MenuButton":
                    _gameState = GameStates.Menu;
                    //MediaPlayer.Stop();
                    //MediaPlayer.Play(_appassionata);
                    //MediaPlayer.IsRepeating = true;
                    break;
            }
        }

        /// <summary>
        /// Creates the next level.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void NextLevel(string fileName)
        {
            _enemyManager = new EnemyManager(3, 3, 1);
            _enemyManager.LoadContent(Content);
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
            _enemyManager.Path.Clear();
            points = input.ReadLine().Split('|');
            foreach (string p in points)
            {
                if (!p.Equals(""))
                {
                    string[] coordinates = p.Split(",");
                    int x = (int.Parse(coordinates[0]) * _tileLength) + _marginWidth + (_tileLength / 2);
                    int y = (int.Parse(coordinates[1]) * _tileLength) + (_tileLength / 2);
                    _enemyManager.Path.Add(new Vector2((float)x, (float)y));
                }
            }

            _enemyManager.NumOfEnemies = int.Parse(input.ReadLine());
            _enemyManager.TotalWaves = int.Parse(input.ReadLine());
            _enemyManager.WaveModifier = double.Parse(input.ReadLine());

            string[] spawn;
            spawn = input.ReadLine().Split(',');
            _player = new Player((int.Parse(spawn[0]) * _tileLength) + _marginWidth, int.Parse(spawn[1]) * _tileLength, _playerTexture, 100, 100, _tileLength, 6);
        }

        /// <summary>
        /// Draws the level
        /// </summary>
        /// <param name="sb"></param>
        private void DrawLevel(SpriteBatch sb)
        {
            _tileLength = _windowSize.Y / _map.GetLength(0);

            int mapWidth = _tileLength * _map.GetLength(1);
            int MarginWidth = (_windowSize.X - mapWidth) / 2;

            //Drawing the  map
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int k = 0; k < _map.GetLength(1); k++)
                {
                    switch (_map[i, k])
                    {
                        case TileType.Wall:
                            sb.Draw(_wallFloralTexture, new Rectangle(MarginWidth + (k * _tileLength), (i * _tileLength),
                                _tileLength, _tileLength), Color.White);
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

            foreach (Vector2 p in _enemyManager.Path)
            {
                float x = p.X;
                float y = p.Y;
                Rectangle pointRect = new Rectangle((int)(x - 5), (int)(y - 5), 10, 10);
                sb.Draw(_woodFloorTexture, pointRect, Color.Blue);
            }
        }

        private void DrawDebug(SpriteBatch sb)
        {
            if (_debug)
            {
                ShapeBatch.Begin(GraphicsDevice);
                ShapeBatch.BoxOutline(_player.Position, Color.Red);
                ShapeBatch.End();
                foreach (Enemy em in _enemyManager.Enemies)
                {
                    ShapeBatch.Begin(GraphicsDevice);
                    ShapeBatch.BoxOutline(new Rectangle(em.Position.X, em.Position.Y, em.SpriteSize.X, em.SpriteSize.Y), Color.Blue);
                    ShapeBatch.End();
                    sb.Begin();
                    sb.DrawString(_arial25, $"X : {em.Position.X}, Y: {em.Position.Y}", new Vector2(em.Position.X + em.SpriteSize.X, em.Position.Y), Color.Blue);
                    sb.DrawString(_arial25, $"Speed: {em.Speed}", new Vector2(em.Position.X + em.SpriteSize.X, em.Position.Y + 25), Color.Blue);
                    sb.End();
                }
            }
        }

        public void ResolveCollisions()
        {
            // For all the enemies
            foreach (Enemy enemy in _enemyManager.Enemies)
            {
                // For all the traps
                for (int i = 0; i < _traps.Count; i++)
                {
                    // If that trap is colliding with the current enemy
                    if (_traps[i].CheckCollisions(enemy))
                    {
                        if (!enemy.SteppedOn.Contains(_traps[i]))
                        {
                            enemy.SteppedOn.Add(_traps[i]);
                            switch (_traps[i])
                            {
                                case Glue:
                                    _traps[i].DoEffect(enemy);
                                    break;

                                case Spike:
                                    _traps[i].DoEffect(enemy);
                                    Spike s = (Spike)_traps[i];
                                    if (s.Health <= 0)
                                    {
                                        _traps.RemoveAt(i);
                                        i--;
                                    }
                                    break;

                                default:
                                    enemy.TakeDamage(_traps[i].DamageAmnt);
                                    _traps.RemoveAt(i);
                                    break;
                            }
                        }
                        //break;
                    }
                    else
                    {
                        enemy.SteppedOn.Remove(_traps[i]);
                    }
                }

                if (enemy.IsSlowed && !enemy.OnGlue)
                {
                    enemy.Speed *= 2;
                    enemy.IsSlowed = false;
                }
            }

            // Check collisions with walls
            if (_debug == false)
                _player.ResolveCollisions(_wallList);
        }

        /// <summary>
        /// Place a trap based on input
        /// </summary>
        private Trap PlaceTrap()
        {
            Trap trap = null;
            if (SingleKeyPress(Keys.D2) && _player.Money >= 20)
            {
                _player.Money -= 20;
                trap = new Glue(_player.Position.X - 10, _player.Position.Y + _player.Position.Height / 6, _glueTexture, 20, 0);
            }
            if (SingleKeyPress(Keys.D1) && _player.Money >= 20)
            {
                _player.Money -= 20;
                trap = new Spike(_player.Position.X - 10, _player.Position.Y + _player.Position.Height / 6, _nailTexture, 20, 100);
            }

            return trap;
        }

        private void DebugInputs()
        {
            if (SingleKeyPress(Keys.F1))
            {
                if (_debug)
                {
                    _debug = false;
                }
                else
                {
                    _debug = true;
                }
            }

            if (_debug)
            {
                if (SingleKeyPress(Keys.F2))
                {
                    _player.Money += 100;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.F3) == true)
                {
                    _gameState = GameStates.GameOver;
                }
            }
        }

        /// <summary>
        /// Checks if a key has been pressed only this frame and not the previous
        /// </summary>
        /// <param name="key">key to check for a single press</param>
        /// <returns>True if the key was pressed only this frame</returns>
        private bool SingleKeyPress(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && _prevKbState.IsKeyUp(key);
        }
    }

    #endregion Methods
}