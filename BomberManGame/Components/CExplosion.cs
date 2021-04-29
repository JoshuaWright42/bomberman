using System;
using System.Collections.Generic;
using System.Timers;

namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Explosion Component. Is a type of ITile. Any Explosion entity needs this component.
    /// Represents an explosion at current cell, will last for a given time than despawn.
    /// </summary>
    public class CExplosion: Component, ITile
    {
        /// <summary>
        /// Constructor. Doesn't do anything special.
        /// </summary>
        /// <param name="self">The Entity this component belongs too.</param>
        internal CExplosion(Entity self): base (self) { }

        /// <summary>
        /// Event handler to detect/know when the explosion has finished.
        /// Replaces itself with an Air/Background Tile.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void onComplete(object source, EventArgs e)
        {
            throw new NotImplementedException();
            //TODO: assign cell new air/background entity
        }

        /// <summary>
        /// Handles what happens if this tile/cell is instructed to explode mid-
        /// explosion. Essentially just overwrites itself with a new explosion entity.
        /// Passes explosion on to next cell if explosion not finished (size > 0).
        /// </summary>
        /// <param name="size">The current size of the explosion.</param>
        /// <param name="dir">Direction the explosion is travelling. -1 if at epicentre.</param>
        public void Explode(int size, int dir = -1)
        {
            throw new NotImplementedException();
            //TODO: assign cell new explostion entity
            if (size > 0) GetComponent<CLocation>().Location[dir].Data.Explode(size - 1, dir);
        }
    }
}
