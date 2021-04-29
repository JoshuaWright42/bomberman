using System;
using System.Collections;
using System.Collections.Generic;

namespace BomberManGame
{
    /// <summary>
    /// Class that represents a single cell in a 2D Doubly Linked List.
    /// A Cell knows who it's neighbouring cells are in each of 4 directions.
    /// Carries a payload of type ITile.
    /// </summary>
    public class Cell : IEnumerable<Cell>
    {
        public ITile Data { get; set; }
        public Cell Left { get; set; }
        public Cell Right { get; set; }
        public Cell Up { get; set; }
        public Cell Down { get; set; }

        public Cell(int x, int y)
        {
            //needs implementation once Tile data type has been created
        }

        /// <summary>
        /// Readonly indexer which allows using an index to access the different neighbouring Cells.
        /// 0: Left
        /// 1: Right
        /// 2: Up
        /// 3: Down
        /// </summary>
        /// <param name="i">The desired index/Cell.</param>
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
            foreach (Cell n in this)
            {
                if (n == value) return result;
                result++;
            }
            throw new ArgumentException("Given value does not match any cells in Cell!");
        }

        /// <summary>
        /// Enumerater for Cell. Makes it possible to use foreach on a Cell object.
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
}
