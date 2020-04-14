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
            Menu,
            GamePlay,
            EndOfGame,
        }

        enum MenuState
        {
            Main,
            Upgrades,
            Skins,
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
        Texture2D startButtonTexture, startButtonPressedTexture, exitButtonTexture, exitButtonPressedTexture, upgradesButtonTexture, upgradesButtonPressedTexture, skinsButtonTexture, skinsButtonPressedTexture,
            spaceShip1Texture, spaceShip2Texture, spaceShip3Texture, enemy1Texture, enemy2Texture, enemy3Texture, enemy4Texture, missileTexture, hearthTexture,
            pauseButtonTexture, pauseButtonPressedTexture, resumeButtonTexture, resumeButtonPressedTexture, pauseBackgroundTexture,
            restartButtonTexture, restartButtonPressedTexture, menuButtonTexture, menuButtonPressedTexture,
            background1Texture, background2Texture;
        #endregion

        #region player
        Player player;
        int fireRate = 250; // lower = faster, lowest = 32
        int missileSpeed = 45; //higher = faster default = 20
        int enemySpeed = 2;
        double nextBlinkTime = 0;
        int hearthBlinkLeft = 0;
        #endregion

        #region Resolution scaling
        float screenWidth, screenHeight, scaleX, scaleY;
        int virtualWidth, virtualHeight;
        Matrix matrix;
        #endregion

        #region controls
        List<Button> mainMenuButtons = new List<Button>();
        List<Button> upgradesMenuButtons = new List<Button>();
        List<Button> skinsMenuButtons = new List<Button>();
        List<Missile> missiles = new List<Missile>();
        List<Enemy> enemies = new List<Enemy>();
        Button pauseButton;
        List<Button> pauseMenuButtons = new List<Button>();
        List<Button> endOfGameButtons = new List<Button>();
        #endregion

        #region states
        GameState _gameState;
        MenuState _menuState;
        GameplayState _gameplayState;
        #endregion

        #region scores
        int actualScore = 0;
        int highScore;
        #endregion

        GameplayBackground gameplayBackground;
        MainMenuBackground mainMenuBackground;
        List<Texture2D> enemyTextures = new List<Texture2D>();
        List<Texture2D> shipTextures = new List<Texture2D>();

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //load content
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background1Texture = Content.Load<Texture2D>("background1");
            background2Texture = Content.Load<Texture2D>("background2");
            enemy1Texture = Content.Load<Texture2D>("enemy1");
            enemy2Texture = Content.Load<Texture2D>("enemy2");
            enemy3Texture = Content.Load<Texture2D>("enemy3");
            enemy4Texture = Content.Load<Texture2D>("enemy4");
            exitButtonTexture = Content.Load<Texture2D>("exitButton");
            exitButtonPressedTexture = Content.Load<Texture2D>("exitButtonPressed");
            hearthTexture = Content.Load<Texture2D>("hearth");
            menuButtonTexture = Content.Load<Texture2D>("menuButton");
            menuButtonPressedTexture = Content.Load<Texture2D>("menuButtonPressed");
            missileTexture = Content.Load<Texture2D>("missile");
            pauseBackgroundTexture = Content.Load<Texture2D>("pauseBackground");
            pauseButtonTexture = Content.Load<Texture2D>("pauseButton");
            pauseButtonPressedTexture = Content.Load<Texture2D>("pauseButtonPressed");
            restartButtonTexture = Content.Load<Texture2D>("restartButton");
            restartButtonPressedTexture = Content.Load<Texture2D>("restartButtonPressed");
            resumeButtonTexture = Content.Load<Texture2D>("resumeButton");
            resumeButtonPressedTexture = Content.Load<Texture2D>("resumeButtonPressed");
            skinsButtonTexture = Content.Load<Texture2D>("skinsButton");
            skinsButtonPressedTexture = Content.Load<Texture2D>("skinsButtonPressed");
            spaceShip1Texture = Content.Load<Texture2D>("spaceShip1");
            spaceShip2Texture = Content.Load<Texture2D>("spaceShip2");
            spaceShip3Texture = Content.Load<Texture2D>("spaceShip3");
            startButtonTexture = Content.Load<Texture2D>("startButton");
            startButtonPressedTexture = Content.Load<Texture2D>("startButtonPressed");
            upgradesButtonTexture = Content.Load<Texture2D>("upgradesButton");
            upgradesButtonPressedTexture = Content.Load<Texture2D>("upgradesButtonPressed");

            Font = Content.Load<SpriteFont>("pixel_f70");

            //declare mainMenuButtons and background
            mainMenuButtons.Add(new Button("start", startButtonTexture, startButtonPressedTexture, new Rectangle(100, 1000, 880, 260)));
            mainMenuButtons.Add(new Button("upgrades", upgradesButtonTexture, upgradesButtonPressedTexture, new Rectangle(188, 1300, 704, 208)));
            mainMenuButtons.Add(new Button("skins", skinsButtonTexture, skinsButtonPressedTexture, new Rectangle(188, 1548, 704, 208)));
            mainMenuButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(232, 1796, 616, 182)));
            mainMenuBackground = new MainMenuBackground(background1Texture);

            //gameplay pause button and background
            pauseButton = new Button("pause", pauseButtonTexture, pauseButtonPressedTexture, new Rectangle(870, 18, 192, 192));
            gameplayBackground = new GameplayBackground(background1Texture, background2Texture);

            //declare pauseMenuButtons
            pauseMenuButtons.Add(new Button("resume", resumeButtonTexture, resumeButtonPressedTexture, new Rectangle(200, 1070, 680, 201)));
            pauseMenuButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(200, 1321, 680, 201)));

            endOfGameButtons.Add(new Button("restart", restartButtonTexture, restartButtonPressedTexture, new Rectangle(100, 1100, 880, 260)));
            endOfGameButtons.Add(new Button("menu", menuButtonTexture, menuButtonPressedTexture, new Rectangle(216, 1400, 648, 192)));

            //player ship textures list
            shipTextures.Add(spaceShip1Texture);
            shipTextures.Add(spaceShip2Texture);
            shipTextures.Add(spaceShip3Texture);

            //enemyTextures list
            enemyTextures.Add(enemy1Texture);
            enemyTextures.Add(enemy2Texture);
            enemyTextures.Add(enemy3Texture);
            enemyTextures.Add(enemy4Texture);
            enemyTextures.Add(enemy2Texture);

            //declare player
            player = new Player(shipTextures[2], new Rectangle(390, 2040, 300, 300), hearthTexture);

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
                case GameState.Menu:
                    UpdateMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    UpdateEndOfGame(gameTime);
                    break;
            }
        }

        private void UpdateMenu(GameTime deltaTime)
        {
            mainMenuBackground.Update();

            switch (_menuState)
            {
                case MenuState.Main:
                    UpdateMainMenu(deltaTime);
                    break;
                case MenuState.Upgrades:
                    UpdateUpgradesMenu(deltaTime);
                    break;
                case MenuState.Skins:
                    UpdateSkinsMenu(deltaTime);
                    break;
            }
        }

        private void UpdateMainMenu(GameTime deltaTime)
        {
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
                                    break;
                                case "upgrades":
                                    b.unpress();
                                    _menuState = MenuState.Upgrades;
                                    break;
                                case "skins":
                                    b.unpress();
                                    _menuState = MenuState.Skins;
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

        private void UpdateUpgradesMenu(GameTime deltaTime)
        {
            touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                foreach (Button b in upgradesMenuButtons)
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
                                    break;
                                case "upgrades":
                                    _menuState = MenuState.Upgrades;
                                    break;
                                case "skins":
                                    _menuState = MenuState.Skins;
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
                _menuState = MenuState.Main;
            }
        }

        private void UpdateSkinsMenu(GameTime deltaTime)
        {
            touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                foreach (Button b in skinsMenuButtons)
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
                                    break;
                                case "upgrades":
                                    _menuState = MenuState.Upgrades;
                                    break;
                                case "skins":
                                    _menuState = MenuState.Skins;
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
                _menuState = MenuState.Main;
            }
        }

        private void UpdateGameplay(GameTime deltaTime)
        {
            //game unpaused
            if (_gameplayState == GameplayState.Unpaused)
            {
                //update actual wave
                if (actualScore > 100 && actualWave == EnemyWave.First)
                {
                    actualWave = EnemyWave.Second;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 210;
                }
                else if (actualScore > 200 && actualWave == EnemyWave.Second)
                {
                    actualWave = EnemyWave.Third;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 190;
                }
                else if (actualScore > 300 && actualWave == EnemyWave.Third)
                {
                    actualWave = EnemyWave.Fourth;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 150;
                }
                else if (actualScore > 400 && actualWave == EnemyWave.Fourth)
                {
                    actualWave = EnemyWave.Fifth;
                    startTime = (int)deltaTime.TotalGameTime.TotalSeconds + 3;
                    enemySpeed = 2;
                    fireRate = 120;
                }

                //deleting missiles
                for (int i = 0; i < missiles.Count; i++)
                {
                    if (missiles[i].isDestroyed) missiles.RemoveAt(i);
                }

                //deleting enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].isDestroyed) enemies.RemoveAt(i);
                }

                //update background
                gameplayBackground.Update();

                //creating missiles
                if (missiles.Count == 0) missiles.Add(new Missile(missileTexture, player.location.X, missileSpeed));
                else if (missiles[missiles.Count - 1].isNextReady(fireRate)) missiles.Add(new Missile(missileTexture, player.location.X, missileSpeed));
                foreach (Missile m in missiles) m.Update();

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
                        hearthBlinkLeft += 10;
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
                                actualScore++;
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
                            if (touchPosition.X < 150) player.location.X = 0;
                            else if (touchPosition.X > 930) player.location.X = 780;
                            else player.location.X = (int)touchPosition.X - 150;
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
                                        _gameState = GameState.Menu;
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
            mainMenuBackground.Update();

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
                                case "menu":
                                    b.unpress();
                                    _gameState = GameState.Menu;
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
                case GameState.Menu:
                    DrawMenu(gameTime);
                    break;
                case GameState.GamePlay:
                    DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
            }
        }
        private void DrawMenu(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: matrix);

            //drawing main menu background
            mainMenuBackground.Draw(spriteBatch);

            switch (_menuState)
            {
                case MenuState.Main:
                    DrawMainMenu(gameTime);
                    break;
                case MenuState.Upgrades:
                    DrawUpgradesMenu(gameTime);
                    break;
                case MenuState.Skins:
                    DrawSkinsMenu(gameTime);
                    break;
            }

            spriteBatch.End();
        }

        private void DrawMainMenu(GameTime gameTime)
        {
            //drawing highest score string
            spriteBatch.DrawString(Font, "HIGHSCORE: " + highScore.ToString(), new Vector2(170, 800), Color.White);

            //drawing buttons
            foreach (Button b in mainMenuButtons)
            {
                b.Draw(spriteBatch);
            }
        }

        private void DrawUpgradesMenu(GameTime gameTime)
        {

        }

        private void DrawSkinsMenu(GameTime gameTime)
        {

        }

        private void DrawGameplay(GameTime gametime)
        {
            spriteBatch.Begin(transformMatrix: matrix);

            //drawing background
            gameplayBackground.Draw(spriteBatch);

            //drawing missiles
            foreach (Missile m in missiles)
            {
                m.Draw(spriteBatch);
            }

            //drawing enemies
            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }

            //drawing player health
            player.DrawHearths(spriteBatch);

            //drawing score
            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(310, 18), Color.White);

            //drawing player's space ship
            player.Draw(spriteBatch);

            //drawing pause button when game is unpaused
            if (_gameplayState == GameplayState.Unpaused) pauseButton.Draw(spriteBatch);

            //drawing pause menu when game is paused
            if (_gameplayState == GameplayState.Paused)
            {
                spriteBatch.Draw(pauseBackgroundTexture, new Rectangle(100, 730, 880, 880), Color.White);

                foreach (Button b in pauseMenuButtons)
                {
                    b.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }

        private void DrawEndOfGame(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: matrix);

            //drawing background
            mainMenuBackground.Draw(spriteBatch);

            //drawing NEW HIGH SCORE string 
            if (newHighScoreVisible) spriteBatch.DrawString(Font, "NEW HIGH SCORE!", new Vector2(150, 600), Color.White);

            //drawing score
            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(290, 800), Color.White);

            //drawing buttons
            foreach (Button b in endOfGameButtons)
            {
                b.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        private void ResetGame(GameTime deltaTime)
        {
            _gameplayState = GameplayState.Unpaused;
            actualWave = EnemyWave.First;
            missiles.Clear();
            enemies.Clear();
            player.location.X = 390;
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