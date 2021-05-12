using System;

namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Generic type for a "Component".
    /// These make up what we call an "Entity".
    /// Modular by design, these represent a small "component" of what an
    /// Entity might need/want to do.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// A reference to the Entity that owns this component.
        /// </summary>
        public Entity Self { get; init; }

        /// <summary>
        /// Constructor. Assigns passed in Entity to Self.
        /// </summary>
        /// <param name="self">A reference to the Entity that owns this component.</param>
        internal Component(Entity self) => Self = self;

        public virtual void Destroy() { }

        /// <summary>
        /// Wrapper for GetComponent method in Entity
        /// </summary>
        /// <typeparam name="T">Desired component to fetch.</typeparam>
        /// <returns>The desired component.</returns>
        public T GetComponent<T>() where T : Component => Self.GetComponent<T>();

        public bool HasComponent<T>() where T : Component => Self.HasComponent<T>();

        public void RemoveComponent<T>() where T : Component => Self.RemoveComponent<T>();
    }
}
