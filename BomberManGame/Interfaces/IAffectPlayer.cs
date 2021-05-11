using System;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
    public interface IAffectPlayer
    {
        public void ApplyEffect(CPlayer player);
    }
}
