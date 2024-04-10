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

    internal delegate void BtnClickedDelegate(Button b);

    internal delegate void EnemyAtGoal();

    /// <summary>
    /// Authors: Carter I, Chase C, Jesse M & Jack M.
    /// Class: IGME 106.
    /// Date: 2/25/2024.
    /// Purpose: Group game project.
    /// Name: You've Been Audited.
    /// </summary>
    public class Game1 : Game
    {
        #region Key Game Fields

        //Managers
        private EnemyManager enemyManager;

        // Monogame fields
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // Window center
        private Point _windowCenter;

        // Game States
        private GameStates _gameState;

        // Player
        private Player _player;

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
        private List<Button> _masterButtonList;

        // Button textures
        private Texture2D _optionsButtonTexture;

        private Texture2D _startButtonTexture;
        private Texture2D _exitButtonTexture;
        private Texture2D _resumeButtonTexture;

        //Title Textures
        private Texture2D _titleTexture;

        // Input sources
        private MouseState _mouseState;

        private KeyboardState _prevKbState;

        #endregion Key Game Fields

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
        }

        protected override void Initialize()
        {
            // Initialize key game fields.
            _menuButtons = new List<Button>();
            _gameButtons = new List<Button>();
            _optionButtons = new List<Button>();
            _gameButtons = new List<Button>();
            _gameState = GameStates.Menu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _playerTexture = this.Content.Load<Texture2D>("player_spritesheet");
            _player = new Player(50, 50, _playerTexture, 100, 100);
            _arial25 = Content.Load<SpriteFont>("Arial25");
            _windowCenter = new Point(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            _startButtonTexture = Content.Load<Texture2D>("StartButton");
            _exitButtonTexture = Content.Load<Texture2D>("ExitButton");
            _optionsButtonTexture = Content.Load<Texture2D>("OptionsButton");
            _resumeButtonTexture = Content.Load<Texture2D>("ResumeButton");
            _titleTexture = Content.Load<Texture2D>("Title");
            _player.LoadContent(Content);

            
            //Animation Setup
            _timeCount = 0;

            #region Button creation

            // Menu start button
            Button StartButton = new Button(_windowCenter.X - (_startButtonTexture.Width * 5) / 2, _windowCenter.Y - (_startButtonTexture.Height * 5) - 10, _startButtonTexture, "StartButton", Color.White);
            _menuButtons.Add(StartButton);
            StartButton.BtnClicked += ButtonCheck;

            // MenuExit game button
            Button ExitGameButton = new Button(_windowCenter.X - (_exitButtonTexture.Width * 5) / 2, _windowCenter.Y + 10, _exitButtonTexture, "ExitGameButton", Color.White);
            _menuButtons.Add(ExitGameButton);
            ExitGameButton.BtnClicked += ButtonCheck;

            // resume game button
            Button ResumeGame = new Button(_windowCenter.X - (_resumeButtonTexture.Width * 5) / 2, _windowCenter.Y - (_resumeButtonTexture.Height * 5) - 10, _resumeButtonTexture, "ResumeGameButton", Color.White);
            _optionButtons.Add(ResumeGame);
            ResumeGame.BtnClicked += ButtonCheck;

            // Options exit game button
            Button optionsExit = new Button(_windowCenter.X - (_exitButtonTexture.Width * 5) / 2, _windowCenter.Y + 10, _exitButtonTexture, "ExitGameButton", Color.White);
            _optionButtons.Add(optionsExit);
            optionsExit.BtnClicked += ButtonCheck;

            #endregion Button creation
        }

        #endregion Pre GameLoop

        #region GameLoop

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
                    _timeCount += gameTime.ElapsedGameTime.TotalSeconds;
                    _player.Update(gameTime);
                    _timeCount = _player.UpdateAnimation(_timeCount);
                    enemyManager.UpdateEnemies(gameTime);
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
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>Draws the game elements to screen.</summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SaddleBrown);

            // Start the sprite batch for drawing all elements to screen
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
            // Draw according to game state
            switch (_gameState)
            {
                // On menu
                case GameStates.Menu:
                    foreach (Button b in _menuButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    _spriteBatch.Draw(_titleTexture, 
                        new Rectangle(new Point(_windowCenter.X - 400, _windowCenter.Y - 650), new Point(800, 800)),
                        Color.White);
                    break;
                // Active game
                case GameStates.Game:
                    _spriteBatch.DrawString(_arial25, "GameState: Escape to enter options", new Vector2(_windowCenter.X - 13 * 25, _windowCenter.Y - 25), Color.Red);
                    _player.Draw(_spriteBatch);
                    _spriteBatch.DrawString(_arial25, $"${_player.Money}", new Vector2(50, 50), Color.DarkGreen);
                    enemyManager.DrawEnemies(_spriteBatch);
                    break;
                // Options/pause menu
                case GameStates.Options:
                    _spriteBatch.DrawString(_arial25, "OptionState", new Vector2(_windowCenter.X - 5 * 25, _windowCenter.Y - 25), Color.Red);
                    foreach (Button b in _optionButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    break;
                // Game over
                case GameStates.GameOver:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void GameOver()
        {
            _gameState = GameStates.GameOver;
        }

        #endregion GameLoop

        #region Methods

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
                    NextLevel("../../../../testingLevel.level");
                    break;
                // If its the exit game button
                case "ExitGameButton":
                    System.Diagnostics.Debug.WriteLine("ChangeState ==> QuitGame");
                    Exit();
                    break;

                case "ResumeGameButton":
                    _gameState = GameStates.Game;
                    break;
            }
        }

        private void NextLevel(string fileName)
        {
            enemyManager = new EnemyManager(3);
            enemyManager.LoadContent(Content);
            ReadFile(fileName);
            enemyManager.CreateEnemies();
        }

        /// <summary>
        /// Reads a file into the game
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadFile(string fileName)
        {
            StreamReader input = new StreamReader(fileName);
            string[] dimensions;
            int height;
            int width;

            dimensions = input.ReadLine().Split(",");
            width = int.Parse(dimensions[0]);
            height = int.Parse(dimensions[1]);

            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    switch (input.Read())
                    {
                        default:
                            break;
                    }
                }
                input.ReadLine();
            }

            //adds the points to the enemy path List
            string[] points;
            enemyManager._Path.Clear();
            points = input.ReadToEnd().Split('|');
            foreach (string p in points)
            {
                if (!p.Equals(""))
                {
                    string[] coordinates = p.Split(",");
                    enemyManager._Path.Add(new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1])));
                }
            }

            #endregion Methods
        }
    }
}