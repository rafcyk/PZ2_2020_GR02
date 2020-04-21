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
        public int speed = 5;
        public Texture2D hearthTexture;
        public int health = 3;
        public bool hearthVisible = true;

        public Player(Texture2D texture,Texture2D h)
        {
            this.playerTexture = texture;
            this.hearthTexture = h;
        }

        public void UpdateMovement()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (playerLocation.X <= 0) playerLocation.X += speed;
                playerLocation.X -= speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (playerLocation.X >= 600 - 50) playerLocation.X -= speed;
                playerLocation.X += speed;
            }
        }

        public void DrawHearths(SpriteBatch spriteBatch)
        {
            if (hearthVisible)
            {
                for (int i = 0; i < health; i++)
                {
                    spriteBatch.Draw(hearthTexture, new Rectangle(18 + i * 30, 30, 56, 48), Color.White);
                }
            }
        }

        public bool Hit()
        {
            health--;
            if (health <= 0) return true;
            else return false;
        }
    }
}
