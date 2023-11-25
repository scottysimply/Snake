using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Components
{
    public class Cell
    {
        /// <summary>
        /// The ID of the cell. See <see cref="LogicIDs"/>.
        /// </summary>
        public LogicIDs ID { get; set; }
        /// <summary>
        /// The direction the cell is facing. For the snake, the drawing will be done back to front; from the tail to the head. See <see cref="Direction"/>.
        /// </summary>
        public Direction CellDirection { get; set; }
        public Point Coordinates { get; set; }
        public Vector2 Position { get; set; }
        public void ResetCell()
        {
            ID = 0;
            CellDirection = Direction.North;
        }
    }
}
