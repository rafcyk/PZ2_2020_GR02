using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Enemy
    {
        public Texture2D enemyTexture;
        public Rectangle enemyLocation;
        public int speed,health = 25;
        public bool isDestroyed;

        public Enemy(Texture2D texture1, Texture2D texture2, Texture2D texture3, Texture2D texture4, Texture2D texture5, Rectangle rec, int speed, EnemyWave actualWave)
        {
            this.enemyLocation = rec;
            this.speed = speed;

            switch (actualWave)
            {
                case EnemyWave.First:
                    health = 25;
                    enemyTexture = texture1;
                    break;
                case EnemyWave.Second:
                    health = 50;
                    enemyTexture = texture2;
                    break;
                case EnemyWave.Third:
                    health = 75;
                    enemyTexture = texture3;
                    break;
                case EnemyWave.Fourth:
                    health = 100;
                    enemyTexture = texture4;
                    break;
                case EnemyWave.Fifth:
                    health = 125;
                    enemyTexture = texture5;
                    break;
            }
        }

        public void Update()
        {
            enemyLocation.Y += speed;
        }

        public bool isNextReady()
        {
            if (enemyLocation.Y >= 100) return true;
            else return false;
        }

        public bool Hit(int damage)
        {
            if (enemyLocation.Y > 0)
            {
                health -= damage;
                if (health <= 0) isDestroyed = true;
            }
            return isDestroyed;
        }
    }
}
