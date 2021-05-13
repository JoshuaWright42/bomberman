using System;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
    public interface IAffectPlayer
    {
        public void onCollide(CPlayer plr);
    }
}
