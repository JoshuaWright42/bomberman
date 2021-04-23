using System.Reflection.Metadata;
using System.Reflection;
using NUnit.Framework;
using BomberManGame;
using System;

namespace NUnitTests
{
    [TestFixture]
    public class MapTests
    {
        [SetUp]
        public void Setup()
        {
            Map.CreateNewInstance(100, 100);
        }

        /// <summary>
        /// Verifies that each node has been correctly created in our 2D linked list.
        /// </summary>
        /// <param name="cols">Number of columns there should be.</param>
        /// <param name="rows">Number of rows there should be.</param>
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(50, 50)]
        public void TestMapCreation(int cols, int rows)
        {
            Map.CreateNewInstance(cols, rows); //initialise
            int colCounter = 0; //counter to verify correct amount of columns

            //foreach column
            for (Cell col = Map.Instance.RootNode; col != null; col = col.Neighbours.Right)
            {
                colCounter++;
                int rowCounter = 0; //counter to verify correct amount of rows

                //foreach row in this column
                for (Cell row = col; row != null; row = row.Neighbours.Down)
                {
                    rowCounter++;
                }
                Assert.AreEqual(rows, rowCounter, $"Quadruply Linked List does not have correct number of rows in column {colCounter}!");
            }
            Assert.AreEqual(cols, colCounter, "Quadruply Linked List does not have correct number of columns!");
        }

        /// <summary>
        /// Verifies that an exception is thrown in the event a number less than 1 is given
        /// for columns or rows.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        [TestCase(-43, 20)]
        [TestCase(10, -3)]
        [TestCase(-13, -51)]
        [TestCase(0, 2)]
        [TestCase(5, 0)]
        [TestCase(0, 0)]
        public void TestInvalidMapCreation(int cols, int rows)
        {
            try
            {
                Map.CreateNewInstance(cols, rows);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<IndexOutOfRangeException>(e, "Wrong exception thrown!");
                return;
            }
            Assert.Fail("An exception was not thrown!");
        }

        /// <summary>
        /// Verifies that the indexer gets the correct cell for given values x and y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [TestCase(0, 0)]
        [TestCase(0, 20)]
        [TestCase(10, 0)]
        [TestCase(1, 1)]
        [TestCase(23, 67)]
        public void TestIndexer(int x, int y)
        {
            Cell actual = Map.Instance[x, y];
            Assert.AreEqual(x, actual.X, "Cell x value did not match given index!");
            Assert.AreEqual(y, actual.Y, "Cell y value did not match given index!");
        }

        /// <summary>
        /// Verifies that if invalid values are given an IndexOutOfRangeException is thrown.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [TestCase(102, 50)]
        [TestCase(12, 150)]
        [TestCase(-59, 50)]
        [TestCase(10, -5)]
        public void TestInvalidIndexes(int x, int y)
        {
            try
            {
                Cell actual = Map.Instance[x, y];
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf<IndexOutOfRangeException>(e, "Wrong exception thrown!");
                return;
            }
            Assert.Fail("An exception was not thrown!");
        }
    }
}