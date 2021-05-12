using System;
namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Brick Component. Is a type of tile. Represents a solid wall/cell.
    /// Players and explosions cannot pass through this.
    /// </summary>
    public class CBrick: Component, ITile, ISolid
    {
        /// <summary>
        /// Default component constructor.
        /// </summary>
        /// <param name="self"></param>
        public CBrick(Entity self): base (self) { }

        /// <summary>
        /// Overrides default implementation in ITile interface.
        /// Stops explosion.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="dir"></param>
        public void Explode(int size, int dir = -1)
        {
            //Intentionally does nothing, therfore stopping the explosion.
        }
    }
}
