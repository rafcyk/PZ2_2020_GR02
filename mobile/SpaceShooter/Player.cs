using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Player
    {
        public Texture2D texture;
        public Texture2D hearthTexture;
        private Texture2D hearthMainTexture;
        private Texture2D hearthHitTexture;
        public int health = 3;
        public bool hearthVisible = true;

        public Player(Texture2D texture, Texture2D hearthMainTexture, Texture2D hearthHitTexture)
        {
            this.hearthMainTexture = hearthMainTexture;
            this.hearthHitTexture = hearthHitTexture;
            hearthTexture = this.hearthMainTexture;
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