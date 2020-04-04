using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region player
        Player player;
        #endregion

        #region Textures
        Texture2D playerTexture,startButtonTexture,startButtonPressedTexture,exitButtonTexture,exitButtonPressedTexture,
                  pauseBackgroundTexture;
        #endregion

        #region Buttons
        Button startButton;
        Button exitButton;
        Button exitToMainMenuButton;
        #endregion
        enum GameState
        {
            MainMenu,
            GamePlay,
            EndOfGame,
        }

        GameState _currentGameState;
        private int screenCenterX;
        private bool isPausePressed = false;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            screenCenterX = graphics.PreferredBackBufferWidth / 2;
        }

        protected override void Initialize()
        {
            _currentGameState = GameState.MainMenu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("SpaceShipSmall");

            startButtonTexture = Content.Load<Texture2D>("startButton");
            startButtonPressedTexture = Content.Load<Texture2D>("startButtonPressed");
            exitButtonTexture = Content.Load<Texture2D>("exitButton");
            exitButtonPressedTexture = Content.Load<Texture2D>("exitButtonPressed");
            pauseBackgroundTexture = Content.Load<Texture2D>("pauseBackground");

            startButton = new Button(startButtonTexture, startButtonPressedTexture, new Rectangle(screenCenterX-122, 300, 244, 72));
            exitButton = new Button(exitButtonTexture, exitButtonPressedTexture, new Rectangle(screenCenterX-122, 400, 244, 72));
            exitToMainMenuButton = new Button(exitButtonTexture, exitButtonPressedTexture, new Rectangle(screenCenterX-122, 400, 244, 72));

            player = new Player(playerTexture);
            player.playerLocation = new Rectangle(screenCenterX-25, 10, 50, 50);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            switch (_currentGameState)
            {
                case GameState.MainMenu: UpdateMainMenu(gameTime); break;
                case GameState.GamePlay: UpdateGameplay(gameTime); break;
                case GameState.EndOfGame: UpdateEndOfGame(gameTime); break;
            }

            base.Update(gameTime);
        }
        void UpdateMainMenu(GameTime gameTime) {
            this.IsMouseVisible = true;
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            if (startButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                startButton.press();
            }
            else if (exitButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                exitButton.press();
            }

            if (exitButton.isPressed)
            {
                if (exitButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released) {
                    exitButton.unpress();
                    Exit();
                }
            }
            else if (startButton.isPressed)
            {
                if(startButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    startButton.unpress();
                    _currentGameState = GameState.GamePlay;
                }
            }
 
        }
        void UpdateGameplay(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isPausePressed = true;

                //TODO napisać funkcje pauzująca grę
            }

            this.IsMouseVisible = true;
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            if (exitToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                exitToMainMenuButton.press();
            }
            if (exitToMainMenuButton.isPressed) {
                if (exitToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    exitToMainMenuButton.unpress();
                    isPausePressed = false;
                    _currentGameState = GameState.MainMenu;
                }
            }
            

        }
        void UpdateEndOfGame(GameTime gameTime) { }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (_currentGameState)
            {
                case GameState.MainMenu: DrawMainMenu(gameTime); break;
                case GameState.GamePlay: DrawGameplay(gameTime); break;
                case GameState.EndOfGame: DrawEndOfGame(gameTime); break;
            }

            base.Draw(gameTime);
        }

        void DrawMainMenu(GameTime gameTime) {
            spriteBatch.Begin();
            spriteBatch.Draw(startButton.texture, startButton.location, Color.White);
            spriteBatch.Draw(exitButton.texture, exitButton.location, Color.White);
            spriteBatch.End();
        }
        void DrawGameplay(GameTime gameTime) {
            spriteBatch.Begin();
            spriteBatch.Draw(player.playerTexture, player.playerLocation, Color.White);
            spriteBatch.End();

            if (isPausePressed == true)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(pauseBackgroundTexture, new Rectangle(screenCenterX-150, 200, 300, 300), Color.White);
                spriteBatch.Draw(exitToMainMenuButton.texture, exitToMainMenuButton.location, Color.White);
                spriteBatch.End();
            }
        }
        void DrawEndOfGame(GameTime gameTime) { }
    }
}
