using System;

namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Air Component. Is a type of tile. Represents an empty cell.
    /// Doesn't do anything special really.
    /// </summary>
    public class CAir: Component, ITile
    {
        /// <summary>
        /// Default component constructor.
        /// </summary>
        /// <param name="self"></param>
        public CAir(Entity self): base (self)
        {
        }
    }
}
