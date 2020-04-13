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
        private int speed = 1;
        private bool previousArrowPressed;

        public Player(Texture2D texture)
        {
            this.playerTexture = texture;
        }

        public void UpdateMovement()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (playerLocation.X <= 0) speed = 0;
                playerLocation.X -= speed;
                if (previousArrowPressed == true) speed = 1;
                previousArrowPressed = false;
                speed ++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (playerLocation.X >= 600 - 50) speed = 0;
                playerLocation.X += speed;
                if (previousArrowPressed == false) speed = 1;
                previousArrowPressed = true;
                speed ++;
            }
        }
    }
}
