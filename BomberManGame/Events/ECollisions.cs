using System;
using System.Collections.Generic;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
    public class ECollisions: Event
    {
        private List<CPlayer> Players { get; init; }

        public delegate void OnPlayerMove(CPlayer sender, Direction dir, ref bool success);
        private event OnPlayerMove Solids;

        public delegate void OnCollide(CPlayer sender);
        private event OnCollide Effects;

        public ECollisions()
        {
            Players = new List<CPlayer>();
        }

        public void AddPlayer(CPlayer plr)
        {
            Players.Add(plr);
        }

        public void RemovePlayer(CPlayer plr)
        {
            Players.Remove(plr);
        }

        public void Subscribe(OnPlayerMove sub) => Solids += sub;
        public void UnSubscribe(OnPlayerMove sub) => Solids -= sub;

        public void Subscribe(OnCollide sub) => Effects += sub;
        public void Unsubscribe(OnCollide sub) => Effects -= sub;

        public void TryPlayerMove(CPlayer plr, Direction dir)
        {
            bool successful = true;
            Solids?.Invoke(plr, dir, ref successful);
            if (successful) plr.Move(dir);
        }

        public void CheckCollisions()
        {
            foreach (CPlayer plr in Players)
            {
                Effects?.Invoke(plr);
            }
        }
    }
}
