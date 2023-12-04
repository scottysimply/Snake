using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Components
{
    public class DialogBox
    {
        public Rectangle Border;
        public string Text;
        public DialogBox(Rectangle dimensions, string text)
        {
            Text = text;
            Border = dimensions;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetList.BlankSquare, Border, Color.MediumSlateBlue);
            Vector2 text_offset = AssetList.ArialLarge.MeasureString(Text);
            spriteBatch.DrawString(AssetList.ArialLarge, Text, Border.Center.ToVector2() - text_offset / 2, Color.WhiteSmoke);
        }
    }
}
