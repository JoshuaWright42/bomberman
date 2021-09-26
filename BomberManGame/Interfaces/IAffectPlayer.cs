using System;
using BomberManGame.Components;

namespace BomberManGame.Interfaces
{
    public interface IAffectPlayer
    {
        public void onCollide(CPlayer plr);
        public void Subscribe()
        {
            EventPublisher.Instance.GetEvent<ECollisions>().Subscribe(onCollide);
        }

        public void UnSubcribe()
        {
            EventPublisher.Instance.GetEvent<ECollisions>().Unsubscribe(onCollide);
        }
    }
}
