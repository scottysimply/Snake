using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrickOrTreat
{
    internal class Door
    {
        public int frame = 0;
        public int frameCount = 4;

        public Rectangle Bounds { get; set; }

        public Door(Point location, float scale)
        {
            Point size = new((int)(30 * scale), (int)(50 * scale));
            Bounds = new Rectangle(location.X - size.X / 2, location.Y - size.Y / 2, size.X, size.Y);
        }
        int clicks = 0;
        public bool opening { get; set; } = false;
        public int openingTimer { get; set; } = 0;
        public void OnClick()
        {
            if (clicks < 3)
            {
                clicks++;
            }
            if (clicks == 3)
            {
                opening = true;
            }
        }
        Texture2D texture { get => Game1.DoorTexture; }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (opening)
            {
                openingTimer++;
                if (openingTimer % 8 == 0 && frame < frameCount)
                {
                    frame++;
                }
            }
            spriteBatch.Draw(texture, Bounds, new(0, texture.Height / frameCount * frame , texture.Width, texture.Height / frameCount) , Color.White);
        }
    }
}
