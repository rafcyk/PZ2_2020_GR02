using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public void UpdateMovement()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerLocation.X -= 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerLocation.X += 5;
            }
        }
    }
}
