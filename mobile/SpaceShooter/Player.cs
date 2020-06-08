using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceShooter
{
    class Player
    {
        public Texture2D texture;
        public Texture2D heartTexture;
        public Texture2D missileTexture;
        public int health = 3, missileSpeed = 10;
        public bool heartVisible = true;
        public Rectangle location;

        public Player(Texture2D texture, Rectangle location, Texture2D heartTexture, Texture2D missileTexture)
        {
            this.heartTexture = heartTexture;
            this.texture = texture;
            this.missileTexture = missileTexture;
            this.location = location;
        }

        public bool Hit()
        {
            health--;
            if (health <= 0) return true;
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.White);
        }

        public void DrawHearts(SpriteBatch spriteBatch)
        {
            if (heartVisible)
            {
                for (int i = 0; i < health; i++)
                {
                    spriteBatch.Draw(heartTexture, new Rectangle(18 + i * 50, 50, 150, 128), Color.White);
                }
            }
        }
    }
}