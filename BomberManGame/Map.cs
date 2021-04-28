using System;
namespace BomberManGame
{
    /// <summary>
    /// Singleton. Consists of a 2D doubly linked list that represents
    /// the entire map.
    /// </summary>
    public class Map
    {
        private int Columns { get; init; } //number of columns
        private int Rows { get; init; } //number of rows

        /// <summary>
        /// Recursive method that Constructs the cells for each column.
        /// When this method is complete, the 2D Doubly Linked list has been
        /// fully created.
        /// </summary>
        /// <param name="col">Indexer for Columns</param>
        /// <param name="row">Indexer for Row</param>
        /// <param name="left">The previous column</param>
        /// <returns></returns>
        private Cell ConstructColumn(int col, int row, Cell left)
        {
            //reached right border, terminate recursion
            if (col >= Columns) return null;

            Cell newCell = new(col, row);
            newCell.Left = left;

            //Begin constructing each row for this column
            newCell.Down = ConstructRow(col, row + 1, left?.Down, newCell);

            //Construct next column
            newCell.Right = ConstructColumn(col + 1, row, newCell);

            //Column complete
            return newCell;
        }

        /// <summary>
        /// Recursive method that constructs the cells for each row.
        /// When this method is complete, a single column has been
        /// fully created.
        /// </summary>
        /// <param name="col">Indexer for Column</param>
        /// <param name="row">Indexer for Row</param>
        /// <param name="left">The previous Column</param>
        /// <param name="above">The row above</param>
        /// <returns></returns>
        private Cell ConstructRow(int col, int row, Cell left, Cell above)
        {
            //reached bottom border, terminate recursion
            if (row >= Rows) return null;

            Cell newCell = new(col, row);
            newCell.Up = above;
            newCell.Left = left;

            //if there is a cell to the left, assign right cell to current cell
            if (left != null) left.Right = newCell;

            //continue constructing rows
            newCell.Down = ConstructRow(col, row + 1, left?.Down, newCell);

            //rows/columns are complete
            return newCell;
        }

        /// <summary>
        /// Constructs a new map.
        /// </summary>
        /// <param name="cols">How many columns the map will have.</param>
        /// <param name="rows">How many rows the map will have.</param>
        private Map(int cols, int rows)
        {
            if (cols <= 0 || rows <= 0)
            {
                throw new IndexOutOfRangeException("Provided values for Columns (cols) and Rows (rows) must be greater than 0.");
            }
            Columns = cols;
            Rows = rows;
            RootCell = ConstructColumn(0, 0, null);
        }

        public Cell RootCell { get; init; } //Top-Left cell of 2D doubly linked list.
        public static Map Instance { get; private set; } //Single instance of Map

        /// <summary>
        /// Creates a new instance of the map.
        /// </summary>
        /// <param name="cols">How many columns the map will have.</param>
        /// <param name="rows">How many rows the map will have.</param>
        public static void CreateNewInstance(int cols, int rows)
        {
            Instance = new(cols, rows);
        }

        /// <summary>
        /// Indexer that gets a specific cell from the map.
        /// </summary>
        /// <param name="x">Column of desired cell.</param>
        /// <param name="y">Row of desired cell.</param>
        /// <returns></returns>
        public Cell this[int x, int y]
        {
            get
            {
                Cell result = RootCell;
                for (int i = 0; i < (uint)x; i++) //find column
                {
                    result = result.Right;
                    if (result == null) throw new IndexOutOfRangeException("Provided x index is out of range.");
                }
                for (int j = 0; j < (uint)y; j++) //find row
                {
                    result = result.Down;
                    if (result == null) throw new IndexOutOfRangeException("Provided y index is out of range.");
                }
                return result;
            } 
        }
    }
}