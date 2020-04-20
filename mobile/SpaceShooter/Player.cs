using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceShooter
{
    class Player
    {
        public Texture2D texture;
        public Texture2D hearthTexture;
        public int health = 3;
        public bool hearthVisible = true;
        public Rectangle location;
        public int coins;

        public Player(Texture2D texture, Rectangle location, Texture2D hearthTexture)
        {
            this.hearthTexture = hearthTexture;
            this.texture = texture;
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

        public void DrawHearths(SpriteBatch spriteBatch)
        {
            if (hearthVisible)
            {
                for (int i = 0; i < health; i++)
                {
                    spriteBatch.Draw(hearthTexture, new Rectangle(18 + i * 50, 50, 150, 128), Color.White);
                }
            }
        }
    }
}