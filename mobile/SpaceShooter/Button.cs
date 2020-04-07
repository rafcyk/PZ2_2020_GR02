using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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
    }
}