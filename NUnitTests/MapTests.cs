using System.Reflection.Metadata;
using System.Reflection;
using NUnit.Framework;
using BomberManGame;

namespace NUnitTests
{
    [TestFixture]
    public class MapTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(50, 50)]
        public void TestMapCreation(int cols, int rows)
        {
            Map.CreateNewInstance(cols, rows);
            int colCounter = 0;
            for (Cell col = Map.Instance.RootNode; col != null; col = col.Neighbours.Right)
            {
                colCounter++;
                int rowCounter = 0;
                for (Cell row = col; row != null; row = row.Neighbours.Down)
                {
                    rowCounter++;
                }
                Assert.AreEqual(rows, rowCounter, $"Quadruply Linked List does not have correct number of rows in column {colCounter}!");
            }
            Assert.AreEqual(cols, colCounter, "Quadruply Linked List does not have correct number of columns!");
        }
    }
}