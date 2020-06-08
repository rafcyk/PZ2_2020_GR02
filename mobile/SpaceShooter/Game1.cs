using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.IO;
using System;
using Android.Views;
using Microsoft.Xna.Framework.Media;


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
        }

        enum MenuState
        {
            Main,
            Skins,
            EndOfGame,
            Instructions,
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
        Texture2D startButtonTexture, startButtonPressedTexture, exitButtonTexture, exitButtonPressedTexture, skinsButtonTexture, skinsButtonPressedTexture, soundOnButtonTexture, soundOnButtonPressedTexture, soundOffButtonTexture, soundOffButtonPressedTexture,
            spaceShip1Texture, spaceShip2Texture, spaceShip3Texture, spaceShip4Texture, spaceShip5Texture, enemy1Texture, enemy2Texture, enemy3Texture, enemy4Texture, selectedShipCover,
            missile1Texture, missile2Texture, missile3Texture, missile4Texture, missile5Texture, heartTexture,
            pauseButtonTexture, pauseButtonPressedTexture, resumeButtonTexture, resumeButtonPressedTexture, pauseBackgroundTexture,
            restartButtonTexture, restartButtonPressedTexture, menuButtonTexture, menuButtonPressedTexture,
            background1Texture, background2Texture,
            skillCheckBackgroundTexture, skillCheckPointerTexture,
            skin1Texture, skin2Texture, skin3Texture, skin4Texture, skin5Texture, skin1PressedTexture, skin2PressedTexture, skin3PressedTexture, skin4PressedTexture, skin5PressedTexture, backButtonTexture, backButtonPressedTexture,
            instructionsTexture, instructionsButtonTexture, instructionsButtonPressedTexture;
        #endregion

        #region player
        Player player;
        int fireRate = 250; // lower = faster, lowest = 32
        int enemySpeed = 2;
        double nextBlinkTime = 0;
        int heartBlinkLeft = 0;
        #endregion

        #region Resolution scaling
        float screenWidth, screenHeight, scaleX, scaleY;
        int virtualWidth, virtualHeight;
        Matrix matrix;
        #endregion

        #region controls
        List<Button> mainMenuButtons = new List<Button>();
        List<Button> skinsMenuButtons = new List<Button>();
        List<Missile> missiles = new List<Missile>();
        List<Enemy> enemies = new List<Enemy>();
        Button pauseButton;
        List<Button> pauseMenuButtons = new List<Button>();
        List<Button> endOfGameButtons = new List<Button>();
        List<Button> instructionsButtons = new List<Button>();
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

        #region songs, backgrounds, list of textures
        Song menuSong, gamePlayMusic;
        GameplayBackground gameplayBackground;
        MainMenuBackground mainMenuBackground;
        List<Texture2D> enemyTextures = new List<Texture2D>();
        List<Texture2D> shipTextures = new List<Texture2D>();
        List<Texture2D> missileTextures = new List<Texture2D>();
        #endregion

        SkillCheck skillCheck;
        long nextSkilCheck;
        private Random r = new Random();
        bool newHighScore = false;
        bool newHighScoreVisible;
        int startTime = 0;
        int selectedShip = 0;
        EnemyWave actualWave;
        bool soundOn = true;
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
            MediaPlayer.IsRepeating = true;
        }
        protected override void LoadContent()
        {
            //load content
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backButtonTexture = Content.Load<Texture2D>("backButton");
            backButtonPressedTexture = Content.Load<Texture2D>("backButtonPressed");
            background1Texture = Content.Load<Texture2D>("background1");
            background2Texture = Content.Load<Texture2D>("background2");
            enemy1Texture = Content.Load<Texture2D>("enemy1");
            enemy2Texture = Content.Load<Texture2D>("enemy2");
            enemy3Texture = Content.Load<Texture2D>("enemy3");
            enemy4Texture = Content.Load<Texture2D>("enemy4");
            exitButtonTexture = Content.Load<Texture2D>("exitButton");
            exitButtonPressedTexture = Content.Load<Texture2D>("exitButtonPressed");
            heartTexture = Content.Load<Texture2D>("heart");
            instructionsTexture = Content.Load<Texture2D>("instructions");
            instructionsButtonPressedTexture = Content.Load<Texture2D>("instructionsButtonPressed");
            instructionsButtonTexture = Content.Load<Texture2D>("instructionsButton");
            menuButtonTexture = Content.Load<Texture2D>("menuButton");
            menuButtonPressedTexture = Content.Load<Texture2D>("menuButtonPressed");
            missile1Texture = Content.Load<Texture2D>("missile1");
            missile2Texture = Content.Load<Texture2D>("missile2");
            missile3Texture = Content.Load<Texture2D>("missile3");
            missile4Texture = Content.Load<Texture2D>("missile4");
            missile5Texture = Content.Load<Texture2D>("missile5");
            pauseBackgroundTexture = Content.Load<Texture2D>("pauseBackground");
            pauseButtonTexture = Content.Load<Texture2D>("pauseButton");
            pauseButtonPressedTexture = Content.Load<Texture2D>("pauseButtonPressed");
            restartButtonTexture = Content.Load<Texture2D>("restartButton");
            restartButtonPressedTexture = Content.Load<Texture2D>("restartButtonPressed");
            resumeButtonTexture = Content.Load<Texture2D>("resumeButton");
            resumeButtonPressedTexture = Content.Load<Texture2D>("resumeButtonPressed");
            selectedShipCover = Content.Load<Texture2D>("selectedShipCover");
            skillCheckBackgroundTexture = Content.Load<Texture2D>("skillCheckBackground");
            skillCheckPointerTexture = Content.Load<Texture2D>("skillCheckPointer");
            skin1Texture = Content.Load<Texture2D>("skin1");
            skin1PressedTexture = Content.Load<Texture2D>("skin1Pressed");
            skin2Texture = Content.Load<Texture2D>("skin2");
            skin2PressedTexture = Content.Load<Texture2D>("skin2Pressed");
            skin3Texture = Content.Load<Texture2D>("skin3");
            skin3PressedTexture = Content.Load<Texture2D>("skin3Pressed");
            skin4Texture = Content.Load<Texture2D>("skin4");
            skin4PressedTexture = Content.Load<Texture2D>("skin4Pressed");
            skin5Texture = Content.Load<Texture2D>("skin5");
            skin5PressedTexture = Content.Load<Texture2D>("skin5Pressed");
            skinsButtonTexture = Content.Load<Texture2D>("skinsButton");
            skinsButtonPressedTexture = Content.Load<Texture2D>("skinsButtonPressed");
            soundOffButtonTexture = Content.Load<Texture2D>("soundOffButton");
            soundOffButtonPressedTexture = Content.Load<Texture2D>("soundOffButtonPressed");
            soundOnButtonTexture = Content.Load<Texture2D>("soundOnButton");
            soundOnButtonPressedTexture = Content.Load<Texture2D>("soundOnButtonPressed");
            spaceShip1Texture = Content.Load<Texture2D>("spaceShip1");
            spaceShip2Texture = Content.Load<Texture2D>("spaceShip2");
            spaceShip3Texture = Content.Load<Texture2D>("spaceShip3");
            spaceShip4Texture = Content.Load<Texture2D>("spaceShip4");
            spaceShip5Texture = Content.Load<Texture2D>("spaceShip5");
            startButtonTexture = Content.Load<Texture2D>("startButton");
            startButtonPressedTexture = Content.Load<Texture2D>("startButtonPressed");

            Font = Content.Load<SpriteFont>("pixel_f70");

            menuSong = Content.Load<Song>("menuSong");
            gamePlayMusic = Content.Load<Song>("gameplayMusic");

            //declare main menu background
            mainMenuBackground = new MainMenuBackground(background1Texture);

            //declare mainMenuButtons
            mainMenuButtons.Add(new Button("start", startButtonTexture, startButtonPressedTexture, new Rectangle(100, 1000, 880, 260)));
            mainMenuButtons.Add(new Button("skins", skinsButtonTexture, skinsButtonPressedTexture, new Rectangle(188, 1300, 704, 208)));
            mainMenuButtons.Add(new Button("instructions", instructionsButtonTexture, instructionsButtonPressedTexture, new Rectangle(188, 1548, 704, 208)));
            mainMenuButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(232, 1796, 616, 182)));
            mainMenuButtons.Add(new Button("sound", soundOnButtonTexture, soundOnButtonPressedTexture, new Rectangle(870, 18, 192, 192)));

            //skins menu buttons
            skinsMenuButtons.Add(new Button("back", backButtonTexture, backButtonPressedTexture, new Rectangle(100, 30, 270, 260)));
            skinsMenuButtons.Add(new Button("skin1", skin1Texture, skin1PressedTexture, new Rectangle(0, 390, 1080, 290)));
            skinsMenuButtons.Add(new Button("skin2", skin2Texture, skin2PressedTexture, new Rectangle(0, 780, 1080, 290)));
            skinsMenuButtons.Add(new Button("skin3", skin3Texture, skin3PressedTexture, new Rectangle(0, 1170, 1080, 290)));
            skinsMenuButtons.Add(new Button("skin4", skin4Texture, skin4PressedTexture, new Rectangle(0, 1560, 1080, 290)));
            skinsMenuButtons.Add(new Button("skin5", skin5Texture, skin5PressedTexture, new Rectangle(0, 1950, 1080, 290)));

            //gameplay pause button and background
            pauseButton = new Button("pause", pauseButtonTexture, pauseButtonPressedTexture, new Rectangle(870, 18, 192, 192));
            gameplayBackground = new GameplayBackground(background1Texture, background2Texture);

            //pause menu buttons
            pauseMenuButtons.Add(new Button("resume", resumeButtonTexture, resumeButtonPressedTexture, new Rectangle(200, 1070, 680, 201)));
            pauseMenuButtons.Add(new Button("exit", exitButtonTexture, exitButtonPressedTexture, new Rectangle(200, 1321, 680, 201)));

            //end of game screen buttons
            endOfGameButtons.Add(new Button("restart", restartButtonTexture, restartButtonPressedTexture, new Rectangle(100, 1100, 880, 260)));
            endOfGameButtons.Add(new Button("menu", menuButtonTexture, menuButtonPressedTexture, new Rectangle(216, 1400, 648, 192)));

            //instructions button
            instructionsButtons.Add(new Button("back", backButtonTexture, backButtonPressedTexture, new Rectangle(100, 30, 270, 260)));

            //player ship textures list
            shipTextures.Add(spaceShip1Texture);
            shipTextures.Add(spaceShip2Texture);
            shipTextures.Add(spaceShip3Texture);
            shipTextures.Add(spaceShip4Texture);
            shipTextures.Add(spaceShip5Texture);

            //missiles textures list
            missileTextures.Add(missile1Texture);
            missileTextures.Add(missile2Texture);
            missileTextures.Add(missile3Texture);
            missileTextures.Add(missile4Texture);
            missileTextures.Add(missile5Texture);

            //enemyTextures list
            enemyTextures.Add(enemy1Texture);
            enemyTextures.Add(enemy2Texture);
            enemyTextures.Add(enemy3Texture);
            enemyTextures.Add(enemy4Texture);
            enemyTextures.Add(enemy4Texture);

            //declare skillcheck
            skillCheck = new SkillCheck(skillCheckBackgroundTexture, skillCheckPointerTexture);

            //LoadSettings and highest score
            LoadSettings();

            //declare player
            player = new Player(shipTextures[selectedShip], new Rectangle(390, 2040, 300, 300), heartTexture, missileTextures[selectedShip]);

            PlaySong();
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //hide navigation bar
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
                case MenuState.Skins:
                    UpdateSkinsMenu(deltaTime);
                    break;
                case MenuState.EndOfGame:
                    UpdateEndOfGame(deltaTime);
                    break;
                case MenuState.Instructions:
                    UpdateInstructions(deltaTime);
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
                                    PlaySong();
                                    break;
                                case "skins":
                                    b.unpress();
                                    _menuState = MenuState.Skins;
                                    break;
                                case "instructions":
                                    b.unpress();
                                    _menuState = MenuState.Instructions;
                                    break;
                                case "exit":
                                    b.unpress();
                                    Game.Activity.MoveTaskToBack(true);
                                    break;
                                case "sound":
                                    soundOn = !soundOn;
                                    if (soundOn)
                                    {
                                        //mainMenuButtons[4].mainTexture = soundOnButtonTexture;
                                        //mainMenuButtons[4].pressedTexture = soundOnButtonPressedTexture;
                                    }
                                    else
                                    {
                                        //mainMenuButtons[4].mainTexture = soundOffButtonTexture;
                                        //mainMenuButtons[4].pressedTexture = soundOffButtonPressedTexture;
                                    }
                                    b.unpress();
                                    SaveSettings();
                                    PlaySong();
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
                                case "back":
                                    b.unpress();
                                    _menuState = MenuState.Main;
                                    break;
                                case "skin1":
                                    b.unpress();
                                    selectedShip = 0;
                                    break;
                                case "skin2":
                                    b.unpress();
                                    selectedShip = 1;
                                    break;
                                case "skin3":
                                    b.unpress();
                                    selectedShip = 2;
                                    break;
                                case "skin4":
                                    b.unpress();
                                    selectedShip = 3;
                                    break;
                                case "skin5":
                                    b.unpress();
                                    selectedShip = 4;
                                    break;
                            }
                            SaveSettings();
                            player.texture = shipTextures[selectedShip];
                            player.missileTexture = missileTextures[selectedShip];
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

                //creating missiles
                if (missiles.Count == 0) missiles.Add(new Missile(player.missileTexture, player.location.X, (int)(player.missileSpeed * skillCheck.multiplier)));
                else if (missiles[missiles.Count - 1].isNextReady(fireRate)) missiles.Add(new Missile(player.missileTexture, player.location.X, (int)(player.missileSpeed * skillCheck.multiplier)));

                //creating enemies
                if (deltaTime.TotalGameTime.TotalSeconds > startTime)
                {
                    if (enemies.Count == 0) enemies.Add(new Enemy(enemyTextures, r.Next(4), enemySpeed, actualWave));
                    else if (enemies[enemies.Count - 1].isNextReady()) enemies.Add(new Enemy(enemyTextures, r.Next(4), enemySpeed, actualWave));
                }

                if (deltaTime.TotalGameTime.TotalSeconds > nextSkilCheck)
                {
                    skillCheck.Run();
                    nextSkilCheck = r.Next((int)deltaTime.TotalGameTime.TotalSeconds + 20, (int)deltaTime.TotalGameTime.TotalSeconds + 40);
                }

                //updateplayer health
                foreach (Enemy e in enemies)
                {
                    if (e.location.Y >= 2340 && !e.isDestroyed)
                    {
                        e.isDestroyed = true;
                        heartBlinkLeft += 10;
                        if (player.Hit())
                        {
                            if (actualScore > highScore)
                            {
                                highScore = actualScore;
                                newHighScore = true;
                                SaveSettings();
                            }
                            else newHighScore = false;
                            _gameState = GameState.Menu;
                            _menuState = MenuState.EndOfGame;

                            PlaySong();
                        }
                    }
                }

                //enemy-missile collision handling
                foreach (Missile m in missiles)
                {
                    foreach (Enemy e in enemies)
                    {
                        if (e.location.Contains(new Point(m.location.X + 8, m.location.Y + 135)) && !(e.isDestroyed || m.isDestroyed))
                        {
                            if (e.Hit(skillCheck.damage))
                            {
                                actualScore++;
                                if (actualScore % 5 == 0) enemySpeed++;
                            }
                            m.isDestroyed = true;
                        }
                    }
                }

                //hearts blinking
                if (heartBlinkLeft > 0)
                {
                    if (deltaTime.TotalGameTime.TotalMilliseconds >= nextBlinkTime)
                    {
                        player.heartVisible = !player.heartVisible;

                        nextBlinkTime = deltaTime.TotalGameTime.TotalMilliseconds + 100;
                        heartBlinkLeft--;
                    }
                }

                if (skillCheck.isActive)
                {
                    //updating enemies, missiles and background position
                    foreach (Enemy e in enemies) e.UpdateSlow();
                    foreach (Missile m in missiles) m.UpdateSlow();
                    gameplayBackground.UpdateSlow();
                    skillCheck.Update(deltaTime);

                    if (!skillCheck.isPressed)
                    {
                        touchCollection = TouchPanel.GetState();
                        foreach (TouchLocation tl in touchCollection)
                        {
                            if (tl.State == TouchLocationState.Pressed) skillCheck.Press(deltaTime);
                        }
                    }
                }
                else
                {
                    //updating enemies, missiles and background position
                    foreach (Enemy e in enemies) e.Update();
                    foreach (Missile m in missiles) m.Update();
                    gameplayBackground.Update();

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
                                //player ship controls
                                if (touchPosition.X < 150) player.location.X = 0;
                                else if (touchPosition.X > 930) player.location.X = 780;
                                else player.location.X = (int)touchPosition.X - 150;
                            }
                        }

                    }

                    //pressing Android back button
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        _gameplayState = GameplayState.Paused;
                    }
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
                                        PlaySong();
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
                                    PlaySong();
                                    break;
                                case "menu":
                                    b.unpress();
                                    _menuState = MenuState.Main;
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
        private void UpdateInstructions(GameTime deltaTime)
        {
            touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                Vector2 position = new Vector2((int)tl.Position.X, (int)tl.Position.Y);
                Vector2 touchPosition = Vector2.Transform(position, Matrix.Invert(matrix));

                foreach (Button b in instructionsButtons)
                {
                    if (b.isPressed)
                    {
                        if (tl.State == TouchLocationState.Released && b.location.Contains(touchPosition))
                        {
                            switch (b.Name)
                            {
                                case "back":
                                    b.unpress();
                                    _menuState = MenuState.Main;
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
                case MenuState.Skins:
                    DrawSkinsMenu(gameTime);
                    break;
                case MenuState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
                case MenuState.Instructions:
                    DrawInstructions(gameTime);
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
        private void DrawSkinsMenu(GameTime gameTime)
        {
            //drawing top skins texture
            spriteBatch.Draw(skinsButtonTexture, new Rectangle(100, 30, 880, 260), Color.White);

            //drawing buttons
            foreach (Button b in skinsMenuButtons)
            {
                b.Draw(spriteBatch);
            }

            //drawing selected ship cover
            spriteBatch.Draw(selectedShipCover, new Rectangle(0, 390 + (390 * selectedShip), 1080, 290), Color.White);
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
            player.DrawHearts(spriteBatch);

            //drawing score
            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(310, 18), Color.White);

            //drawing player's space ship
            player.Draw(spriteBatch);

            //drawing pause button if unpaused / pause menu if paused
            if (_gameplayState == GameplayState.Unpaused) pauseButton.Draw(spriteBatch);
            else if (_gameplayState == GameplayState.Paused)
            {
                spriteBatch.Draw(pauseBackgroundTexture, new Rectangle(100, 730, 880, 880), Color.White);

                foreach (Button b in pauseMenuButtons)
                {
                    b.Draw(spriteBatch);
                }
            }

            if (skillCheck.isActive) skillCheck.Draw(spriteBatch, Font);

            spriteBatch.End();
        }
        private void DrawEndOfGame(GameTime gameTime)
        {
            //drawing NEW HIGH SCORE string 
            if (newHighScoreVisible) spriteBatch.DrawString(Font, "NEW HIGH SCORE!", new Vector2(150, 600), Color.White);

            //drawing score
            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(290, 800), Color.White);

            //drawing buttons
            foreach (Button b in endOfGameButtons)
            {
                b.Draw(spriteBatch);
            }
        }
        private void DrawInstructions(GameTime gameTime)
        {
            spriteBatch.Draw(instructionsTexture, new Rectangle(40, 300, 1000, 2000), Color.White);

            foreach (Button b in instructionsButtons)
            {
                b.Draw(spriteBatch);
            }
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
            skillCheck.isActive = false;
            skillCheck.isPressed = false;
            skillCheck.multiplier = 1;
            skillCheck.damage = 25;
            nextSkilCheck = r.Next((int)deltaTime.TotalGameTime.TotalSeconds + 20, (int)deltaTime.TotalGameTime.TotalSeconds + 40);
        }
        private void SaveSettings()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "settings.bin");
            int song;

            if (soundOn) song = 1;
            else song = 0;

            try
            {
                using (var streamWriter = new StreamWriter(filename, false))
                {
                    streamWriter.Write(highScore.ToString() + ";" + selectedShip.ToString() + ";" + song.ToString()); ;
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
                    string[] parm = content.Split(';');
                    highScore = Int32.Parse(parm[0]);
                    selectedShip = Int32.Parse(parm[1]);
                    if (Int32.Parse(parm[2]) == 1) soundOn = true;
                    else if (Int32.Parse(parm[2]) == 0)
                    {
                        soundOn = false;
                        mainMenuButtons[4].texture = soundOffButtonTexture;
                        mainMenuButtons[4].mainTexture = soundOffButtonTexture;
                        mainMenuButtons[4].pressedTexture = soundOffButtonPressedTexture;
                    }
                }
            }
            catch
            {
                highScore = 0;
                selectedShip = 0;
            }
        }
        private void PlaySong()
        {
            if (soundOn)
            {
                switch (_gameState)
                {
                    case GameState.Menu:
                        MediaPlayer.Stop();
                        MediaPlayer.Play(menuSong);
                        break;
                    case GameState.GamePlay:
                        MediaPlayer.Stop();
                        MediaPlayer.Play(gamePlayMusic);
                        break;
                }
            }
            else
            {
                if (MediaPlayer.State == MediaState.Playing) MediaPlayer.Stop();
            }
        }
    }
}