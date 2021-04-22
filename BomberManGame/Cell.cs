using System;

namespace BomberManGame
{
    /// <summary>
    /// A quadruply linked node/cell. These make up the game map.
    /// Contains a nested class for the Nodes.
    /// </summary>
    public class Cell//: Drawable (need splashkit adapter before we can implement)
    {
        /// <summary>
        /// Nested class used to store each of the four nodes (left, right, up, down).
        /// </summary>
        public class Nodes
        {
            public Cell Left { get; set; }
            public Cell Right { get; set; }
            public Cell Up { get; set; }
            public Cell Down { get; set; }
        }

        /// <summary>
        /// Used to access the Cell's nodes/neighbours.
        /// </summary>
        public Nodes Neighbours { get; }

        /// <summary>
        /// Used to instantiate a new cell.
        /// At the moment this only initialises Neighbours.
        /// </summary>
        public Cell()//float x, float y): base (x, y) (need splashkit adapter before we can implement)
        {
            Neighbours = new Nodes();
        }
    }
}