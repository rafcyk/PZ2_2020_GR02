using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class TossACoin
    {
        public Texture2D coinTexture;
        public Rectangle coinLocation;
        public bool isDestroyed;

        public TossACoin(Texture2D texture, Rectangle rec)
        {
            coinTexture = texture;
            this.coinLocation = rec;
        }

        public bool isNextReady()
        {
            if (coinLocation.Y >= 40) return true;
            else return false;
        }

        public void Update()
        {
            coinLocation.Y += 3;
            if (coinLocation.Y > 800) isDestroyed = true;
        }
    }  
}
