using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Components
{
    public class Grid
    {
        public Rectangle Dimensions;
        const int MARGIN = 2;
        public Cell[,] CellArray { get; set; }
        public Point Size { get; set; }
        public Grid(int num_cols, int num_rows, Point parent_size)
        {
            // Parameters for auto-fitting the cell size.
            int width = parent_size.X / num_cols;
            int height = parent_size.Y / num_rows;

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
                    Rectangle cell_dimensions = new(Dimensions.X + cell_size * x,
                                                    Dimensions.Y + cell_size * y,
                                                    cell_size,
                                                    cell_size);
                    CellArray[x, y] = new(cell_dimensions, new Point(x, y));
                }
            }
            Size = new(num_cols, num_rows);
        }
        public void ClearGrid()
        {
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    CellArray[x, y].ID = 0;
                    CellArray[x, y].WhoAmI = -1;
                }
            }
        }
        public Point SpawnSnake()
        {
            int spawnX = Size.X / 2 + 1 - 4;
            int centerY = Size.Y / 2 + 1;

            CellArray[spawnX - 1, centerY].ID = LogicIDs.SnakeHeadEast;
            CellArray[spawnX - 1, centerY].WhoAmI = 0;

            CellArray[spawnX - 2, centerY].ID = LogicIDs.SnakeBody;
            CellArray[spawnX - 2, centerY].WhoAmI = 1;

            CellArray[spawnX - 3, centerY].ID = LogicIDs.SnakeBody;
            CellArray[spawnX - 3, centerY].WhoAmI = 2;

            return CellArray[spawnX - 1, centerY].Coordinates;
        }
        public void SpawnAnApple(Random random)
        {
            List<Point> grid_points = new();
            foreach (Cell cell in CellArray)
            {
                if (cell.ID == LogicIDs.Empty)
                {
                    grid_points.Add(cell.Coordinates);
                }
            }
            int index = random.Next(0, grid_points.Count);

            SetIDAtPosition(grid_points[index], LogicIDs.Apple);

        }
        /// <summary>
        /// Retrieves the ID at a given position.
        /// </summary>
        /// <param name="position">The position to retrieve the ID of.</param>
        /// <returns></returns>
        public LogicIDs GetIDAtPosition(Point position)
        {
            return CellArray[position.X, position.Y].ID;
        }
        /// <summary>
        /// Given a position in the grid, set the ID at that position to the given ID.
        /// </summary>
        /// <param name="position">The position in the grid to set.</param>
        /// <param name="ID">The ID to set at the given position.</param>
        public void SetIDAtPosition(Point position, LogicIDs ID)
        {
            CellArray[position.X, position.Y].ID = ID;
        }
        /// <summary>
        /// Retrieves the WhoAmI at a given position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int GetWhoAmIAtPosition(Point position)
        {
            return CellArray[position.X, position.Y].WhoAmI;
        }
        /// <summary>
        /// Given a position in the grid, set the WhoAmI at that position to the given WhoAmI.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="whoAmI"></param>
        public void SetWhoAmIAtPosition(Point position, int whoAmI)
        {
            CellArray[position.X, position.Y].WhoAmI = whoAmI;
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
        /// <summary>
        /// Recursively finds the tail of the snake. Returns the <see cref="Point"/> of the tail location
        /// </summary>
        /// <param name="position">The position to begin checking from.</param>
        /// <returns></returns>
        public Point PathFind(Point starting_position)
        {
            bool found_tail = false;
            Point previously_checked_position = starting_position;
            int previously_checked_whoAmI = 0;
            Point currently_checking_position;
            int currently_checking_whoAmI;

            while (!found_tail)
            {
                // Begin by checking north. Surrounded in a check to make sure this is in range!
                if (previously_checked_position.Y > 0)
                {
                    currently_checking_position = new(previously_checked_position.X, previously_checked_position.Y - 1);
                    currently_checking_whoAmI = GetWhoAmIAtPosition(currently_checking_position);
                    if (previously_checked_whoAmI == currently_checking_whoAmI)
                    {
                        currently_checking_whoAmI++;
                        SetWhoAmIAtPosition(currently_checking_position, currently_checking_whoAmI);
                        previously_checked_position = currently_checking_position;
                        previously_checked_whoAmI = currently_checking_whoAmI;
                        continue;
                    }
                }
                // Check south. Surrounded in a check to make sure this is in range!
                if (previously_checked_position.Y + 1 < Size.Y)
                {
                    currently_checking_position = new(previously_checked_position.X, previously_checked_position.Y + 1);
                    currently_checking_whoAmI = GetWhoAmIAtPosition(currently_checking_position);
                    if (previously_checked_whoAmI == currently_checking_whoAmI)
                    {
                        currently_checking_whoAmI++;
                        SetWhoAmIAtPosition(currently_checking_position, currently_checking_whoAmI);
                        previously_checked_position = currently_checking_position;
                        previously_checked_whoAmI = currently_checking_whoAmI;
                        continue;
                    }
                }
                // Check west. Surrounded in a check to make sure this is in range!
                if (previously_checked_position.X > 0)
                {
                    currently_checking_position = new(previously_checked_position.X - 1, previously_checked_position.Y);
                    currently_checking_whoAmI = GetWhoAmIAtPosition(currently_checking_position);
                    if (previously_checked_whoAmI == currently_checking_whoAmI)
                    {
                        currently_checking_whoAmI++;
                        SetWhoAmIAtPosition(currently_checking_position, currently_checking_whoAmI);
                        previously_checked_position = currently_checking_position;
                        previously_checked_whoAmI = currently_checking_whoAmI;
                        continue;
                    }
                }
                // Check east. Surrounded in a check to make sure this is in range!
                if (previously_checked_position.X + 1 < Size.X)
                {
                    currently_checking_position = new(previously_checked_position.X + 1, previously_checked_position.Y);
                    currently_checking_whoAmI = GetWhoAmIAtPosition(currently_checking_position);
                    if (previously_checked_whoAmI == currently_checking_whoAmI)
                    {
                        currently_checking_whoAmI++;
                        SetWhoAmIAtPosition(currently_checking_position, currently_checking_whoAmI);
                        previously_checked_position = currently_checking_position;
                        previously_checked_whoAmI = currently_checking_whoAmI;
                        continue;
                    }
                }
                // If no snake body was found, we found the end of the snake!
                found_tail = true;
            }
            return previously_checked_position;
        }
    }
}
