using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class MainMenuBackground
    {
        public Texture2D texture;
        public Rectangle location;
        private double i = 0;

        public MainMenuBackground(Texture2D texture)
        {
            this.texture = texture;
            this.location = new Rectangle(-500, -500, 2160, 4680);
        }

        public void Update()
        {
            location.X = (int)Math.Round(Math.Sin(i) * (450)) - 500;
            location.Y = (int)Math.Round(Math.Cos(i) * (450)) - 500;
            i += 0.005;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.White);
        }
    }
}