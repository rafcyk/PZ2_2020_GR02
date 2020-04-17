﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        Texture2D playerTexture, startButtonTexture, startButtonPressedTexture, exitButtonTexture, exitButtonPressedTexture,
                  pauseBackgroundTexture, backgroundTexture, missileTexture, resumeButtonTexture, resumeButtonPressedTexture,
                  restartButtonTesture, restartButtonPressedTexture,enemyTexture;
        #endregion

        #region Buttons
        Button startButton;
        Button exitButton;
        Button exitToMainMenuButton;
        Button resumeButton;
        Button restartButton;
        #endregion

        Scrolling scroll1;
        Scrolling scroll2;
        enum GameState
        {
            MainMenu,
            GamePlay,
            EndOfGame,
        }

        GameState _currentGameState;
        private int screenCenterX;
        private bool isPausePressed = false;
        private bool shooting = false;

        List<Missile> missiles = new List<Missile>();
        List<Enemy> enemies = new List<Enemy>();
        Random rnd = new Random();

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
            backgroundTexture = Content.Load<Texture2D>("backgroundTexture1");
            missileTexture = Content.Load<Texture2D>("bulletShip1");
            resumeButtonTexture = Content.Load<Texture2D>("buttonResume");
            resumeButtonPressedTexture = Content.Load<Texture2D>("buttonResumePressed");
            restartButtonTesture = Content.Load<Texture2D>("buttonRestart");
            restartButtonPressedTexture = Content.Load<Texture2D>("buttonRestartPressed");
            enemyTexture = Content.Load<Texture2D>("AlienShips2");

            startButton = new Button(startButtonTexture, startButtonPressedTexture, new Rectangle(screenCenterX - 122, 300, 244, 72));
            exitButton = new Button(exitButtonTexture, exitButtonPressedTexture, new Rectangle(screenCenterX - 122, 400, 244, 72));
            exitToMainMenuButton = new Button(exitButtonTexture, exitButtonPressedTexture, new Rectangle(screenCenterX - 122, 460, 244, 72));
            resumeButton = new Button(resumeButtonTexture, resumeButtonPressedTexture, new Rectangle(screenCenterX - 122, 300, 244, 72));
            restartButton = new Button(restartButtonTesture, restartButtonPressedTexture, new Rectangle(screenCenterX - 122, 380, 244, 72));

            scroll1 = new Scrolling(backgroundTexture, new Rectangle(0, 0, 600, 4096));
            scroll2 = new Scrolling(backgroundTexture, new Rectangle(0, -4096, 600, 4096));

            player = new Player(playerTexture);
            player.playerLocation = new Rectangle(screenCenterX - 25, 800 - player.playerTexture.Height - 10, 50, 50);
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

            if (startButton.location.Contains(mousePoint)) startButton.hover(); else startButton.unhover();
            if (exitButton.location.Contains(mousePoint)) exitButton.hover(); else exitButton.unhover();

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
                    RestartGame();
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

            if (exitToMainMenuButton.location.Contains(mousePoint)) exitToMainMenuButton.hover(); else exitToMainMenuButton.unhover();
            if (resumeButton.location.Contains(mousePoint)) resumeButton.hover(); else resumeButton.unhover();
            if (restartButton.location.Contains(mousePoint)) restartButton.hover(); else restartButton.unhover();

            if (exitToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                exitToMainMenuButton.press();
            }
            else if (resumeButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                resumeButton.press();
            }
            else if (restartButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed) {
                restartButton.press();
            }

            if (exitToMainMenuButton.isPressed)
            {
                if (exitToMainMenuButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released)
                {
                    exitToMainMenuButton.unpress();
                    isPausePressed = false;
                    _currentGameState = GameState.MainMenu;
                }
            }
            else if (resumeButton.isPressed) {
                if (resumeButton.location.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Released) {
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
            #endregion

            if (shooting)
            {
                //drawing missiles
                if (missiles.Count == 0)
                {
                    missiles.Add(new Missile(missileTexture, new Rectangle(player.playerLocation.X + player.playerLocation.Width / 2 - 5, player.playerLocation.Y, 10, 32), 5));
                }
                else if (missiles[missiles.Count - 1].isNextReady(150))
                {
                    missiles.Add(new Missile(missileTexture, new Rectangle(player.playerLocation.X + player.playerLocation.Width / 2 - 5, player.playerLocation.Y, 10, 32), 5));
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
                    enemies.Add(new Enemy(enemyTexture, new Rectangle(rnd.Next(500), -100, 100, 100), 2));
                }
                else if (enemies[enemies.Count - 1].isNextReady(300))
                {
                    enemies.Add(new Enemy(enemyTexture, new Rectangle(rnd.Next(500), -100, 100, 100), 2));
                }
                foreach (Enemy e in enemies)
                {
                    //clearing destroyed missiles
                    if (e.isDestroyed)
                    {
                        enemies.Remove(e);
                        break;
                    }
                    else e.Update();
                }

                //collision detection
                foreach (Missile m in missiles)
                {
                    foreach (Enemy e in enemies)
                    {
                        if (e.enemyLocation.Contains(m.missileLocation))
                        {
                            e.isDestroyed = true;
                            m.isDestroyed = true;
                        }
                    }
                }
            }
            else if (isPausePressed == false) shooting = true;
        }

        private void UpdateEndOfGame(GameTime gameTime) { }

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
            spriteBatch.Draw(startButton.texture, startButton.location, Color.White);
            spriteBatch.Draw(exitButton.texture, exitButton.location, Color.White);
            spriteBatch.End();
        }
        private void DrawGameplay(GameTime gameTime) {
            spriteBatch.Begin();
            scroll1.Draw(spriteBatch);
            scroll2.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            foreach (Enemy m in enemies) spriteBatch.Draw(m.enemyTexture, m.enemyLocation, Color.White);
            foreach (Missile m in missiles) spriteBatch.Draw(m.missileTexture, m.missileLocation, Color.White);
            spriteBatch.Draw(player.playerTexture, player.playerLocation, Color.White);
            spriteBatch.End();

            if (isPausePressed == true)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(pauseBackgroundTexture, new Rectangle(screenCenterX-150, 200, 300, 350), Color.White);
                spriteBatch.Draw(resumeButton.texture, resumeButton.location, Color.White);
                spriteBatch.Draw(restartButton.texture, restartButton.location, Color.White);
                spriteBatch.Draw(exitToMainMenuButton.texture, exitToMainMenuButton.location, Color.White);
                spriteBatch.End();
            }
        }
        private void DrawEndOfGame(GameTime gameTime) { }

        private void Pause() {
            shooting = false;
            scroll1.speed = 0;
            scroll2.speed = 0;
        }
        private void RestartGame() {
            player.playerLocation.X = screenCenterX - 25;
            missiles.Clear();
            enemies.Clear();
            scroll1.speed = 1;
            scroll2.speed = 1;
        }
        private void Resume()
        {
            shooting = true;
            scroll1.speed = 1;
            scroll2.speed = 1;
        }
    }
}