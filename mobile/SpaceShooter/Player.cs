using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Player
    {
        public Texture2D texture;
        public Texture2D hearthTexture;
        public int health = 3;
        public bool hearthVisible = true;

        public Player(Texture2D texture, Texture2D hearthTexture)
        {
            this.hearthTexture = hearthTexture;
            this.texture = texture;
        }

        public bool Hit()
        {
            health--;
            if (health <= 0) return true;
            else return false;
        }
    }
}