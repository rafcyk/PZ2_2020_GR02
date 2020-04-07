using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class GameplayBackground
    {
        public Texture2D texture;
        public Rectangle location;

        public GameplayBackground(Texture2D texture, Rectangle location)
        {
            this.texture = texture;
            this.location = location;
        }

        public void Update()
        {
            if (location.Y + 5 < 2340) location.Y += 5;
            else location.Y = -2340;
        }
    }
}