using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;

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

        // Monogame fields
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // Game States
        private GameStates _gameState;

        // Player
        private Player _player;

        private Texture2D _playerTexture;

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

            #region Button creation

            // Menu start button
            Button StartButton = new Button(45, 45, _playerTexture, "StartButton");
            _menuButtons.Add(StartButton);
            StartButton.BtnClicked += ButtonCheck;

            // Exit game button
            Button ExitGameButton = new Button(110, 45, _playerTexture, "ExitGameButton");
            _menuButtons.Add(ExitGameButton);
            ExitGameButton.BtnClicked += ButtonCheck;

            #endregion Button creation
        }

        #endregion Pre GameLoop

        #region GameLoop

        protected override void Update(GameTime gameTime)
        {
            // TODO: remove this statement after menu UI functional
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
            {
                Exit();
            }

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
                    _player.Move();
                    break;
                // Options screen / paused
                case GameStates.Options:
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
                    foreach (Button b in _menuButtons)
                    {
                        b.Draw(_spriteBatch, Color.Green);
                    }
                    break;
                // Active game
                case GameStates.Game:
                    _player.Draw(_spriteBatch);
                    break;
                // Options/pause menu
                case GameStates.Options:
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
            }
        }

        #endregion Methods
    }
}