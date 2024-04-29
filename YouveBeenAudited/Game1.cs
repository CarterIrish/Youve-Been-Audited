using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ShapeUtils;
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
        LevelSelect,
        Game,
        Options,
        GameOver
    }

    /// <summary>
    /// Types of map tiles.
    /// </summary>
    internal enum TileType
    {
        Wall,
        Wood
    }

    /// <summary>
    /// Delegate for checking if a button has been clicked.
    /// </summary>
    /// <param name="b">The button that was clicked.</param>
    internal delegate void BtnClickedDelegate(Button b);

    /// <summary>
    /// Authors: Carter I, Chase C, Jesse M & Jack M.
    /// Class: IGME 106.
    /// Date: 4/23/2024.
    /// Purpose: Group game project.
    /// Name of game: You've Been Audited.
    /// </summary>
    public class Game1 : Game
    {
        #region Game Fields

        //Managers
        private EnemyManager _enemyManager;

        // MonoGame fields
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // Window fields
        private Point _windowCenter;

        private Point _windowSize;

        private readonly Point _ReferenceWindow;
        public readonly double _UIscaler;

        private readonly string[] _filePaths;

        // Game States
        private GameStates _gameState;

        // Player
        private Player _player;

        private Rectangle _playerHealthBar;

        // Traps
        private List<Trap> _traps;

        // Music
        private Song _moonlightSonata;

        private Song _appassionata;

        // Animation
        private double _timeCount;

        private Texture2D _playerTexture;

        // SpriteFonts
        private SpriteFont _arial25;

        // Element lists
        private List<Button> _menuButtons;

        private List<Button> _optionButtons;
        private List<Button> _gameOverButtons;
        private List<Button> _levelSelectButtons;

        // Button textures
        private Texture2D _optionsButtonTexture;

        private Texture2D _startButtonTexture;
        private Texture2D _exitButtonTexture;
        private Texture2D _resumeButtonTexture;
        private Texture2D _menuButtonTexture;

        //Menu Textures
        private Texture2D _titleTexture;

        private Texture2D _pauseTexture;

        private Texture2D _gameOverTexture;

        //Map Textures
        private Texture2D _woodFloorTexture;
        private Texture2D _pathFloorTexture;

        private Texture2D _wallFloralTexture;
        private Texture2D _grassFloorTexture;
        private Texture2D _safeTexture;
        private Texture2D _nailTexture;
        private Texture2D _glueTexture;
        private Texture2D _bombTexture;
        private Texture2D _inventoryTexture;

        // Input sources
        private MouseState _mouseState;

        private KeyboardState _prevKbState;

        // Level Information

        // Dimensions of a square tile
        private int _tileLength;

        // 2D array representing the tile types of the map
        private TileType[,] _map;

        // pixel width of the side margins
        private int _marginWidth;

        // list of walls in the map
        private List<GameObject> _wallList;

        // Safe stuff
        private int _safeHealth;
        private int _maxSafeHealth;

        private int _healthSubtractionAmt;
        private Texture2D _healthBarTexture;
        private Rectangle _safeHealthBar;

        //Debugger
        private bool _debug;

        #endregion Game Fields

        #region Pre GameLoop

        /// <summary>
        /// Creates a new game1 object
        /// </summary>
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
            _ReferenceWindow = new Point(2560, 1440);
            _UIscaler = _graphics.PreferredBackBufferWidth / (double)_ReferenceWindow.Y;

            // initialize useful window measurements
            _windowCenter = new Point(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _windowSize = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            // turns debugger on/off
            _debug = false;

            // File paths
            _filePaths = new string[]
            {
                "Content/Level1.level",
                "Content/Level2.level",
                "Content/Level3.level",
                "Content/Level4.level",
                "Content/Level5.level"
            };
        }

        /// <summary>
        /// Initializes all important pre game loop fields.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize key game fields.
            _menuButtons = new List<Button>();
            _optionButtons = new List<Button>();
            _gameOverButtons = new List<Button>();
            _levelSelectButtons = new List<Button>();
            _gameState = GameStates.Menu;
            _traps = new List<Trap>();
            _wallList = new List<GameObject>();
            _safeHealthBar = new Rectangle(_windowCenter.X - 500, 75, 1000, 50);
            base.Initialize();
        }

        /// <summary>
        /// Loads all content for the game.
        /// </summary>
        protected override void LoadContent()
        {
            // Animation Setup.
            _timeCount = 0;
            // Create the sprite batch.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sprite font.
            _arial25 = Content.Load<SpriteFont>("Arial25");

            // All asset textures.
            _playerTexture = Content.Load<Texture2D>("player_spritesheet");
            _player = new Player(999, 999, _playerTexture, 999, 999, 999, 999);
            _player.LoadContent(Content);
            _menuButtonTexture = Content.Load<Texture2D>("MenuButton");
            _startButtonTexture = Content.Load<Texture2D>("StartButton");
            _exitButtonTexture = Content.Load<Texture2D>("ExitButton");
            _optionsButtonTexture = Content.Load<Texture2D>("OptionsButton");
            _resumeButtonTexture = Content.Load<Texture2D>("ResumeButton");
            _titleTexture = Content.Load<Texture2D>("Title");
            _pauseTexture = Content.Load<Texture2D>("pause_screen");
            _gameOverTexture = Content.Load<Texture2D>("game_over_screen");
            _woodFloorTexture = Content.Load<Texture2D>("tile_wood_floor");
            _pathFloorTexture = Content.Load<Texture2D>("tile_path_floor");
            _wallFloralTexture = Content.Load<Texture2D>("tile_floral_wall");
            _grassFloorTexture = Content.Load<Texture2D>("tile_grass_large");
            _safeTexture = Content.Load<Texture2D>("safe_new");
            _nailTexture = Content.Load<Texture2D>("spikes");
            _glueTexture = Content.Load<Texture2D>("glue");
            _bombTexture = Content.Load<Texture2D>("bomb");
            _healthBarTexture = Content.Load<Texture2D>("healthBar");
            _inventoryTexture = Content.Load<Texture2D>("inventory");



            // Game music.
            _appassionata = (Content.Load<Song>("Appassionata"));
            _moonlightSonata = (Content.Load<Song>("Moonlight Sonata"));

            // Level select icons.
            Texture2D levelOneSelect = Content.Load<Texture2D>("select_level_1");
            Texture2D levelTwoSelect = Content.Load<Texture2D>("select_level_2");
            Texture2D levelThreeSelect = Content.Load<Texture2D>("select_level_3");
            Texture2D levelFourSelect = Content.Load<Texture2D>("select_level_4");
            Texture2D levelFiveSelect = Content.Load<Texture2D>("select_level_5");

            // Player the loaded music.
            MediaPlayer.Play(_appassionata);
            MediaPlayer.IsRepeating = true;

            #region Button creation

            #region Menu buttons

            // Menu start button
            Button StartButton = new Button(
                _windowCenter.X - (int)(_startButtonTexture.Width * _UIscaler) / 2,
                _windowCenter.Y + (int)(40 * _UIscaler),
                _startButtonTexture,
                "StartButton",
                Color.White,
                _UIscaler);

            _menuButtons.Add(StartButton);
            StartButton.BtnClicked += ButtonCheck;

            // MenuExit game button
            Button ExitGameButton = new Button(
                _windowCenter.X - (int)(_exitButtonTexture.Width * _UIscaler) / 2,
                StartButton.Position.Y + (int)(_exitButtonTexture.Height * _UIscaler + 40),
                _exitButtonTexture,
                "ExitGameButton",
                Color.White,
                _UIscaler);
            _menuButtons.Add(ExitGameButton);
            ExitGameButton.BtnClicked += ButtonCheck;

            #endregion Menu buttons

            #region Options menu buttons

            // resume game button
            Button ResumeGame = new Button(
                _windowCenter.X - (int)(_resumeButtonTexture.Width * _UIscaler) / 2,
                _windowCenter.Y + (int)(_resumeButtonTexture.Height * _UIscaler * .5),
                _resumeButtonTexture,
                "ResumeGameButton",
                Color.White,
                _UIscaler);
            _optionButtons.Add(ResumeGame);
            ResumeGame.BtnClicked += ButtonCheck;

            // Options exit game button
            Button optionsExit = new Button(
                _windowCenter.X + (int)(_exitButtonTexture.Width * _UIscaler),
                _windowCenter.Y + (int)(_resumeButtonTexture.Height * _UIscaler * .5),
                _exitButtonTexture,
                "ExitGameButton",
                Color.White,
                _UIscaler);
            _optionButtons.Add(optionsExit);
            optionsExit.BtnClicked += ButtonCheck;

            // Pause menu button
            Button optionsMenu = new Button
                (
                    _windowCenter.X - (int)(_menuButtonTexture.Width * _UIscaler * 2),
                    _windowCenter.Y + (int)(_resumeButtonTexture.Height * _UIscaler * .5),
                    _menuButtonTexture,
                    "MenuButton",
                    Color.White,
                    _UIscaler
                );
            _optionButtons.Add(optionsMenu);
            optionsMenu.BtnClicked += ButtonCheck;

            #endregion Options menu buttons

            #region Game over buttons

            // game over exit game.
            Button gameOverExit = new Button(
                _windowCenter.X - (int)(_exitButtonTexture.Width * _UIscaler) / 2,
                _windowCenter.Y + (int)(_exitButtonTexture.Height * _UIscaler + 150),
                _exitButtonTexture,
                "ExitGameButton",
                Color.White,
                _UIscaler);
            _gameOverButtons.Add(gameOverExit);
            gameOverExit.BtnClicked += ButtonCheck;

            // Menu button on game over state.
            Button gameOverMenu = new Button(
                _windowCenter.X - (int)(_menuButtonTexture.Width * _UIscaler) / 2,
                _windowCenter.Y + 200 - (int)(_menuButtonTexture.Height * _UIscaler),
                _menuButtonTexture,
                "MenuButton",
                Color.White,
                _UIscaler);
            _gameOverButtons.Add(gameOverMenu);
            gameOverMenu.BtnClicked += ButtonCheck;

            #endregion Game over buttons

            #region Level select screen

            // level select buttons
            Button levelSelectOne = new Button(
                _windowCenter.X - (int)(levelOneSelect.Width * _UIscaler * 3.5),
                _windowCenter.Y - (int)(levelOneSelect.Height * _UIscaler) / 2,
                levelOneSelect,
                "LevelSelectOne",
                Color.White,
                _UIscaler
                );
            _levelSelectButtons.Add(levelSelectOne);
            levelSelectOne.BtnClicked += ButtonCheck;

            Button levelSelectTwo = new Button(
                _windowCenter.X - (int)(levelTwoSelect.Width * _UIscaler * 2),
                _windowCenter.Y - (int)(levelTwoSelect.Height * _UIscaler) / 2,
                levelTwoSelect,
                "LevelSelectTwo",
                Color.White,
                _UIscaler
                );
            _levelSelectButtons.Add(levelSelectTwo);
            levelSelectTwo.BtnClicked += ButtonCheck;

            Button levelSelectThree = new Button(
                _windowCenter.X - (int)(levelThreeSelect.Width * _UIscaler) / 2,
                _windowCenter.Y - (int)(levelThreeSelect.Height * _UIscaler) / 2,
                levelThreeSelect,
                "LevelSelectThree",
                Color.White,
                _UIscaler);
            _levelSelectButtons.Add(levelSelectThree);
            levelSelectThree.BtnClicked += ButtonCheck;

            Button levelSelectFour = new Button(
                _windowCenter.X + (int)(levelFourSelect.Width * _UIscaler),
                _windowCenter.Y - (int)(levelFourSelect.Height * _UIscaler) / 2,
                levelFourSelect,
                "LevelSelectFour",
                Color.White,
                _UIscaler
                );
            _levelSelectButtons.Add(levelSelectFour);
            levelSelectFour.BtnClicked += ButtonCheck;

            Button levelSelectFive = new Button(
                _windowCenter.X + (int)(levelFiveSelect.Width * _UIscaler * 2.5),
                _windowCenter.Y - (int)(levelFiveSelect.Height * _UIscaler) / 2,
                levelFiveSelect,
                "LevelSelectFive",
                Color.White,
                _UIscaler
                );
            _levelSelectButtons.Add(levelSelectFive);
            levelSelectFive.BtnClicked += ButtonCheck;

            #endregion Level select screen

            #endregion Button creation
        }

        #endregion Pre GameLoop

        #region GameLoop

        /// <summary>
        /// Updates game logic.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        protected override void Update(GameTime gameTime)
        {
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

                // Level Select
                case GameStates.LevelSelect:
                    foreach (Button b in _levelSelectButtons)
                    {
                        b.CheckClick(_mouseState);
                    }
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

                    //bomb fuse timer updates
                    for (int i = 0; i < _traps.Count; i++)
                    {
                        if (_traps[i] is Bomb)
                        {
                            Bomb b = (Bomb)_traps[i];
                            b.UpdateTime(gameTime);

                            if (b.ExplosionTime <= 0)
                            {
                                _traps.RemoveAt(i);
                                i--;
                            }
                        }
                    }

                    _playerHealthBar = new Rectangle(_player.Position.X - (_tileLength / 2 - _player.Position.Width / 2), _player.Position.Y - 10, _playerHealthBar.Width, _tileLength / 10);

                    if (_player.Health <= 0)
                    {
                        _gameState = GameStates.GameOver;
                    }

                    _timeCount = _player.UpdateAnimation(_timeCount);

                    _enemyManager.UpdateEnemies(gameTime, this);
                    if (currentEnemies > _enemyManager.RemainingEnemies)
                    {
                        _player.Money += 80 * (currentEnemies - _enemyManager.RemainingEnemies); // Player gets money with each kill
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

        /// <summary>
        /// Draws all UI/X elements to the screen & Graphics
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
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
                    _spriteBatch.Draw(_titleTexture,
                        new Rectangle(
                            (int)(_windowCenter.X - _titleTexture.Width / 2 * _UIscaler),
                            (int)(_windowSize.Y / 100 * 2),
                            (int)(_titleTexture.Width * _UIscaler),
                            (int)(_titleTexture.Height * _UIscaler)),
                            Color.White);
                    foreach (Button b in _menuButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    _spriteBatch.End();
                    break;

                //On Level Select
                case GameStates.LevelSelect:
                    foreach (Button b in _levelSelectButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    _spriteBatch.End();
                    break;

                // Active game
                case GameStates.Game:

                    //Draw Grass
                    _spriteBatch.Draw(_grassFloorTexture, new Rectangle(0, 0, _grassFloorTexture.Width * 3 * (int)_UIscaler, _grassFloorTexture.Height * 3 * (int)_UIscaler), Color.White);
                    DrawLevel(_spriteBatch);

                    //Draw Inventory
                    _spriteBatch.Draw(_inventoryTexture, new Rectangle((int)(_windowCenter.X + (_windowSize.X * .45) - _inventoryTexture.Width / 2 * _UIscaler), (int)(_windowSize.Y / 100 * 2),
                        (int)((_inventoryTexture.Width) * _UIscaler), (int)((_inventoryTexture.Height) * _UIscaler)), Color.White);

                    // Handles Text UI
                    _spriteBatch.DrawString(_arial25, $"${_player.Money}", new Vector2(50, 50), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(_arial25, $"Wave {_enemyManager.CurrentWave}/{_enemyManager.TotalWaves}", new Vector2(50, 125), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    if (_enemyManager.RemainingEnemies == 0)
                    {
                        _spriteBatch.DrawString(_arial25, $"Next Wave:" + string.Format("{0:0.00}", 15 - _enemyManager.Timer), new Vector2(50, 180), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }
                    else
                    {
                        _spriteBatch.DrawString(_arial25, $"Enemies Left: {_enemyManager.RemainingEnemies}", new Vector2(50, 175), Color.Red, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }

                    // Draws safe
                    Vector2 safePos = _enemyManager.Path[_enemyManager.Path.Count - 1];
                    _spriteBatch.Draw(_safeTexture, new Rectangle((int)safePos.X - _tileLength / 2, (int)safePos.Y - _tileLength / 2, _tileLength, _tileLength), Color.White);

                    // Draws traps
                    foreach (Trap trap in _traps)
                    {
                        _spriteBatch.End();
                        if (trap is Bomb)
                        {
                            Bomb b = (Bomb)trap;
                            if (b.IsExploding)
                            {
                                ShapeBatch.Begin(GraphicsDevice);
                                ShapeBatch.Circle(b.Position.Center.ToVector2(), (float)(trap.Position.Height * .63), Color.OrangeRed);
                                ShapeBatch.End();
                            }
                        }
                        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null);
                        trap.Draw(_spriteBatch);
                    }
                    _player.Draw(_spriteBatch);
                    _enemyManager.DrawEnemies(_spriteBatch);

                    // Draws Safe Health Bar
                    _spriteBatch.Draw(_healthBarTexture, new Rectangle(_windowCenter.X - 510, _windowSize.Y - _tileLength, 1020, _tileLength), Color.Black);
                    _spriteBatch.Draw(_healthBarTexture, new Rectangle(_windowCenter.X - 500, _windowSize.Y - _tileLength + 10, 1000, _tileLength - 20), Color.Red);
                    _spriteBatch.Draw(_healthBarTexture, _safeHealthBar, Color.Green);
                    _spriteBatch.DrawString(_arial25, $"{_safeHealth}/{_maxSafeHealth}", new Vector2(_safeHealthBar.X + 10, _safeHealthBar.Y + (_tileLength / 15)), Color.Black, 0, Vector2.Zero, _tileLength / 40, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(_arial25, $"{_safeHealth}/{_maxSafeHealth}", new Vector2(_safeHealthBar.X + 10, _safeHealthBar.Y), Color.White, 0, Vector2.Zero, _tileLength / 40, SpriteEffects.None, 0);



                    // Draws Player Health Bar
                    _spriteBatch.Draw(_healthBarTexture, new Rectangle(_player.Position.X - (_tileLength / 2 - _player.Position.Width / 2) - 5, _player.Position.Y - 15, _tileLength + 10, _tileLength / 10 + 10), Color.Black);
                    _spriteBatch.Draw(_healthBarTexture, new Rectangle(_player.Position.X - (_tileLength / 2 - _player.Position.Width / 2), _player.Position.Y - 10, _tileLength, _tileLength / 10), Color.Red);
                    _spriteBatch.Draw(_healthBarTexture, _playerHealthBar, Color.Green);

                    _spriteBatch.End();
                    DrawDebug(_spriteBatch);

                    break;
                // Options/pause menu
                case GameStates.Options:
                    _spriteBatch.Draw(_pauseTexture, new Rectangle((int)(_windowCenter.X - _pauseTexture.Width / 2 * _UIscaler), (int)(_windowSize.Y / 100 * 2),
                        (int)((_pauseTexture.Width) * _UIscaler), (int)((_pauseTexture.Height) * _UIscaler)), Color.White);
                    foreach (Button b in _optionButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    _spriteBatch.End();
                    break;
                // Game over
                case GameStates.GameOver:
                    _spriteBatch.Draw(_gameOverTexture, new Rectangle((int)(_windowCenter.X - _gameOverTexture.Width / 2 * _UIscaler), (int)(_windowSize.Y / 100 * 2),
                        (int)((_gameOverTexture.Width) * _UIscaler), (int)((_gameOverTexture.Height) * _UIscaler)), Color.White);
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

        /// <summary>
        /// Checks button states & performs actions according to the button.
        /// </summary>
        /// <param name="b">The button to check.</param>
        private void ButtonCheck(Button b)
        {
            switch (b.Name)
            {
                // If its the start button
                case "StartButton":
                    System.Diagnostics.Debug.WriteLine("Change State ==> Level Select");
                    _gameState = GameStates.LevelSelect;

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
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_appassionata);
                    MediaPlayer.IsRepeating = true;
                    break;

                case "LevelSelectOne":
                    NextLevel(_filePaths[0]);
                    _gameState = GameStates.Game;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_moonlightSonata);
                    MediaPlayer.IsRepeating = true;
                    break;

                case "LevelSelectTwo":
                    NextLevel(_filePaths[1]);
                    _gameState = GameStates.Game;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_moonlightSonata);
                    MediaPlayer.IsRepeating = true;
                    break;

                case "LevelSelectThree":
                    NextLevel(_filePaths[2]);
                    _gameState = GameStates.Game;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_moonlightSonata);
                    MediaPlayer.IsRepeating = true;
                    break;

                case "LevelSelectFour":
                    NextLevel(_filePaths[3]);
                    _gameState = GameStates.Game;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_moonlightSonata);
                    MediaPlayer.IsRepeating = true;
                    break;

                case "LevelSelectFive":
                    NextLevel(_filePaths[4]);
                    _gameState = GameStates.Game;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_moonlightSonata);
                    MediaPlayer.IsRepeating = true;
                    break;
            }
        }

        /// <summary>
        /// Makes the safe lose health
        /// </summary>
        public void TakeSafeDamage()
        {
            _safeHealth -= 100;
            if (_safeHealth <= 0)
            {
                GameOver();
            }
            _safeHealthBar.Width -= _healthSubtractionAmt;
        }

        /// <summary>
        /// Creates/Loads next level.
        /// </summary>
        /// <param name="fileName">The file path of .Level file</param>
        private void NextLevel(string fileName)
        {
            _wallList.Clear();
            _traps.Clear();
            _enemyManager = new EnemyManager(3, 3, 1);
            _enemyManager.LoadContent(Content);
            ReadFile(fileName);
            _enemyManager.TileHeight = _tileLength;
            _playerHealthBar = new Rectangle(_player.Position.X - (_tileLength / 2 - _player.Position.Width / 2), _player.Position.Y - 10, _tileLength, _tileLength / 10);
            _safeHealthBar = new Rectangle(_windowCenter.X - 500, _windowSize.Y - _tileLength + 10, 1000, _tileLength - 20);
            _maxSafeHealth = 100 + ((_enemyManager.NumOfEnemies * _enemyManager.TotalWaves) / 5) * 100;
            _safeHealth = _maxSafeHealth;
            _healthSubtractionAmt = 1000 / (1 + ((_enemyManager.NumOfEnemies * _enemyManager.TotalWaves) / 5));
        }

        /// <summary>
        /// Reads .Level file info into the game.
        /// </summary>
        /// <param name="fileName">File path for .Level file</param>
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
            _player = new Player((int.Parse(spawn[0]) * _tileLength) + _marginWidth, int.Parse(spawn[1]) * _tileLength, _playerTexture, 500, 300, _tileLength, 6);
        }

        /// <summary>
        /// Draws level to screen.
        /// </summary>
        /// <param name="sb">SpriteBatch object.</param>
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
            foreach (Vector2 point in _enemyManager.Path)
            {
                sb.Draw(_pathFloorTexture, new Rectangle((int)point.X - _tileLength / 2, (int)point.Y - _tileLength / 2, _tileLength, _tileLength), Color.White);
            }
        }

        /// <summary>
        /// Draws debug overlay to screen.
        /// </summary>
        /// <param name="sb">SpriteBatch object.</param>
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
                    ShapeBatch.BoxOutline(em.Position, Color.Blue);
                    ShapeBatch.End();
                    sb.Begin();
                    sb.DrawString(_arial25, $"X : {em.Position.X}, Y: {em.Position.Y}", new Vector2(em.Position.X + em.SpriteSize.X, em.Position.Y), Color.Blue);
                    sb.DrawString(_arial25, $"Speed: {em.Speed}", new Vector2(em.Position.X + em.SpriteSize.X, em.Position.Y + 25), Color.Blue);
                    sb.End();
                }

                sb.Begin();
                sb.DrawString(_arial25, $"Health: {_player.Health}", new Vector2(_player.Position.X + _player.SpriteSize.X, _player.Position.Y), Color.Red);
                sb.DrawString(_arial25, $"Speed: {_player.Speed}", new Vector2(_player.Position.X + _player.SpriteSize.X, _player.Position.Y + 25), Color.Red);
                foreach (Vector2 p in _enemyManager.Path)
                {
                    float x = p.X;
                    float y = p.Y;
                    Rectangle pointRect = new Rectangle((int)(x - 5), (int)(y - 5), 10, 10);
                    sb.Draw(_woodFloorTexture, pointRect, Color.Blue);
                }
                sb.End();
            }
        }

        /// <summary>
        /// Resolves all collisions in the game.
        /// </summary>
        public void ResolveCollisions()
        {
            for (int i = 0; i < _traps.Count; i++)
            {
                if (_traps[i].CheckCollisions(_player))
                {
                    if (!_player.SteppedOn.Contains(_traps[i]))
                    {
                        switch (_traps[i])
                        {
                            case Glue:
                                _traps[i].DoEffect(_player);
                                _player.SteppedOn.Add(_traps[i]);
                                break;

                            case Spike:
                                if (_traps[i].IsActive)
                                {
                                    _traps[i].DoEffect(_player);
                                    _playerHealthBar.Width -= _tileLength / 5; // Adjusts player health bar
                                    Spike s = (Spike)_traps[i];
                                    _player.SteppedOn.Add(_traps[i]);
                                    if (s.Health <= 0)
                                    {
                                        _traps.RemoveAt(i);
                                        i--;
                                    }
                                }
                                break;

                            case Bomb:
                                Bomb b = (Bomb)_traps[i];
                                if (b.IsExploding)
                                {
                                    _playerHealthBar.Width -= (int)(_tileLength * (2.0 / 5)); // Adjusts player health bar
                                    _player.SteppedOn.Add(_traps[i]);
                                }
                                _traps[i].DoEffect(_player);
                                break;

                            default:
                                _player.TakeDamage(_traps[i].DamageAmnt);
                                _player.SteppedOn.Add(_traps[i]);
                                _traps.RemoveAt(i);
                                break;
                        }
                    }
                }
                else if (_traps[i] is Spike && !_traps[i].IsActive)
                {
                    _traps[i].IsActive = true;
                    _player.SteppedOn.Remove(_traps[i]);
                }
                else
                {
                    _player.SteppedOn.Remove(_traps[i]);
                }
            }
            if (_player.IsSlowed && !_player.OnGlue)
            {
                _player.Speed *= 2;
                _player.IsSlowed = false;
            }

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
                            switch (_traps[i])
                            {
                                case Glue:
                                    _traps[i].DoEffect(enemy);
                                    enemy.SteppedOn.Add(_traps[i]);
                                    break;

                                case Spike:
                                    _traps[i].DoEffect(enemy);
                                    Spike s = (Spike)_traps[i];
                                    enemy.SteppedOn.Add(_traps[i]);
                                    if (s.Health <= 0)
                                    {
                                        _traps.RemoveAt(i);
                                        i--;
                                    }
                                    break;

                                case Bomb:
                                    Bomb b = (Bomb)_traps[i];
                                    if (b.IsExploding)
                                    {
                                        enemy.SteppedOn.Add(_traps[i]);
                                    }
                                    _traps[i].DoEffect(enemy);
                                    break;

                                default:
                                    enemy.TakeDamage(_traps[i].DamageAmnt);
                                    enemy.SteppedOn.Add(_traps[i]);
                                    _traps.RemoveAt(i);
                                    break;
                            }
                        }
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
        /// Places a trap based off of user input.
        /// </summary>
        /// <returns>The trap placed.</returns>
        private Trap PlaceTrap()
        {
            bool onTrap = false;
            foreach (Trap t in _traps)
            {
                if (new Rectangle(_player.Position.X - 10, _player.Position.Y + _player.Position.Height / 6, _nailTexture.Width, _nailTexture.Height).Intersects(t.Position))
                {
                    onTrap = true;
                }
            }
            if (!onTrap)
            {
                Trap trap = null;
                if (SingleKeyPress(Keys.K) && _player.Money >= 30)
                {
                    _player.Money -= 30;
                    trap = new Glue(_player.Position.X - 10, _player.Position.Y + _player.Position.Height / 6, _glueTexture, 30, 0, _tileLength);
                }
                else if (SingleKeyPress(Keys.J) && _player.Money >= 50)
                {
                    _player.Money -= 50;
                    trap = new Spike(_player.Position.X - 10, _player.Position.Y + _player.Position.Height / 6, _nailTexture, 50, 100, _tileLength);
                    trap.IsActive = false;
                }
                else if (SingleKeyPress(Keys.L) && _player.Money >= 75)
                {
                    _player.Money -= 75;
                    trap = new Bomb(_player.Position.X - 10, _player.Position.Y + _player.Position.Height / 6, _bombTexture, 75, 200, _tileLength);
                    Bomb bomb = (Bomb)trap;
                }

                if (trap != null)
                {
                    trap.ResolveCollisions(_wallList);
                }

                return trap;
            }
            return null;
        }

        /// <summary>
        /// Checks which user inputs are used when in debug mode.
        /// </summary>
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
        /// Checks if the key pressed was a single input or repeated one.
        /// </summary>
        /// <param name="key">The key input.</param>
        /// <returns>True if single key press, false otherwise.</returns>
        private bool SingleKeyPress(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && _prevKbState.IsKeyUp(key);
        }
    }

    #endregion Methods
}