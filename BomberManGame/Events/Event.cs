using System;
namespace BomberManGame
{
    public abstract class Event
    {
        public Event()
        {
            Register();
        }

        public virtual void Register()
        {
            EventPublisher.Instance.Register(this.GetType(), this);
        }
    }
}
