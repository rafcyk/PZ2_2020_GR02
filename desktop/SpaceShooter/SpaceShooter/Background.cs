using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Background
    {
        public Texture2D backgroundTexture;
        public Rectangle location;
        public int speed = 1;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, location, Color.White);
        }
    }

    class Scrolling : Background
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            backgroundTexture = newTexture;
            location = newRectangle;
        }

        public void Update()
        {
            location.Y += speed;
        }
    }
}
