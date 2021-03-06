using System;
using System.Collections.Generic;
using System.Timers;

namespace BomberManGame.EntityComponents
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
        internal CExplosion(Entity self): base (self)
        {
            EventPublisher.Instance.GetEvent<ECollisions>().Subscribe(onCollide);
        }

        public void onCollide(CPlayer plr)
        {
            if (UIAdapter.Instance.HasCollided(plr, this))
            {
                plr.Data.isDead = true;
            }
        }

        public override void Destroy()
        {
            EventPublisher.Instance.GetEvent<ECollisions>().Unsubscribe(onCollide);
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
