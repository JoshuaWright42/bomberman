using System;
using System.Collections.Generic;

namespace BomberManGame
{
    /// <summary>
    /// Generic data type for anything that can/should be drawn in the game.
    /// Responsible publisher for the Drawables/Draw event which instructs all subscribers to draw themselves.
    /// All Drawable objects are automatically subscribed to the Drawables event upon instantiation.
    /// Event publication is static.
    /// </summary>
    public class EventPublisher
    {
        private static EventPublisher _instance;

        private EventPublisher() { }

        public static EventPublisher Instance
        {
            get
            {
                if (_instance == null) _instance = new();
                return _instance;
            }
        }

        private Dictionary<Type, Event> _publishers = new();

        public void Register(Type type, Event pub)
        {
            _publishers.Add(type, pub);
        }

        public T GetEvent<T>() where T: Event
        {
            return (T)_publishers[typeof(T)];
        }
    }
}