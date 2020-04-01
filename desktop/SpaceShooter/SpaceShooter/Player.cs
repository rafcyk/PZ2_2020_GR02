using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Player
    {
        public Texture2D playerTexture;
        public Rectangle playerLocation;

        public Player(Texture2D texture)
        {
            this.playerTexture = texture;
        }
    }
}
