using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Enemy
    {
        public Texture2D texture;
        public Rectangle location;
        public int speed, health = 25;
        public bool isDestroyed = false;
        private static Random r = new Random();

        public Enemy(Texture2D texture, int column, int speed)
        {
            int x = 0;
            this.speed = speed;
            this.texture = texture;
            switch (column)
            {
                case 1:
                    x = 270;
                    break;
                case 2:
                    x = 540;
                    break;
                case 3:
                    x = 810;
                    break;
            }
            location = new Rectangle(x, -270, 270, 270);
        }

        public void Update()
        {
            if (location.Y <= 2500) location.Y += speed;
        }

        public bool isNextReady()
        {
            if (location.Y >= 50) return true;
            else return false;
        }

        public bool Hit(int damage)
        {
            if (location.Y > 100)
            {
                health -= damage;
                if (health <= 0) isDestroyed = true;
            }
            return isDestroyed;
        }
    }
}