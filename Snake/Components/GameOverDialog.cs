using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Components
{
    public class GameOverDialog
    {
        Rectangle Border;
        string Text;
        public GameOverDialog(Rectangle dimensions, string text)
        {
            Text = text;
            Border = dimensions;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
