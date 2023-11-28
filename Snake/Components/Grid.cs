using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Components
{
    public class Grid
    {
        public Rectangle Dimensions;
        const int MARGIN = 2;
        public Cell[,] CellArray { get; set; }
        public Grid(int num_cols, int num_rows, Point parent_dimensions)
        {
            // Parameters for auto-fitting the cell size.
            int width = num_cols / parent_dimensions.X;
            int height = num_rows / parent_dimensions.Y;

            // Taking the smaller measurement. Since I'm using a funky operator, I give a tl;dr of what it does below.
            int cell_size = width <= height ? width : height; // This is the ternary operator. It allows you to do boolean assignment.

            // Initialize dimensions
            Dimensions.X = Dimensions.Y = MARGIN;
            Dimensions.Width = MARGIN + num_cols * cell_size + MARGIN;
            Dimensions.Height = MARGIN + num_rows * cell_size + MARGIN;

            // Create the cell array.
            CellArray = new Cell[num_cols, num_rows];
            for (int y = 0; y < num_rows; y++)
            {
                for (int x = 0; x < num_cols; x++)
                {
                    Rectangle cell_dimensions = new(Dimensions.X + cell_size * num_cols,
                                                    Dimensions.Y + cell_size * num_rows,
                                                    cell_size,
                                                    cell_size);
                    CellArray[x, y] = new(cell_dimensions, new Point(x, y));
                }
            }

        }
        public void ResetGrid()
        {
            foreach (Cell cell in CellArray)
            {
                cell.ResetCell();
            }
        }
        public void DrawGrid(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in CellArray)
            {
                cell.DrawCell(spriteBatch);
            }
        }
    }
}
