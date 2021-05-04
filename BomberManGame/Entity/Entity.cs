using System;
using System.Collections.Generic;
using System.Linq;

namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Every thing in the game is an Entity.
    /// It is made up of a "bucket" of components.
    /// Assessmbled/Created by the EntityFactory exclusively.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// The components that make up this entity.
        /// </summary>
        private Dictionary<Type, Component> Components { get; init; }

        /// <summary>
        /// Consutructor. Initialises componnent dictionary.
        /// </summary>
        internal Entity() => Components = new Dictionary<Type, Component>();

        /// <summary>
        /// Adds a new component to the Entity.
        /// </summary>
        /// <typeparam name="T">Type of component it is.</typeparam>
        /// <param name="comp">The component itself.</param>
        public void AddComponent<T>(Component comp) where T: Component => Components.Add(typeof(T), comp);

        /// <summary>
        /// Get's the desired component from the dictionary.
        /// </summary>
        /// <typeparam name="T">The type of component desired.</typeparam>
        /// <returns>The component itself.</returns>
        public T GetComponent<T>() where T: Component => (T)Components[typeof(T)];
    }
}
