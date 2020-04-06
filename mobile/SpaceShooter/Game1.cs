using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.IO;
using System;
using Android.Views;


namespace SpaceShooter
{
    enum EnemyWave
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
    }
    public class Game1 : Game
    {
        enum GameState
        {
            MainMenu,
            GamePlay,
            EndOfGame,
        }

        enum GameplayState
        {
            Paused,
            Unpaused,
        }

        #region MonoGame/Android structure
        Window w1;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TouchCollection touchCollection;
        #endregion

        #region Textures
        SpriteFont Font;
        Texture2D startButtonTexture, exitButtonTexture, startButtonPressedTexture, exitButtonPressedTexture, blankButtonTexture, spaceShipTexture,
            enemy1Texture, enemy2Texture, missileTexture, pauseButtonTexture, pauseButtonPressedTexture, resumeButtonTexture, resumeButtonPressedTexture, pauseBackgroundTexture, hearthTexture,
            restartButtonTexture, restartButtonPressedTexture,
            background1Texture, background2Texture;
        #endregion

        #region player
        Rectangle playerLocation;
        Player player;
        int fireRate = 250; // lower = faster, lowest = 32
        int missileSpeed = 20; //higher = faster
        int enemySpeed = 2;
        double nextBlinkTime = 0;
        int hearthBlinkLeft = 0;
        bool shooting = false;
        #endregion

        #region Resolution scaling
        float screenWidth, screenHeight, scaleX, scaleY;
        int virtualWidth, virtualHeight;
        Matrix matrix;
        #endregion

        #region controls
        List<Button> mainMenuButtons = new List<Button>();
        List<Missile> missiles = new List<Missile>();
        List<Enemy> enemies = new List<Enemy>();
        Button pauseButton;
        List<Button> pauseMenuButtons = new List<Button>();
        List<Button> endOfGameButtons = new List<Button>();
        #endregion

        #region states
        GameState _gameState;
        GameplayState _gameplayState;
        #endregion

        #region scores
        int actualScore = 0;
        int highScore;
        #endregion

        GameplayBackground gameplayBackground1, gameplayBackground2;
        MainMenuBackground mainMenuBackground;
        List<Texture2D> enemyTextures = new List<Texture2D>();

        private Random r = new Random();
        bool newHighScore = false;
        bool newHighScoreVisible;
        int startTime = 0;
        EnemyWave actualWave;

        public Game1(Window w1)
        {
            this.w1 = w1;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;


            screenWidth = Window.ClientBounds.Width;
            screenHeight = Window.ClientBounds.Height;
            virtualHeight = 2340;
            virtualWidth = 1080;

            scaleX = screenWidth / virtualWidth;
            scaleY = screenHeight / virtualHeight;

            matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            graphics.PreferredBackBufferWidth = (int)screenWidth;
            graphics.PreferredBackBufferHeight = (int)screenHeight;
        }
        protected override void Initialize()
        {
            playerLocation = new Rectangle(390, 2040, 300, 300);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //load content
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background1Texture = Content.Load<Texture2D>("background1");
            background2Texture = Content.Load<Texture2D>("background2");
            blankButtonTexture = Content.Load<Texture2D>("blankButton");
            enemy1Texture = Content.Load<Texture2D>("enemy1");
            enemy2Texture = Content.Load<Texture2D>("enemy2");
            exitButtonTexture = Content.Load<Texture2D>("exitButton");
            exitButtonPressedTexture = Content.Load<Texture2D>("exitButtonPressed");
            hearthTexture = Content.Load<Texture2D>("hearth");
            missileTexture = Content.Load<Texture2D>("missile");
            pauseBackgroundTexture = Content.Load<Texture2D>("pauseBackground");
            pauseButtonTexture = Content.Load<Texture2D>("pauseButton");
            pauseButtonPressedTexture = Content.Load<Texture2D>("pauseButtonPressed");
            restartButtonTexture = Content.Load<Texture2D>("restartButton");
            restartButtonPressedTexture = Content.Load<Texture2D>("restartButtonPressed");
            resumeButtonTexture = Content.Load<Texture2D>("resumeButton");
            resumeButtonPressedTexture = Content.Load<Texture2D>("resumeButtonPressed");
            spaceShipTexture = Content.Load<Texture2D>("spaceShip");
            startButtonTexture = Content.Load<Texture2D>("startButton");
            startButtonPressedTexture = Content.Load<Texture2D>("startButtonPressed");

            Font = Content.Load<SpriteFont>("pixel_f70");

            //declare mainMenuButtons and background
            mainMenuButtons.Add(new Button("start", startButtonTexture, startButtonPressedTexture, new Rectangle(100, 1100, 880, 260)));
            mainMenuButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(216, 1400, 648, 192)));
            mainMenuBackground = new MainMenuBackground(background1Texture);

            //gameplay pause button and background
            pauseButton = new Button("pause", pauseButtonTexture, pauseButtonPressedTexture, new Rectangle(870, 18, 192, 192));
            gameplayBackground1 = new GameplayBackground(background1Texture, new Rectangle(0, 0, 1080, 2340));
            gameplayBackground2 = new GameplayBackground(background2Texture, new Rectangle(0, -2340, 1080, 2340));

            //declare pauseMenuButtons
            pauseMenuButtons.Add(new Button("resume", resumeButtonTexture, resumeButtonPressedTexture, new Rectangle(200, 1070, 680, 201)));
            pauseMenuButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(200, 1321, 680, 201)));

            endOfGameButtons.Add(new Button("restart", restartButtonTexture, restartButtonPressedTexture, new Rectangle(100, 1100, 880, 260)));
            endOfGameButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(216, 1400, 648, 192)));

            //enemyTextures list
            enemyTextures.Add(enemy1Texture);
            enemyTextures.Add(enemy2Texture);
            enemyTextures.Add(enemy1Texture);
            enemyTextures.Add(enemy2Texture);
            enemyTextures.Add(enemy1Texture);

            //declare player
            player = new Player(spaceShipTexture, hearthTexture);

            LoadSettings();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int uiOptions = (int)w1.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            if (w1.DecorView.SystemUiVisibility != (StatusBarVisibility)uiOptions) w1.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;

            switch (_gameState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    UpdateEndOfGame(gameTime);
                    break;
            }
        }

        private void UpdateMainMenu(GameTime deltaTime)
        {
            mainMenuBackground.Update();

            touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                foreach (Button b in mainMenuButtons)
                {
                    if (b.isPressed)
                    {
                        if (tl.State == TouchLocationState.Released && b.location.Contains(touchPosition))
                        {
                            switch (b.Name)
                            {
                                case "start":
                                    b.unpress();
                                    ResetGame(deltaTime);
                                    _gameState = GameState.GamePlay;
                                    _gameplayState = GameplayState.Unpaused;
                                    break;
                                case "exit":
                                    b.unpress();
                                    Game.Activity.MoveTaskToBack(true);
                                    break;
                            }
                        }
                        else if (tl.State == TouchLocationState.Released) b.unpress();
                    }
                    else
                    {
                        if (b.location.Contains(touchPosition) && tl.State == TouchLocationState.Pressed)
                        {
                            b.press();
                        }
                    }
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Game.Activity.MoveTaskToBack(true);
            }
        }

        private void UpdateGameplay(GameTime deltaTime)
        {
            //game unpaused
            if (_gameplayState == GameplayState.Unpaused)
            {
                //update actual wave
                if (actualScore > 145 && actualWave == EnemyWave.First)
                {
                    actualWave = EnemyWave.Second;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 210;
                }
                else if (actualScore > 295 && actualWave == EnemyWave.Second)
                {
                    actualWave = EnemyWave.Third;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 180;
                }
                else if (actualScore > 445 && actualWave == EnemyWave.Third)
                {
                    actualWave = EnemyWave.Fourth;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 150;
                }
                else if (actualScore > 595 && actualWave == EnemyWave.Fourth)
                {
                    actualWave = EnemyWave.Fifth;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 120;
                }

                if (deltaTime.TotalGameTime.TotalSeconds == startTime)
                {
                    missiles.Clear();
                    enemies.Clear();
                }

                //update background
                gameplayBackground1.Update();
                gameplayBackground2.Update();

                //creating missiles
                if (shooting)
                {
                    if (missiles.Count == 0) missiles.Add(new Missile(missileTexture, playerLocation.X, missileSpeed));
                    else if (missiles[missiles.Count - 1].isNextReady(fireRate)) missiles.Add(new Missile(missileTexture, playerLocation.X, missileSpeed));
                    foreach (Missile m in missiles) m.Update();
                }
                else
                {
                    if (deltaTime.TotalGameTime.TotalSeconds > startTime) shooting = true;
                }

                //creating enemies
                if (deltaTime.TotalGameTime.TotalSeconds > startTime)
                {
                    if (enemies.Count == 0) enemies.Add(new Enemy(enemyTextures, r.Next(4), enemySpeed, actualWave));
                        else if (enemies[enemies.Count - 1].isNextReady()) enemies.Add(new Enemy(enemyTextures, r.Next(4), enemySpeed, actualWave));
                }

                //updating enemies position and player health
                foreach (Enemy e in enemies)
                {
                    if (e.location.Y >= 2340 && !e.isDestroyed)
                    {
                        e.isDestroyed = true;
                        hearthBlinkLeft = 10;
                        if (player.Hit())
                        {
                            if (actualScore > highScore)
                            {
                                highScore = actualScore;
                                newHighScore = true;
                                SaveSettings();
                            }
                            else newHighScore = false;
                            _gameState = GameState.EndOfGame;
                        }
                    }
                    e.Update();
                }

                //enemy-missile collision handling
                foreach (Missile m in missiles)
                {
                    foreach (Enemy e in enemies)
                    {
                        if (e.location.Contains(new Point(m.location.X + 8, m.location.Y + 135)) && !(e.isDestroyed || m.isDestroyed))
                        {
                            if (e.Hit(25))
                            {
                                if (actualWave == EnemyWave.First)
                                    actualScore++;
                                else if (actualWave == EnemyWave.Second)
                                    actualScore += 2;
                                else if (actualWave == EnemyWave.Third)
                                    actualScore += 3;
                                else if (actualWave == EnemyWave.Fourth)
                                    actualScore += 4;
                                else if (actualWave == EnemyWave.Fifth)
                                    actualScore += 5;

                                if (actualScore % 5 == 0) enemySpeed++;
                            }
                            m.isDestroyed = true;
                        }
                    }
                }

                //hearths blinking
                if (hearthBlinkLeft > 0)
                {
                    if (deltaTime.TotalGameTime.TotalMilliseconds >= nextBlinkTime)
                    {
                        player.hearthVisible = !player.hearthVisible;

                        nextBlinkTime = deltaTime.TotalGameTime.TotalMilliseconds + 100;
                        hearthBlinkLeft--;
                    }
                }

                //controls / touch handling
                touchCollection = TouchPanel.GetState();

                foreach (TouchLocation tl in touchCollection)
                {
                    Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                    Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                    if (pauseButton.isPressed)
                    {
                        if (tl.State == TouchLocationState.Released && pauseButton.location.Contains(touchPosition))
                        {
                            pauseButton.unpress();
                            _gameplayState = GameplayState.Paused;
                        }
                        else if (tl.State == TouchLocationState.Released) pauseButton.unpress();
                    }
                    else
                    {
                        if (pauseButton.location.Contains(touchPosition) && tl.State == TouchLocationState.Pressed)
                        {
                            pauseButton.press();
                        }
                        else
                        {
                            if (touchPosition.X < 150) playerLocation.X = 0;
                            else if (touchPosition.X > 930) playerLocation.X = 780;
                            else playerLocation.X = (int)touchPosition.X - 150;
                        }
                    }

                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    _gameplayState = GameplayState.Paused;
                }
            }
            //game paused
            else if (_gameplayState == GameplayState.Paused)
            {
                touchCollection = TouchPanel.GetState();

                foreach (TouchLocation tl in touchCollection)
                {
                    Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                    Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                    foreach (Button b in pauseMenuButtons)
                    {
                        if (b.isPressed)
                        {
                            if (tl.State == TouchLocationState.Released && b.location.Contains(touchPosition))
                            {
                                switch (b.Name)
                                {
                                    case "resume":
                                        b.unpress();
                                        _gameplayState = GameplayState.Unpaused;
                                        break;
                                    case "exit":
                                        b.unpress();
                                        _gameState = GameState.MainMenu;
                                        break;
                                }
                            }
                            else if (tl.State == TouchLocationState.Released) b.unpress();
                        }
                        else
                        {
                            if (b.location.Contains(touchPosition) && tl.State == TouchLocationState.Pressed)
                            {
                                b.press();
                            }
                        }
                    }
                }
            }
        }

        private void UpdateEndOfGame(GameTime deltaTime)
        {
            if (newHighScore)
            {
                if (deltaTime.TotalGameTime.TotalMilliseconds >= nextBlinkTime)
                {
                    newHighScoreVisible = !newHighScoreVisible;

                    nextBlinkTime = deltaTime.TotalGameTime.TotalMilliseconds + 200;
                }
            }

            touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                foreach (Button b in endOfGameButtons)
                {
                    if (b.isPressed)
                    {
                        if (tl.State == TouchLocationState.Released && b.location.Contains(touchPosition))
                        {
                            switch (b.Name)
                            {
                                case "restart":
                                    b.unpress();
                                    ResetGame(deltaTime);
                                    _gameplayState = GameplayState.Unpaused;
                                    _gameState = GameState.GamePlay;
                                    break;
                                case "exit":
                                    b.unpress();
                                    _gameState = GameState.MainMenu;
                                    break;
                            }
                        }
                        else if (tl.State == TouchLocationState.Released) b.unpress();
                    }
                    else
                    {
                        if (b.location.Contains(touchPosition) && tl.State == TouchLocationState.Pressed)
                        {
                            b.press();
                        }
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            switch (_gameState)
            {
                case GameState.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
            }
        }
        private void DrawMainMenu(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: matrix);

            spriteBatch.Draw(mainMenuBackground.texture, mainMenuBackground.location, Color.White);

            spriteBatch.DrawString(Font, "HIGHSCORE: " + highScore.ToString(), new Vector2(200, 800), Color.White);

            foreach (Button b in mainMenuButtons)
            {
                spriteBatch.Draw(b.texture, b.location, Color.White);
            }

            spriteBatch.End();
        }

        private void DrawGameplay(GameTime gametime)
        {
            spriteBatch.Begin(transformMatrix: matrix);

            //drawing background
            spriteBatch.Draw(gameplayBackground1.texture, gameplayBackground1.location, Color.White);
            spriteBatch.Draw(gameplayBackground2.texture, gameplayBackground2.location, Color.White);

            //drawing missiles
            foreach (Missile m in missiles)
            {
                if (!m.isDestroyed) spriteBatch.Draw(m.texture, m.location, Color.White);
            }

            //drawing enemies
            foreach (Enemy e in enemies)
            {
                if (!e.isDestroyed) spriteBatch.Draw(e.texture, e.location, Color.White);
            }

            //drawing player health
            if (player.hearthVisible)
            {
                for (int i = 0; i < player.health; i++)
                {
                    spriteBatch.Draw(player.hearthTexture, new Rectangle(18 + i * 50, 50, 150, 128), Color.White);
                }
            }

            //drawing score
            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(310, 18), Color.White);

            //drawing player's space ship
            spriteBatch.Draw(player.texture, playerLocation, Color.White);

            //drawing pause button when game is unpaused
            if (_gameplayState == GameplayState.Unpaused) spriteBatch.Draw(pauseButton.texture, pauseButton.location, Color.White);

            //drawing pause menu when game is paused
            if (_gameplayState == GameplayState.Paused)
            {
                spriteBatch.Draw(pauseBackgroundTexture, new Rectangle(100, 730, 880, 880), Color.White);

                foreach (Button b in pauseMenuButtons)
                {
                    spriteBatch.Draw(b.texture, b.location, Color.White);
                }
            }

            spriteBatch.End();
        }

        private void DrawEndOfGame(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: matrix);

            if (newHighScoreVisible) spriteBatch.DrawString(Font, "NEW HIGH SCORE!", new Vector2(150, 600), Color.White);

            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(310, 800), Color.White);

            foreach (Button b in endOfGameButtons)
            {
                spriteBatch.Draw(b.texture, b.location, Color.White);
            }

            spriteBatch.End();
        }

        private void ResetGame(GameTime deltaTime)
        {
            actualWave = EnemyWave.First;
            missiles.Clear();
            enemies.Clear();
            playerLocation.X = 390;
            actualScore = 0;
            player.health = 3;
            enemySpeed = 2;
            fireRate = 250;
            startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
        }

        private void SaveSettings()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "settings.bin");

            try
            {
                using (var streamWriter = new StreamWriter(filename, false))
                {
                    streamWriter.Write(highScore.ToString());
                }
            }
            catch { }
        }

        private void LoadSettings()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "settings.bin");
            try
            {
                using (var streamReader = new StreamReader(filename))
                {
                    string content = streamReader.ReadToEnd();
                    highScore = Int32.Parse(content);
                }
            }
            catch
            {
                highScore = 0;
            }
        }
    }
}