using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Missile
    {
        public Texture2D missileTexture;
        public Rectangle missileLocation;
        public int speed;
        public bool isDestroyed;

        public Missile(Texture2D texture, Rectangle rec, int speed) {
            this.missileTexture = texture;
            this.missileLocation = rec;
            this.speed = speed;
        }

        public void Update() {
            missileLocation.Y -= speed;
            if (missileLocation.Y < 0 ) isDestroyed = true;
        }

        public bool isNextReady(int rate)
        {
            if (missileLocation.Y <= 800 - rate) return true;
            else return false;
        }
    }
}
