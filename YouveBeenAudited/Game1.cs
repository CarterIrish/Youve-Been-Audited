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
        EnemyManager enemyManager;

        // Monogame fields
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // Window center
        private Vector2 _windowCenter;

        // Game States
        private GameStates _gameState;

        // Player
        private Player _player;

        private Texture2D _playerTexture;

        // SpriteFonts
        private SpriteFont _arial25;

        // Element lists
        private List<Button> _menuButtons;

        private List<Button> _gameButtons;
        private List<Button> _optionButtons;
        private List<Button> _gameOverButtons;
        private List<Button> _masterButtonList;

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
            _playerTexture = this.Content.Load<Texture2D>("playerStanding");
            _player = new Player(50, 50, _playerTexture, 100, 100);
            _arial25 = Content.Load<SpriteFont>("Arial25");
            _windowCenter = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            _player.LoadContent(Content);
            #region Button creation

            // Menu start button
            Button StartButton = new Button(45, 60, _playerTexture, "StartButton", Color.Green);
            _menuButtons.Add(StartButton);
            StartButton.BtnClicked += ButtonCheck;

            // MenuExit game button
            Button ExitGameButton = new Button(110, 60, _playerTexture, "ExitGameButton", Color.Red);
            _menuButtons.Add(ExitGameButton);
            ExitGameButton.BtnClicked += ButtonCheck;

            // resume game button
            Button ResumeGame = new Button((int)_windowCenter.X, (int)_windowCenter.Y + 5, _playerTexture, "ResumeGameButton", Color.Green);
            _optionButtons.Add(ResumeGame);
            ResumeGame.BtnClicked += ButtonCheck;

            // Options exit game button
            Button optionsExit = new Button((int)_windowCenter.X - 120, (int)_windowCenter.Y + 5, _playerTexture, "ExitGameButton", Color.Red);
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
                    _player.Update(gameTime);
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
            GraphicsDevice.Clear(Color.CadetBlue);

            // Start the sprite batch for drawing all elements to screen
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
            // Draw according to game state
            switch (_gameState)
            {
                // On menu
                case GameStates.Menu:
                    _spriteBatch.DrawString(_arial25, "MenuState", new Vector2(_windowCenter.X - 5 * 25, _windowCenter.Y - 25), Color.Red);
                    foreach (Button b in _menuButtons)
                    {
                        b.Draw(_spriteBatch, b.Color);
                    }
                    break;
                // Active game
                case GameStates.Game:
                    _spriteBatch.DrawString(_arial25, "GameState: Escape to enter options", new Vector2(_windowCenter.X - 13 * 25, _windowCenter.Y - 25), Color.Red);
                    _player.Draw(_spriteBatch);
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