using System;
using System.Collections;
using System.Collections.Generic;

namespace BomberManGame
{
    /// <summary>
    /// A quadruply linked node/cell. These make up the game map.
    /// Contains a nested class for the Nodes.
    /// </summary>
    public class Cell: Drawable
    {
        /// <summary>
        /// Nested class used to store each of the four nodes (left, right, up, down).
        /// </summary>
        public class Nodes: IEnumerable<Cell>
        {
            public Cell Left { get; set; }
            public Cell Right { get; set; }
            public Cell Up { get; set; }
            public Cell Down { get; set; }

            /// <summary>
            /// Readonly indexer which allows using an index to access the different nodes.
            /// 0: Left
            /// 1: Right
            /// 2: Up
            /// 3: Down
            /// </summary>
            /// <param name="i">The desired index/node.</param>
            /// <returns></returns>
            public Cell this[int i]
            {
                get
                {
                    switch (i)
                    {
                        case 0: return Left;
                        case 1: return Right;
                        case 2: return Up;
                        case 3: return Down;
                        default: throw new IndexOutOfRangeException("Index must be within range from 0..3!");
                    }
                }
            }

            /// <summary>
            /// Will get the index of the given value. If there is one.
            /// Will throw exception if index is not found.
            /// </summary>
            /// <param name="value">The cell you wish to get the index of.</param>
            /// <returns></returns>
            public int IndexOf(Cell value)
            {
                int result = 0;
                foreach (Cell c in this)
                {
                    if (c == value) return result;
                    result++;
                }
                throw new ArgumentException("Given value does not match any cells in Node!");
            }

            /// <summary>
            /// Enumerater for Nodes. Makes it possible to use foreach on a Nodes object.
            /// </summary>
            /// <returns>Cell</returns>
            public IEnumerator<Cell> GetEnumerator()
            {
                for (int i = 0; i < 4; i++) yield return this[i];
            }

            IEnumerator IEnumerable.GetEnumerator() //requirement of IEnumerable interface
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// Used to access the Cell's nodes/neighbours.
        /// </summary>
        public Nodes Neighbours { get; init; }

        /// <summary>
        /// Used to instantiate a new cell.
        /// At the moment this only initialises Neighbours.
        /// </summary>
        /// <param name="x">Represents cell number on x axis.</param>
        /// <param name="y">Represents cell number on y axis</param>
        public Cell(int x, int y): base (x, y)
        {
            Neighbours = new();
        }
    }
}