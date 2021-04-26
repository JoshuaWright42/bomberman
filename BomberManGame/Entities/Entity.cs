using System;
using System.Collections.Generic;
using System.Linq;

namespace BomberManGame
{
    public abstract class Entity
    {
        private Dictionary<Type, Component> Components { get; init; }

        public Entity(Dictionary<Type, Component> comps)
        {
            Components = comps;
        }

        public Component this[Type t] { get => Components[t]; }
    }
}
