using System;
namespace BomberManGame
{
    public enum EntityType
    {
        Explosion,
        Bomb,
        Player,
        Air
    }

    public interface UIAdapter
    {
        public void DrawEntity(int x, int y, EntityType type);
        public void ProcessInput();
    }
}
