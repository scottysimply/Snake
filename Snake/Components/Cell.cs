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

            if (ID == LogicIDs.Apple)
            {
                spriteBatch.Draw(AssetList.TApple, Dimensions, Color.White);
            }
            else if (WhoAmI > 0)
            {
                spriteBatch.Draw(AssetList.TSnakeBody, Dimensions, Color.White);
            }
            else if (WhoAmI == 0)
            {
                switch (ID)
                {
                    case LogicIDs.SnakeHeadSouth:
                        spriteBatch.Draw(AssetList.TSnakeHeadSouth, Dimensions, Color.White);
                        break;
                    case LogicIDs.SnakeHeadEast:
                        spriteBatch.Draw(AssetList.TSnakeHeadEast, Dimensions, Color.White);
                        break;
                    case LogicIDs.SnakeHeadWest:
                        spriteBatch.Draw(AssetList.TSnakeHeadWest, Dimensions, Color.White);
                        break;
                    default:
                        spriteBatch.Draw(AssetList.TSnakeHeadNorth, Dimensions, Color.White);
                        break;
                }
            }
            spriteBatch.Draw(AssetList.TBlankCell, Dimensions, Color.White);
        }
    }
}
