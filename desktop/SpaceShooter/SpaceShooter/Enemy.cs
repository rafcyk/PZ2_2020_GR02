using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Enemy
    {
        public Texture2D enemyTexture;
        public Rectangle enemyLocation;
        public int speed;
        public bool isDestroyed;

        public Enemy(Texture2D texture, Rectangle rec, int speed)
        {
            this.enemyTexture = texture;
            this.enemyLocation = rec;
            this.speed = speed;
        }

        public void Update()
        {
            enemyLocation.Y += speed;
        }

        public bool isNextReady(int rate)
        {
            if (enemyLocation.Y >= 0 + rate) return true;
            else return false;
        }
    }
}
