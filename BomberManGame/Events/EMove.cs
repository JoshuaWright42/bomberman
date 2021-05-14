using System;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
    public class EMove: Event
    {
        public delegate void OnPlayerMove(CPlayer sender, Direction dir, ref bool success);
        private event OnPlayerMove Solids;

        public void Subscribe(OnPlayerMove sub) => Solids += sub;
        public void UnSubscribe(OnPlayerMove sub) => Solids -= sub;

        public void TryPlayerMove(CPlayer plr, Direction dir)
        {
            bool successful = true;
            Solids?.Invoke(plr, dir, ref successful);
            if (successful) plr.Move(dir);
        }
    }
}
