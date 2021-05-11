using System;
namespace BomberManGame
{
    /// <summary>
    /// Generic datatype for an Event.
    /// These events drive everything that happens in the game.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Constructor. Simply calls Register().
        /// </summary>
        public Event() { }

        /// <summary>
        /// Registers the event with the EventPublisher.
        /// Can be overriden.
        /// </summary>
        public virtual void Register() => EventPublisher.Instance.Register(this.GetType(), this);
    }
}
