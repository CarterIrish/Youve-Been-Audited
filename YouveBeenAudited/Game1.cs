using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace YouveBeenAudited
{
    /// <summary>
    /// States of the game.
    /// </summary>
    enum GameState
    {
        Menu,
        Game,
        Options,
        GameOver
    }


    /// <summary>
    /// Authors: Carter I, Chase C, Jesse M & Jack M.
    /// Class: IGME 106.
    /// Date: 2/25/2024.
    /// Purpose: Group game project.
    /// Name: You've Been Audited.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState _gameState;
        private MouseState _mouseState;
        private List<Button> _buttonList;


        private Texture2D playerTexture;
        private Player player;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _buttonList = new List<Button>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = this.Content.Load<Texture2D>("playerStanding");
            player = new Player(50, 50, playerTexture, 100, 100);
            // Make menu ui elements
            _buttonList.Add(new Button(45, 45, playerTexture, "StartButton"));
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            switch (_gameState)
            {
                case GameState.Menu:
                    ButtonCheck();
                    break;
                case GameState.Game:
                    break;
                case GameState.Options:
                    break;
                case GameState.GameOver:
                    break;
            }

            player.Move();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
            switch (_gameState)
            {
                case GameState.Menu:
                    foreach (Button b in _buttonList)
                    {
                        b.Draw(_spriteBatch);
                    }
                    break;
                case GameState.Game:
                    GraphicsDevice.Clear(Color.Black);
                    break;
                case GameState.Options:
                    break;
                case GameState.GameOver:
                    break;
            }

            // TODO: Add your drawing code here
            player.Draw(_spriteBatch, 5);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void ButtonCheck()
        {
            foreach(Button b in _buttonList)
            {
                switch (b.Name)
                {
                    case "StartButton":
                        if (b.ButtonClick(_mouseState))
                        {
                            _gameState = GameState.Game;
                        }
                        break;
                    case "OptionsButton":
                        System.Diagnostics.Debug.WriteLine($"{b.ButtonClick(_mouseState)} || {_gameState}");
                        if(b.ButtonClick(_mouseState))
                        {
                            _gameState = GameState.Options;
                            System.Diagnostics.Debug.WriteLine($"GameState: {_gameState}");
                        }
                        break;
                    case "ExitGame":
                        if(b.ButtonClick(_mouseState))
                        {
                            Exit();
                        }
                        break;
                }

            }
        }
    }
}                                                                                 
