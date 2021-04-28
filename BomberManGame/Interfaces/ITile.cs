using System;
namespace BomberManGame
{
    public interface ITile
    {
        public void Explode(int size, int dir = -1);
    }
}
