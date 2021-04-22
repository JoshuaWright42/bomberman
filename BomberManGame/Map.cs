using System;

namespace BomberManGame
{
    /// <summary>
    /// Contains and manages the map of the game. Uses singleton design pattern.
    /// Map uses a custom cell based design.
    /// Cells are a quadruply linked list
    /// AKA a doubly linked skip list
    /// AKA a 2D linked list
    /// </summary>
    public class Map: Drawable
    {
        /// <summary>
        /// Gets the only instance of Map.
        /// </summary>
        public static Map Instance { get; private set; }

        /// <summary>
        /// Generates a fresh instance of map and overwrites the existing one if there is one.
        /// </summary>
        /// <param name="cols">Number of columns the map should have.</param>
        /// <param name="rows">Number of rows the map should have.</param>
        public static void CreateNewInstance(int cols, int rows)
        {
            Instance = new Map(cols, rows);
        }

        /// <summary>
        /// Gets the root node of the map (the top left cell).
        /// </summary>
        public Cell RootNode { get; }

        /// <summary>
        /// Used to construct/generate a new instance of the map. Handles creation of all necessary cells/nodes.
        /// </summary>
        /// <param name="cols">Number of columns the map should have.</param>
        /// <param name="rows">Number of rows the map should have.</param>
        private Map(int cols, int rows): base (0, 0)
        {
            //Must have at least 1 column and 1 row
            if (cols <= 0 || rows <= 0)
            {
                throw new IndexOutOfRangeException("Provided values for Columns (cols) and Rows (rows) must be greater than 0.");
            }

            //Top-Left cell
            RootNode = new Cell();

            //Cell to the left of the current node
            Cell colNodeLeft = null;

            //Current cell/column
            Cell colNode = RootNode;

            //foreach column
            for (int col = 0; col < cols; col++)
            {
                //Cell above the current node
                Cell rowNodeUp = colNode;

                //assign the node to the left of current node if one exists
                Cell rowNodeLeft = colNodeLeft != null ? colNodeLeft.Neighbours.Down : null;

                //foreach row in current column
                for (int row = 1; row < rows; row++)
                {
                    //create new row/cell
                    Cell rowNode = new Cell();

                    //Link cell above with current cell
                    rowNodeUp.Neighbours.Down = rowNode;
                    rowNode.Neighbours.Up = rowNodeUp;

                    //Current cell becomes the cell "above" for next row/iteration
                    rowNodeUp = rowNode;

                    //if there is a cell to the left, link it
                    if (rowNodeLeft != null)
                    {
                        //links cell to the left with current cell
                        rowNodeLeft.Neighbours.Right = rowNode;
                        rowNode.Neighbours.Left = rowNodeLeft;

                        //Cell to the "left" becomes the next cell down ready for next row/iteration
                        rowNodeLeft = rowNodeLeft.Neighbours.Down;
                    }
                }

                //This code creates the next column, which we don't want to do if we are in the last column
                if (col < cols - 1)
                {
                    //Current column becomes the column to the left
                    colNodeLeft = colNode;

                    //Create new current column
                    colNode = new Cell();

                    //Link cell to the left with new current column
                    colNodeLeft.Neighbours.Right = colNode;
                    colNode.Neighbours.Left = colNodeLeft;
                }
            }
        }
    }
}
