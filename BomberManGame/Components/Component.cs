using System;
using BomberManGame.Entities;
using BomberManGame.Interfaces;

namespace BomberManGame.Components
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
            (this as IAffectPlayer)?.Subscribe(); //if this component affects player, call it's subscribe method
        }

        public virtual void Destroy()
        {
            (this as IAffectPlayer)?.UnSubcribe(); //if this component affects player, call it's unsubscribe method
        }
    }
}
