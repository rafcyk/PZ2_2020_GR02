﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Button
    {
        public Texture2D texture;
        public Texture2D mainTexture;
        public Texture2D pressedTexture;
        public Rectangle location;

        public Button(Texture2D mainTexture, Texture2D pressedTexture, Rectangle location)
        {
            this.mainTexture = mainTexture;
            this.pressedTexture = pressedTexture;
            this.location = location;

            texture = mainTexture;
        }

        public void press()
        {
            texture = pressedTexture;
        }

        public void unpress()
        {
            texture = mainTexture;
        }
    }
}
