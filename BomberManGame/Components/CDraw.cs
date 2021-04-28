using System;
namespace BomberManGame
{
    public class CDraw: Component
    {
        public float X { get; set; }
        public float Y { get; set; }

        public CDraw(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
