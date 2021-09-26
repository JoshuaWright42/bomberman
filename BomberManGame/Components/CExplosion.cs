using System;
using System.Collections.Generic;
using System.Timers;
using BomberManGame.Entities;
using BomberManGame.Interfaces;

namespace BomberManGame.Components
{
    /// <summary>
    /// Explosion Component. Is a type of ITile. Any Explosion entity needs this component.
    /// Represents an explosion at current cell, will last for a given time than despawn.
    /// </summary>
    public class CExplosion: Component, ITile, IAffectPlayer
    {
        /// <summary>
        /// Constructor. Doesn't do anything special.
        /// </summary>
        /// <param name="self">The Entity this component belongs too.</param>
        public CExplosion(Entity self): base (self)
        {
            
        }

        public void onCollide(CPlayer plr)
        {
            if (UIAdapter.Instance.HasCollided(plr, this))
            {
                plr.Data.isDead = true;
            }
        }

        /// <summary>
        /// Event handler to detect/know when the explosion has finished.
        /// Replaces itself with an Air/Background Tile.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void onComplete(object source, EventArgs e)
        {
            //assign cell new air entity
            CDraw pos = Self.GetComponent<CDraw>();
            Entity air = EntityFactory.Instance.CreateAir(pos.X, pos.Y);
            Self.GetComponent<CLocation>().Location.Data = (ITile)air.GetComponent<CAir>();
        }
    }
}
