using System;
using System.Collections.Generic;
using BomberManGame.Components;

namespace BomberManGame
{
    public class ECollisions: Event
    {
        private List<CPlayer> Players { get; init; }

        public delegate void OnCollide(CPlayer sender);
        private event OnCollide Effects;

        public ECollisions() => Players = new List<CPlayer>();

        public void AddPlayer(CPlayer plr) => Players.Add(plr);
        public void RemovePlayer(CPlayer plr) => Players.Remove(plr);

        public void Subscribe(OnCollide sub) => Effects += sub;
        public void Unsubscribe(OnCollide sub) => Effects -= sub;

        public void CheckCollisions()
        {
            Players.ForEach(plr => Effects?.Invoke(plr));

            List<CPlayer> toKill = new List<CPlayer>();
            foreach (CPlayer plr in Players)
            {
                if (plr.Data.isDead) toKill.Add(plr);
            }
            toKill.ForEach(plr => plr.Destroy());
        }
    }
}
