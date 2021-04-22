using System;

namespace BomberManGame
{
    public class Cell//: Drawable (need splashkit adapter before we can implement)
    {
        public class Nodes
        {
            public Cell Left { get; set; }
            public Cell Right { get; set; }
            public Cell Up { get; set; }
            public Cell Down { get; set; }
        }

        public Nodes Neighbours { get; }

        public Cell()//float x, float y): base (x, y) (need splashkit adapter before we can implement)
        {
            Neighbours = new Nodes();
        }
    }
}