using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region player
        Player player;
        #endregion

        #region Textures
        SpriteFont Font;
        Texture2D playerTexture, startButtonTexture, startButtonPressedTexture, exitButtonTexture, exitButtonPressedTexture,
                  pauseBackgroundTexture, backgroundTexture, missileTexture, resumeButtonTexture, resumeButtonPressedTexture,
                  restartButtonTesture, restartButtonPressedTexture, enemyTexture1, enemyTexture2, enemyTexture3, enemyTexture4, enemyTexture5, hearthTexture, mainMenuButtonTexture, mainMenuButtonPressedTexture,
                  upgradesButtonTexture, upgradesButtonPressedTexture, skinsButtonTexture, skinsButtonPressedTexture, skin1ButtonTexture, skin2ButtonTexture,
                  skin3ButtonTexture, skin4ButtonTexture, skin5ButtonTexture, skin1ButtonPressedTexture, skin2ButtonPressedTexture, skin3ButtonPressedTexture,
                  skin4ButtonPressedTexture, skin5ButtonPressedTexture, spaceShipSkin2, spaceShipSkin3, spaceShipSkin4, spaceShipSkin5, bulletSkin1, bulletSkin2, bulletSkin3,
                  bulletSkin4, bulletSkin5, selectedShipCoverTexture, coinTexture, coinBackgroundTexture, oneShotTexture,oneShotPressedTexture,piercingTexture,piercingPressedTexture,
                  movementSpeedTexture,movementspeedPressedTexture,fireRateTexture, fireRatePressedTexture,oneShotMissileTexture,piercingMissileTexture,currentBulletTexture;
        #endregion

        #region Buttons
        Button startButton;
        Button exitButton;
        Button exitToMainMenuButton;
        Button resumeButton;
        Button restartButton;
        Button returnToMainMenuButton;
        Button restartEndOfGameButton;
        Button upgradeButton;
        Button skinsButton;
        Button skin1Button;
        Button skin2Button;
        Button skin3Button;
        Button skin4Button;
        Button skin5Button;
        Button selectedSkinCoverButton;
        Button coinBackgroundButton;
        Button oneShotButton;
        Button piercingButton;
        Button movementSpeedButton;
        Button fireRateButton;
        #endregion

        #region Variables
        Scrolling scroll1;
        Scrolling scroll2;

        private Song menuSong,gameplaySong;

        enum GameState
        {
            MainMenu,
            GamePlay,
            EndOfGame,
        }
        GameState _currentGameState;
        
        EnemyWave _currentEnemyWave;

        private int screenCenterX;
        private bool isPausePressed = false;
        private bool shooting = false;
        private int shootingSpeed = 4;
        private int enemySpeed = 2;
        private int coinCount = 0;
        private int fireRate = 150;
        private bool isSpeedUpReady = true;

        List<Missile> missiles = new List<Missile>();
        List<Enemy> enemies = new List<Enemy>();
        List<TossACoin> coins = new List<TossACoin>();

        Random rnd = new Random();
        private int actualScore = 0;
        private bool showSkinsWindow = false;
        private bool showUpgradeWindow = false;
        private bool throwSomeCoins = false;
        private bool isOneShotBought = false;
        private bool isPiercingBought = false;
        #endregion
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

            menuSong = Content.Load<Song>("menuSong");
            gameplaySong = Content.Load<Song>("gameplayMusic");

            MediaPlayer.Play(menuSong);

            playerTexture = Content.Load<Texture2D>("SpaceShipSmall");
            spaceShipSkin2 = Content.Load<Texture2D>("SpaceShip2");
            spaceShipSkin3 = Content.Load<Texture2D>("SpaceShip3");
            spaceShipSkin4 = Content.Load<Texture2D>("SpaceShip4");
            spaceShipSkin5 = Content.Load<Texture2D>("SpaceShip5");
            selectedShipCoverTexture = Content.Load<Texture2D>("selectedShipCover");

            coinTexture = Content.Load<Texture2D>("Coin");
            coinBackgroundTexture = Content.Load<Texture2D>("coinBackground");
            oneShotTexture = Content.Load<Texture2D>("oneShot");
            oneShotPressedTexture = Content.Load<Texture2D>("oneShot2");
            piercingTexture = Content.Load<Texture2D>("piercing");
            piercingPressedTexture = Content.Load<Texture2D>("piercing2");
            movementSpeedTexture = Content.Load<Texture2D>("movementSpeed");
            movementspeedPressedTexture = Content.Load<Texture2D>("movementSpeed2");
            fireRateTexture = Content.Load<Texture2D>("fireRate");
            fireRatePressedTexture = Content.Load<Texture2D>("fireRate2");

            bulletSkin1 = Content.Load<Texture2D>("bulletShip1");
            bulletSkin2 = Content.Load<Texture2D>("bulletShip2");
            bulletSkin3 = Content.Load<Texture2D>("bulletShip3");
            bulletSkin4 = Content.Load<Texture2D>("bulletShip4");
            bulletSkin5 = Content.Load<Texture2D>("bulletShip5");
            currentBulletTexture = bulletSkin1;
            oneShotMissileTexture = Content.Load<Texture2D>("oneShotMissile");
            piercingMissileTexture = Content.Load<Texture2D>("piercingMissile");

            enemyTexture1 = Content.Load<Texture2D>("AlienShips2");
            enemyTexture2 = Content.Load<Texture2D>("AlienShips1");
            enemyTexture3 = Content.Load<Texture2D>("AlienShips3");
            enemyTexture4 = Content.Load<Texture2D>("AlienShips4");
            enemyTexture5 = Content.Load<Texture2D>("AlienShips5");

            startButtonTexture = Content.Load<Texture2D>("startButton");
            startButtonPressedTexture = Content.Load<Texture2D>("startButtonPressed");
            exitButtonTexture = Content.Load<Texture2D>("exitButton");
            exitButtonPressedTexture = Content.Load<Texture2D>("exitButtonPressed");
            pauseBackgroundTexture = Content.Load<Texture2D>("pauseBackground");
            backgroundTexture = Content.Load<Texture2D>("backgroundTexture1");
            resumeButtonTexture = Content.Load<Texture2D>("buttonResume");
            resumeButtonPressedTexture = Content.Load<Texture2D>("buttonResumePressed");
            restartButtonTesture = Content.Load<Texture2D>("buttonRestart");
            restartButtonPressedTexture = Content.Load<Texture2D>("buttonRestartPressed");
            hearthTexture = Content.Load<Texture2D>("Serducho");
            mainMenuButtonTexture = Content.Load<Texture2D>("buttonMainMenu");
            mainMenuButtonPressedTexture = Content.Load<Texture2D>("buttonMainMenuPressed");
            upgradesButtonTexture = Content.Load<Texture2D>("buttonUpgrades");
            upgradesButtonPressedTexture = Content.Load<Texture2D>("buttonUpgradesPressed");
            skinsButtonTexture = Content.Load<Texture2D>("buttonSkins");
            skinsButtonPressedTexture = Content.Load<Texture2D>("buttonSkinsPressed");

            skin1ButtonTexture = Content.Load<Texture2D>("skin1");
            skin2ButtonTexture = Content.Load<Texture2D>("skin2");
            skin3ButtonTexture = Content.Load<Texture2D>("skin3");
            skin4ButtonTexture = Content.Load<Texture2D>("skin4");
            skin5ButtonTexture = Content.Load<Texture2D>("skin5");
            skin1ButtonPressedTexture = Content.Load<Texture2D>("skin1Pressed");
            skin2ButtonPressedTexture = Content.Load<Texture2D>("skin2Pressed");
            skin3ButtonPressedTexture = Content.Load<Texture2D>("skin3Pressed");
            skin4ButtonPressedTexture = Content.Load<Texture2D>("skin4Pressed");
            skin5ButtonPressedTexture = Content.Load<Texture2D>("skin5Pressed");

            Font = Content.Load<SpriteFont>("font");

            startButton = new Button(startButtonTexture, startButtonPressedTexture, new Rectangle(screenCenterX - 134, 250, 268, 79));
            skinsButton = new Button(skinsButtonTexture, skinsButtonPressedTexture, new Rectangle(screenCenterX - 122, 340, 244, 72));
            exitButton = new Button(exitButtonTexture, exitButtonPressedTexture, new Rectangle(screenCenterX - 122, 430, 244, 72));
            exitToMainMenuButton = new Button(exitButtonTexture, exitButtonPressedTexture, new Rectangle(screenCenterX - 122, 540, 244, 72));
            resumeButton = new Button(resumeButtonTexture, resumeButtonPressedTexture, new Rectangle(screenCenterX - 122, 300, 244, 72));
            restartButton = new Button(restartButtonTesture, restartButtonPressedTexture, new Rectangle(screenCenterX - 122, 380, 244, 72));
            returnToMainMenuButton = new Button(mainMenuButtonTexture, mainMenuButtonPressedTexture, new Rectangle(screenCenterX - 122, 380, 244, 72));
            restartEndOfGameButton = new Button(restartButtonTesture, restartButtonPressedTexture, new Rectangle(screenCenterX - 122, 300, 244, 72));
            upgradeButton = new Button(upgradesButtonTexture, upgradesButtonPressedTexture, new Rectangle(screenCenterX - 122, 460, 244, 72));

            skin1Button = new Button(skin1ButtonTexture, skin1ButtonPressedTexture, new Rectangle(-600, 0, 600, 160));
            skin2Button = new Button(skin2ButtonTexture, skin2ButtonPressedTexture, new Rectangle(-600, 160, 600, 160));
            skin3Button = new Button(skin3ButtonTexture, skin3ButtonPressedTexture, new Rectangle(-600, 320, 600, 160));
            skin4Button = new Button(skin4ButtonTexture, skin4ButtonPressedTexture, new Rectangle(-600, 480, 600, 160));
            skin5Button = new Button(skin5ButtonTexture, skin5ButtonPressedTexture, new Rectangle(-600, 640, 600, 160));
            selectedSkinCoverButton = new Button(selectedShipCoverTexture, selectedShipCoverTexture, new Rectangle(-600, 0, 600, 160));
            coinBackgroundButton = new Button(coinBackgroundTexture, coinBackgroundTexture, new Rectangle(-600, 0, 600, 160));

            oneShotButton = new Button(oneShotTexture, oneShotPressedTexture, new Rectangle(-600, 640, 600, 160));
            piercingButton = new Button(piercingTexture, piercingPressedTexture, new Rectangle(-600, 480, 600, 160));
            movementSpeedButton = new Button(movementSpeedTexture, movementspeedPressedTexture, new Rectangle(-600, 160, 600, 160));
            fireRateButton = new Button(fireRateTexture, fireRatePressedTexture, new Rectangle(-600, 320, 600, 160));

            scroll1 = new Scrolling(backgroundTexture, new Rectangle(0, 0, 600, 4096));
            scroll2 = new Scrolling(backgroundTexture, new Rectangle(0, -4096, 600, 4096));

            player = new Player(playerTexture,hearthTexture);
            missileTexture = bulletSkin1;
            player.playerLocation = new Rectangle(screenCenterX - 35, 800 - player.playerTexture.Height - 25, 70, 70);
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
        private void UpdateMainMenu(GameTime gameTime) {
            this.IsMouseVisible = true;
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            #region ButtonsFunctionality
            if (!showSkinsWindow) {
                if (startButton.location.Contains(mousePoint)) startButton.hover(); else startButton.unhover();
                if (exitButton.location.Contains(mousePoint)) exitButton.hover(); else exitButton.unhover();
                if (skinsButton.location.Contains(mousePoint)) skinsButton.hover(); else skinsButton.unhover();

                if (startButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) startButton.press();
                else if (exitButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) exitButton.press();
                else if (skinsButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) skinsButton.press();

                skin1Button.hideButton();
                skin2Button.hideButton();
                skin3Button.hideButton();
                skin4Button.hideButton();
                skin5Button.hideButton();
                selectedSkinCoverButton.hideButton();
            }
            if (showSkinsWindow) {
                if (skin1Button.location.Contains(mousePoint)) skin1Button.hover(); else skin1Button.unhover();
                if (skin2Button.location.Contains(mousePoint)) skin2Button.hover(); else skin2Button.unhover();
                if (skin3Button.location.Contains(mousePoint)) skin3Button.hover(); else skin3Button.unhover();
                if (skin4Button.location.Contains(mousePoint)) skin4Button.hover(); else skin4Button.unhover();
                if (skin5Button.location.Contains(mousePoint)) skin5Button.hover(); else skin5Button.unhover();

                if (skin1Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) skin1Button.press();
                else if (skin2Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) skin2Button.press();
                else if (skin3Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) skin3Button.press();
                else if (skin4Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) skin4Button.press();
                else if (skin5Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) skin5Button.press();

                skin1Button.showHiddenButton();
                skin2Button.showHiddenButton();
                skin3Button.showHiddenButton();
                skin4Button.showHiddenButton();
                skin5Button.showHiddenButton();
                selectedSkinCoverButton.showHiddenButton();

                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) showSkinsWindow = false;
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
                    MediaPlayer.Stop();
                    MediaPlayer.Play(gameplaySong);
                    MediaPlayer.IsRepeating = true;
                    _currentGameState = GameState.GamePlay;
                    RestartGame();
                }
            }
            else if (skinsButton.isPressed)
            {
                if (skinsButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    skinsButton.unpress();
                    showSkinsWindow = true;
                }
            }
            else if (skin1Button.isPressed)
            {
                if (skin1Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    skin1Button.unpress();
                    player.playerTexture = playerTexture;
                    missileTexture = bulletSkin1;
                    currentBulletTexture = bulletSkin1;
                    selectedSkinCoverButton.location.Y = 0;
                }
            }
            else if (skin2Button.isPressed)
            {
                if (skin2Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    skin2Button.unpress();
                    player.playerTexture = spaceShipSkin2;
                    currentBulletTexture = bulletSkin2;
                    missileTexture = bulletSkin2;
                    selectedSkinCoverButton.location.Y = 160;
                }
            }
            else if (skin3Button.isPressed)
            {
                if (skin3Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    skin3Button.unpress();
                    player.playerTexture = spaceShipSkin3;
                    currentBulletTexture = bulletSkin3;
                    missileTexture = bulletSkin3;
                    selectedSkinCoverButton.location.Y = 320;
                }
            }
            else if (skin4Button.isPressed)
            {
                if (skin4Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    skin4Button.unpress();
                    player.playerTexture = spaceShipSkin4;
                    currentBulletTexture = bulletSkin4;
                    missileTexture = bulletSkin4;
                    selectedSkinCoverButton.location.Y = 480;
                }
            }
            else if (skin5Button.isPressed)
            {
                if (skin5Button.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    skin5Button.unpress();
                    player.playerTexture = spaceShipSkin5;
                    missileTexture = bulletSkin5;
                    currentBulletTexture = bulletSkin5;
                    selectedSkinCoverButton.location.Y = 640;
                }
            }
            #endregion
        }        
        private void UpdateGameplay(GameTime gameTime) {
            if (scroll1.location.Y + scroll1.location.Height >= 8192) {
                scroll1.location.Y = scroll2.location.Y - scroll2.backgroundTexture.Height;
            }
            if (scroll2.location.Y + scroll2.backgroundTexture.Height >= 8192)
            {
                scroll2.location.Y = scroll1.location.Y - scroll1.backgroundTexture.Height;
            }
            scroll1.Update();
            scroll2.Update();
           
            player.UpdateMovement();

            #region ButtonFunctionality

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isPausePressed = true;
                this.IsMouseVisible = true;
                Pause();

                //TODO napisać funkcje pauzująca grę
            }

            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            if (!showUpgradeWindow) {
                if (exitToMainMenuButton.location.Contains(mousePoint)) exitToMainMenuButton.hover(); else exitToMainMenuButton.unhover();
                if (resumeButton.location.Contains(mousePoint)) resumeButton.hover(); else resumeButton.unhover();
                if (restartButton.location.Contains(mousePoint)) restartButton.hover(); else restartButton.unhover();
                if (upgradeButton.location.Contains(mousePoint)) upgradeButton.hover(); else upgradeButton.unhover();

                if (exitToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) exitToMainMenuButton.press();
                else if (resumeButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) resumeButton.press();
                else if (restartButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) restartButton.press();
                else if (upgradeButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) upgradeButton.press();
                coinBackgroundButton.hideButton();
                oneShotButton.hideButton();
                piercingButton.hideButton();
                movementSpeedButton.hideButton();
                fireRateButton.hideButton();
            }
            if (showUpgradeWindow) {
                if (oneShotButton.location.Contains(mousePoint)) oneShotButton.hover(); else oneShotButton.unhover();
                if (piercingButton.location.Contains(mousePoint)) piercingButton.hover(); else piercingButton.unhover();
                if (movementSpeedButton.location.Contains(mousePoint)) movementSpeedButton.hover(); else movementSpeedButton.unhover();
                if (fireRateButton.location.Contains(mousePoint)) fireRateButton.hover(); else fireRateButton.unhover();

                if (oneShotButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) oneShotButton.press();
                else if (piercingButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) piercingButton.press();
                else if (movementSpeedButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) movementSpeedButton.press();
                else if (fireRateButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) fireRateButton.press();

                coinBackgroundButton.showHiddenButton();
                oneShotButton.showHiddenButton();
                piercingButton.showHiddenButton();
                movementSpeedButton.showHiddenButton();
                fireRateButton.showHiddenButton();
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) showUpgradeWindow = false;
            }
            
            if (exitToMainMenuButton.isPressed)
            {
                if (exitToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(menuSong);
                    exitToMainMenuButton.unpress();
                    isPausePressed = false;
                    _currentGameState = GameState.MainMenu;
                }
            }
            else if (resumeButton.isPressed)
            {
                if (resumeButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    resumeButton.unpress();
                    isPausePressed = false;
                    Resume();
                }
            }
            else if (restartButton.isPressed)
            {
                if (restartButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    restartButton.unpress();
                    isPausePressed = false;
                    RestartGame();
                }
            }
            else if (upgradeButton.isPressed) {
                if (upgradeButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    upgradeButton.unpress();
                    showUpgradeWindow = true;
                }
            }
            else if (oneShotButton.isPressed)
            {
                if (oneShotButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    oneShotButton.unpress();
                    if (coinCount >= 5 && !isOneShotBought)
                    {
                        missileTexture = oneShotMissileTexture;
                        coinCount -= 5;
                    }
                }
            }
            else if (piercingButton.isPressed)
            {
                if (piercingButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    piercingButton.unpress();
                    if (coinCount >= 3 && !isPiercingBought) {
                        missileTexture = piercingMissileTexture;
                        coinCount -= 3;
                    } 
                }
            }
            else if (movementSpeedButton.isPressed)
            {
                if (movementSpeedButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    movementSpeedButton.unpress();
                    if(coinCount >= 1)
                    {
                        player.speed++;
                        coinCount--;
                    }
                }
            }
            else if (fireRateButton.isPressed)
            {
                if (fireRateButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    fireRateButton.unpress();
                    if (coinCount >= 1)
                    {
                        shootingSpeed++;
                        coinCount--;
                    }
                }
            }
            #endregion

            if (shooting)
            {
                //waves config
                if (actualScore > 100 && _currentEnemyWave == EnemyWave.First)
                {
                    throwSomeCoins = true;
                    _currentEnemyWave = EnemyWave.Second;
                    enemySpeed = 2;
                }
                else if (actualScore > 200 && _currentEnemyWave == EnemyWave.Second)
                {
                    throwSomeCoins = true;
                    _currentEnemyWave = EnemyWave.Third;
                    enemySpeed = 2;
                }
                else if (actualScore > 300 && _currentEnemyWave == EnemyWave.Third)
                {
                    throwSomeCoins = true;
                    _currentEnemyWave = EnemyWave.Fourth;
                    enemySpeed = 2;
                }
                else if (actualScore > 400 && _currentEnemyWave == EnemyWave.Fourth)
                {
                    throwSomeCoins = true;
                    _currentEnemyWave = EnemyWave.Fifth;
                    enemySpeed = 2;
                }
                else if (actualScore > 500 && actualScore % 60 == 0 && isSpeedUpReady == true) {
                    enemySpeed++;
                    isSpeedUpReady = false;
                }
                else if (actualScore > 500 && actualScore % 60 != 0 )
                {
                    isSpeedUpReady = true;
                }

                //drawing missiles
                if (missiles.Count == 0)
                {
                    missiles.Add(new Missile(missileTexture, new Rectangle(player.playerLocation.X + player.playerLocation.Width / 2 - 5, player.playerLocation.Y, 10, 32), shootingSpeed));
                }
                else if (missiles[missiles.Count - 1].isNextReady(fireRate))
                {
                    missiles.Add(new Missile(missileTexture, new Rectangle(player.playerLocation.X + player.playerLocation.Width / 2 - 5, player.playerLocation.Y, 10, 32), shootingSpeed));
                }
                foreach (Missile m in missiles) {
                    //clearing destroyed missiles
                    if (m.isDestroyed) {
                        missiles.Remove(m);
                        break;
                    } 
                    else m.Update();
                }
                
                //drawing enemies
                if (enemies.Count == 0)
                {
                    enemies.Add(new Enemy(enemyTexture1, enemyTexture2, enemyTexture3, enemyTexture4, enemyTexture5, new Rectangle(rnd.Next(500), -100, 100, 100), enemySpeed, _currentEnemyWave));
                }
                else if (enemies[enemies.Count - 1].isNextReady())
                {
                    enemies.Add(new Enemy(enemyTexture1, enemyTexture2, enemyTexture3, enemyTexture4, enemyTexture5, new Rectangle(rnd.Next(500), -100, 100, 100), enemySpeed, _currentEnemyWave));
                }
                foreach (Enemy e in enemies)
                {
                    //clearing destroyed enemies
                    if (e.isDestroyed)
                    {
                        enemies.Remove(e);
                        actualScore++;
                        break;
                    }
                    else e.Update();
                    
                    if (e.enemyLocation.Y > 800)
                    {
                        player.Hit();
                        e.isDestroyed = true;
                    }

                    if (player.health == 0)
                    {
                        Pause();
                        _currentGameState = GameState.EndOfGame;
                    }
                }

                //missiles collision detection
                foreach (Missile m in missiles)
                {
                    foreach (Enemy e in enemies)
                    {
                        if (e.enemyLocation.Contains(m.missileLocation))
                        {
                            if (missileTexture == oneShotMissileTexture) e.Hit(500);
                            else if (missileTexture == piercingMissileTexture) e.Hit(50);
                            else e.Hit(25);

                            m.isDestroyed = true;
                            if (actualScore % 10 == 0 && enemySpeed != 6) enemySpeed++;
                        }
                    }

                    foreach (TossACoin c in coins) {
                        if (c.coinLocation.Contains(m.missileLocation)) {
                            c.isDestroyed = true;
                            coinCount++;
                        }
                    }
                }
                //update coins, check if out of screen
                foreach (TossACoin c in coins)
                {
                    if (c.isDestroyed)
                    {
                        coins.Remove(c);
                        break;
                    } 
                    else c.Update();
                }
                //drawing coins
                if (throwSomeCoins) {
                    coins.Add(new TossACoin(coinTexture, new Rectangle(rnd.Next(500), -90, 45, 55)));
                    coins.Add(new TossACoin(coinTexture, new Rectangle(rnd.Next(500), -150, 45, 55)));
                    coins.Add(new TossACoin(coinTexture, new Rectangle(rnd.Next(500), -210, 45, 55)));

                    throwSomeCoins = false;
                }
            }
            else if (isPausePressed == false) shooting = true;
        }
        private void UpdateEndOfGame(GameTime gameTime) {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            if (returnToMainMenuButton.location.Contains(mousePoint)) returnToMainMenuButton.hover(); else returnToMainMenuButton.unhover();
            if (restartEndOfGameButton.location.Contains(mousePoint)) restartEndOfGameButton.hover(); else restartEndOfGameButton.unhover();

            if (returnToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                returnToMainMenuButton.press();
            }
            else if (restartEndOfGameButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                restartEndOfGameButton.press();
            }

            if (returnToMainMenuButton.isPressed)
            {
                if (returnToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(menuSong);
                    returnToMainMenuButton.unpress();
                    isPausePressed = false;
                    _currentGameState = GameState.MainMenu;
                }
            }
            else if (restartEndOfGameButton.isPressed)
            {
                if (restartEndOfGameButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    restartEndOfGameButton.unpress();
                    isPausePressed = false;
                    RestartGame();
                    _currentGameState = GameState.GamePlay;
                }
            }

        }

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
        private void DrawMainMenu(GameTime gameTime) {
            spriteBatch.Begin();
            scroll1.Draw(spriteBatch);
            scroll2.Draw(spriteBatch);
            spriteBatch.Draw(startButton.texture, startButton.location, Color.White);
            spriteBatch.Draw(skinsButton.texture, skinsButton.location, Color.White);
            spriteBatch.Draw(exitButton.texture, exitButton.location, Color.White);
            spriteBatch.Draw(skin1Button.texture, skin1Button.location, Color.White);
            spriteBatch.Draw(skin2Button.texture, skin2Button.location, Color.White);
            spriteBatch.Draw(skin3Button.texture, skin3Button.location, Color.White);
            spriteBatch.Draw(skin4Button.texture, skin4Button.location, Color.White);
            spriteBatch.Draw(skin5Button.texture, skin5Button.location, Color.White);
            spriteBatch.Draw(selectedSkinCoverButton.texture, selectedSkinCoverButton.location, Color.White);
            spriteBatch.End();
        }
        private void DrawGameplay(GameTime gameTime) {
            spriteBatch.Begin();
            scroll1.Draw(spriteBatch);
            scroll2.Draw(spriteBatch);
            foreach (Missile m in missiles) spriteBatch.Draw(m.missileTexture, m.missileLocation, Color.White);
            foreach (Enemy m in enemies) spriteBatch.Draw(m.enemyTexture, m.enemyLocation, Color.White);
            spriteBatch.Draw(player.playerTexture, player.playerLocation, Color.White);
            player.DrawHearths(spriteBatch);
            spriteBatch.DrawString(Font, "SCORE: " + actualScore.ToString(), new Vector2(250, 45), Color.White);
            spriteBatch.Draw(coinTexture, new Rectangle(500, 30, 45, 55), Color.White);
            spriteBatch.DrawString(Font, "x " + coinCount.ToString(), new Vector2(560, 40), Color.White);

            if (isPausePressed)
            {
                spriteBatch.Draw(pauseBackgroundTexture, new Rectangle(screenCenterX-150, 200, 300, 430), Color.White);
                spriteBatch.Draw(resumeButton.texture, resumeButton.location, Color.White);
                spriteBatch.Draw(restartButton.texture, restartButton.location, Color.White);
                spriteBatch.Draw(upgradeButton.texture, upgradeButton.location, Color.White);
                spriteBatch.Draw(exitToMainMenuButton.texture, exitToMainMenuButton.location, Color.White);
            }

            spriteBatch.Draw(oneShotButton.texture, oneShotButton.location, Color.White);
            spriteBatch.Draw(piercingButton.texture, piercingButton.location, Color.White);
            spriteBatch.Draw(movementSpeedButton.texture, movementSpeedButton.location, Color.White);
            spriteBatch.Draw(fireRateButton.texture, fireRateButton.location, Color.White);

            foreach (TossACoin coin in coins) spriteBatch.Draw(coin.coinTexture, coin.coinLocation, Color.White);
            spriteBatch.End();
        }
        private void DrawEndOfGame(GameTime gameTime) {
            spriteBatch.Begin();
            scroll1.Draw(spriteBatch);
            scroll2.Draw(spriteBatch);
            spriteBatch.DrawString(Font, "HIGHSCORE: " + actualScore.ToString(), new Vector2(200, 250), Color.White);
            spriteBatch.Draw(returnToMainMenuButton.texture, returnToMainMenuButton.location, Color.White);
            spriteBatch.Draw(restartEndOfGameButton.texture, restartEndOfGameButton.location, Color.White);
            spriteBatch.End();
        }

        private void Pause() {
            shooting = false;
            scroll1.speed = 0;
            scroll2.speed = 0;
        }
        private void RestartGame() {
            MediaPlayer.Stop();
            MediaPlayer.Play(gameplaySong);
            MediaPlayer.IsRepeating = true;
            player.playerLocation.X = screenCenterX - 25;
            missiles.Clear();
            enemies.Clear();
            coins.Clear();
            scroll1.speed = 1;
            scroll2.speed = 1;
            player.health = 3;
            player.speed = 6;
            shootingSpeed = 4;
            actualScore = 0;
            _currentEnemyWave = EnemyWave.First;
            enemySpeed = 2;
            coinCount = 0;
            missileTexture = currentBulletTexture;
        }
        private void Resume()
        {
            shooting = true;
            scroll1.speed = 1;
            scroll2.speed = 1;
        }
    }
}
