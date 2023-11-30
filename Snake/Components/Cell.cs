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
        /// <summary>
        /// This is used only if a snake is in the cell. 0 represents the head, and will count up until the very end of the tail. -1 means no player is in the cell.
        /// </summary>
        public int WhoAmI { get; set; }
        public Point Coordinates { get; set; }
        public Cell(Rectangle dimensions, Point location)
        {
            WhoAmI = -1;
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
