using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class GameplayBackground
    {
        public Texture2D texture1, texture2;
        public Rectangle location1, location2;

        public GameplayBackground(Texture2D texture1, Texture2D texture2)
        {
            this.texture1 = texture1;
            this.texture2 = texture2;
            location1 = new Rectangle(0, 0, 1080, 2340);
            location2 = new Rectangle(0, -2340, 1080, 2340);
        }

        public void Update()
        {
            if (location1.Y + 5 < 2340) location1.Y += 5;
            else location1.Y = -2340;

            if (location2.Y + 5 < 2340) location2.Y += 5;
            else location2.Y = -2340;
        }
        public void UpdateSlow()
        {
            if (location1.Y + 5 < 2340) location1.Y += 1;
            else location1.Y = -2340;

            if (location2.Y + 5 < 2340) location2.Y += 1;
            else location2.Y = -2340;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture1, location1, Color.White);
            spriteBatch.Draw(texture2, location2, Color.White);
        }
    }
}