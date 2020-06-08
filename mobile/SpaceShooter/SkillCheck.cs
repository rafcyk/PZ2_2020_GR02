using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class SkillCheck
    {
        private Texture2D backgroundTexture, pointerTexture;
        public Rectangle location, pointerLocation;
        public bool isActive = false, isPressed = false;
        private double i, difficulty = 0.05;
        public double multiplier = 1;
        public int damage = 25;
        long finishTime;
        string result;

        public SkillCheck(Texture2D backgroundTexture, Texture2D pointerTexture)
        {
            this.backgroundTexture = backgroundTexture;
            this.pointerTexture = pointerTexture;
        }
        public void Run()
        {
            Random r = new Random();
            int x, y;
            x = r.Next(10, 720);
            y = r.Next(200, 1900);
            location = new Rectangle(x, y, 360, 360);
            pointerLocation = new Rectangle((int)Math.Round(Math.Sin(i) * (160) + x + 160), (int)Math.Round(Math.Cos(i) * (160) + y + 160), 40, 40);
            isPressed = false;
            finishTime = 0;
            i = Math.PI;
            isActive = true;
        }
        public void Update(GameTime deltaTime)
        {
            if (finishTime != 0 && deltaTime.TotalGameTime.TotalSeconds > finishTime) isActive = false;
            else
            {
                if (!isPressed)
                {
                    pointerLocation.X = (int)Math.Round(Math.Sin(i) * (160) + location.X + 160);
                    pointerLocation.Y = (int)Math.Round(Math.Cos(i) * (160) + location.Y + 160);
                    i += difficulty;
                    if (i > 3 * Math.PI)
                    {
                        multiplier -= 0.05;
                        isPressed = true;
                        finishTime = (int)deltaTime.TotalGameTime.TotalSeconds + 1;
                        result = "TOO SLOW!";
                        difficulty += 0.0075;
                        damage -= 2;
                    }
                }
            }
            Console.WriteLine(((int)deltaTime.TotalGameTime.TotalSeconds).ToString());
            Console.WriteLine(finishTime.ToString());
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isPressed)
            {
                spriteBatch.Draw(backgroundTexture, location, Color.White);
                spriteBatch.Draw(pointerTexture, pointerLocation, Color.White);
                spriteBatch.DrawString(font, result, new Vector2(location.X, location.Y), Color.White);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture, location, Color.White);
                spriteBatch.Draw(pointerTexture, pointerLocation, Color.White);
            }
        }
        public void Press(GameTime deltaTime)
        {
            if (i > 2 * Math.PI)
            {
                difficulty += 0.0075;
                isPressed = true;
                finishTime = (int)deltaTime.TotalGameTime.TotalSeconds + 1;

                if (i < 2.5 * Math.PI)
                {
                    result = "NOOB";
                }
                else if (i < 2.75 * Math.PI)
                {
                    result = "NOT BAD!";
                    multiplier += 0.05;
                }
                else if (i < 2.93 * Math.PI)
                {
                    result = "GOOD!";
                    multiplier += 0.1;
                    damage += 5;
                }
                else if (i < 3 * Math.PI)
                {
                    result = "PERFECT!";
                    multiplier += 0.2;
                    damage += 10;
                }
            }
        }
    }
}