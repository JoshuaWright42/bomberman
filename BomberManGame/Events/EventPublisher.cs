using System;
using System.Collections.Generic;

namespace BomberManGame
{
    /// <summary>
    /// Responsible Event publisher/manager for all events in the game.
    /// Doesn't do much except act as a central hub for most events.
    /// Similar purpose but very different functionality to a "GameManager" style class.
    /// </summary>
    public class EventPublisher
    {
        ///                          ///
        /// SINGLETON IMPLEMENTATION /// More Info: https://www.oodesign.com/singleton-pattern.html
        ///                          ///

        /// <summary>
        /// One and only instance of this class.
        /// </summary>
        private static EventPublisher _instance;

        /// <summary>
        /// Constructor must be private since this is a singleton.
        /// </summary>
        private EventPublisher() { }

        /// <summary>
        /// Property for one and only instance. Creates a new instance if once does not exist.
        /// </summary>
        public static EventPublisher Instance
        {
            get
            {
                if (_instance == null) _instance = new();
                return _instance;
            }
        }

        ///                                   ///
        /// CLASS SPECIFIC DATA/FUNCTIONALITY ///
        ///                                   ///
        
        /// <summary>
        /// A collection of most active events.
        /// </summary>
        private Dictionary<Type, Event> _publishers = new();

        /// <summary>
        /// Registers a new event.
        /// </summary>
        /// <param name="type">Type of event it is.</param>
        /// <param name="pub">The event itself.</param>
        public void Register(Type type, Event pub) => _publishers.Add(type, pub);

        /// <summary>
        /// Gets the corresponding event for Event type 'T'
        /// </summary>
        /// <typeparam name="T">The type of desired event.</typeparam>
        /// <returns>The desired event.</returns>
        public T GetEvent<T>() where T: Event => (T)_publishers[typeof(T)];
    }
}