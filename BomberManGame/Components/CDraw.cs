using System;
namespace BomberManGame
{
    public class CDraw: Component
    {
        private float X { get; set; }
        private float Y { get; set; }

        public CDraw(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
