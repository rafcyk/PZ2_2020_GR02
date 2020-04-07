﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceShooter
{
    class Enemy
    {
        public Texture2D texture;
        public Rectangle location;
        public int speed, health = 25;
        public bool isDestroyed = false;
        EnemyWave wave;
        public Enemy(List<Texture2D> textures, int column, int speed, EnemyWave actualWave)
        {
            int x = 0;
            this.speed = speed;

            switch (actualWave)
            {
                case EnemyWave.First:
                    health = 25;
                    texture = textures[0];
                    break;
                case EnemyWave.Second:
                    health = 50;
                    texture = textures[1];
                    break;
                case EnemyWave.Third:
                    health = 75;
                    texture = textures[2];
                    break;
                case EnemyWave.Fourth:
                    health = 100;
                    texture = textures[3];
                    break;
                case EnemyWave.Fifth:
                    health = 125;
                    texture = textures[4];
                    break;
            }

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
        }

        public bool isNextReady()
        {
            if (location.Y >= 50) return true;
            else return false;
        }

        public bool Hit(int damage)
        {
            if (location.Y > 100)
            {
                health -= damage;
                if (health <= 0) isDestroyed = true;
            }
            return isDestroyed;
        }
    }
}