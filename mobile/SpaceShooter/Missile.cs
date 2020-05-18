using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Missile
    {
        public Texture2D texture;
        public Rectangle location;
        public int speed;
        public bool isDestroyed = false;

        public Missile(Texture2D texture, int location, int speed)
        {
            this.speed = speed;
            this.texture = texture;
            this.location = new Rectangle(location + 134, 2200, 32, 80);
        }

        public void Update()
        {
            if (location.Y >= -100) location.Y -= speed;
            else isDestroyed = true;
        }
        public void UpdateSlow()
        {
            if (location.Y >= -100) location.Y -= 1;
            else isDestroyed = true;
        }
        public bool isNextReady(int rate)
        {
            if (location.Y <= 2240 - rate) return true;
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!isDestroyed) spriteBatch.Draw(texture, location, Color.White);
        }
    }
}