using System;
namespace BomberManGame
{
    /// <summary>
    /// Mainly used as a way of identifying Entities that are Tiles.
    /// Does contain one polymorphic method.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// Any tile can be told to "explode" when it comes into contact
        /// with and explosion. Handled differently depending on Tile type.
        /// </summary>
        /// <param name="size">Size of the explosion at his cell/tile.</param>
        /// <param name="dir">Direction the explosion is travelling. -1 if at epicentre.</param>
        public void Explode(int size, int dir = -1);
    }
}
