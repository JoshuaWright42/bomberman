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

        public T GetComponent<T>() where T: Component
        {
            return (T)Components[typeof(T)];
        }
    }
}
