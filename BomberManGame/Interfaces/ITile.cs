using System;
using BomberManGame.Components;
using BomberManGame.Entities;

namespace BomberManGame.Interfaces
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
        /// Default: Overwrites itself with a new explosion entity.
        /// Passes explosion on to next cell if explosion not finished (size > 0).
        /// </summary>
        /// <param name="size">Size of the explosion at his cell/tile.</param>
        /// <param name="dir">Direction the explosion is travelling. -1 if at epicentre.</param>
        public void Explode(int size, int dir = -1)
        {
            //assign cell new explosion entity
            Component compSelf = (Component)this;
            CDraw pos = compSelf.Self.GetComponent<CDraw>();
            if (compSelf.Self.HasComponent<CTimer>()) compSelf.Self.GetComponent<CTimer>().Stop();
            Entity exp = EntityFactory.Instance.CreateExplosion(pos.X, pos.Y);
            compSelf.Self.GetComponent<CLocation>().Location.Data = (ITile)exp.GetComponent<CExplosion>();

            //pass explosion onto next cell
            if (size > 0) compSelf.Self.GetComponent<CLocation>().Location[dir].Data.Explode(size - 1, dir);
        }
    }
}
