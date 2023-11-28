using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Components
{
    public class Cell
    {
        public Rectangle Dimensions;
        /// <summary>
        /// The ID of the cell. See <see cref="LogicIDs"/>.
        /// </summary>
        public LogicIDs ID { get; set; }
        public Point Coordinates { get; set; }
        public Cell(Rectangle dimensions, Point location)
        {
            Dimensions = dimensions;
            Coordinates = location;
            ResetCell();
        }
        public void ResetCell()
        {
            ID = LogicIDs.Empty;
        }
        public void DrawCell(SpriteBatch spriteBatch)
        {
            switch (ID)
            {
                case LogicIDs.Apple:
                    spriteBatch.Draw(TextureList.TApple, Dimensions, Color.White);
                    break;
                case LogicIDs.Empty:
                    spriteBatch.Draw(TextureList.TBlankCell, Dimensions, Color.White);
                    break;
                default:
                    spriteBatch.Draw(TextureList.TSnake, Dimensions, Color.White);
                    break;
            }
        }
    }
}
