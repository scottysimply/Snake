using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Components
{
    public class Grid
    {
        public Cell[,] CellArray { get; set; }
        public Grid(int num_cols, int num_rows)
        {
            CellArray = new Cell[num_cols, num_rows]; 
        }
        public void ResetGrid()
        {
            foreach (Cell cell in CellArray)
            {
                cell
            }
        }
        public void 
    }
}
