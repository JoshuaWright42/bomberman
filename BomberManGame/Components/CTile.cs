using System;
namespace BomberManGame
{
    public class CTile: Component
    {
        public CTile(Cell location)
        {
            Location = location;
        }

        public Cell Location { get; set; }
    }
}
