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
        internal Component(Entity self)
        {
            Self = self;
        }

        public virtual void Destroy()
        {
        }
    }
}
