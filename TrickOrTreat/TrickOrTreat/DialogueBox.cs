using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrickOrTreat
{
    public class DialogueBox
    {
        public string Text { get; set; }
        public Rectangle Bounds;
        private Point _drawOffset;
        public DialogueBox(string text, Point window_size)
        {
            Text = text;
            Bounds.Size = _drawOffset = (Game1.Arial.MeasureString(text) * SCALE).ToPoint();
            Bounds.Location = new Point(window_size.X / 2 - Bounds.Width / 2, window_size.Y / 2 - Bounds.Height / 2);
        }
        const float SCALE = 4f;
        const int PADDING = 8;
        public void Draw(SpriteBatch spriteBatch, int timer)
        {
            float opacity = (timer - 75) / 400;
            spriteBatch.Draw(Game1.EmptyTexture, new Rectangle(Bounds.X - PADDING, Bounds.Y - PADDING, Bounds.Width + 2 * PADDING, Bounds.Height + 2 * PADDING), new Color(2, 13, 25));
            spriteBatch.Draw(Game1.EmptyTexture, Bounds, new Color(Color.DarkSlateGray, opacity));
            spriteBatch.DrawString(Game1.Arial, Text, Bounds.Location.ToVector2(), new(Color.White, opacity), 0f, new Vector2(0, 0), SCALE, SpriteEffects.None, 0);
        }
    }
}
