using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Button
    {
        public string Name;
        public Texture2D texture;
        public Texture2D mainTexture;
        public Texture2D pressedTexture;
        public Rectangle location;
        public bool isPressed = false;

        public Button(String name, Texture2D mainTexture, Texture2D pressedTexture, Rectangle location)
        {
            this.Name = name;
            this.mainTexture = mainTexture;
            this.pressedTexture = pressedTexture;
            this.location = location;

            texture = mainTexture;
        }

        public void press()
        {
            isPressed = true;
            texture = pressedTexture;
        }

        public void unpress()
        {
            isPressed = false;
            texture = mainTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.White);
        }
    }
}