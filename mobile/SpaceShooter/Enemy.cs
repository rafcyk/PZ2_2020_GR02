using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace SpaceShooter
{
    class Enemy
    {
        public Texture2D texture;
        public Rectangle location;
        public int speed, health = 25;
        public bool isDestroyed = false;
        public Enemy(List<Texture2D> textures, int column, int speed, EnemyWave actualWave)
        {
            int x = 0, i = 0;
            this.speed = speed;
            Random r = new Random();
            switch (actualWave)
            {
                case EnemyWave.First:
                    health = 25;
                    i = 0;
                    break;
                case EnemyWave.Second:
                    health = 50;
                    i = r.Next(0, 2);
                    break;
                case EnemyWave.Third:
                    health = 75;
                    i = r.Next(0, 3);
                    break;
                case EnemyWave.Fourth:
                    health = 100;
                    i = r.Next(0, 4);
                    break;
                case EnemyWave.Fifth:
                    health = 125;
                    i = r.Next(0, 5);
                    break;
            }

            texture = textures[i];

            switch (column)
            {
                case 1:
                    x = 270;
                    break;
                case 2:
                    x = 540;
                    break;
                case 3:
                    x = 810;
                    break;
            }
            location = new Rectangle(x, -270, 270, 270);
        }
        public void Update()
        {
            if (location.Y <= 2500) location.Y += speed;
            else isDestroyed = true;
        }
        public void UpdateSlow()
        {
            if (location.Y <= 2500) location.Y += 1;
            else isDestroyed = true;
        }
        public bool isNextReady()
        {
            if (location.Y >= 50) return true;
            else return false;
        }

        public bool Hit(int damage)
        {
            if (location.Y > 0)
            {
                health -= damage;
                if (health <= 0) isDestroyed = true;
            }
            return isDestroyed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!isDestroyed) spriteBatch.Draw(texture, location, Color.White);
        }
    }
}