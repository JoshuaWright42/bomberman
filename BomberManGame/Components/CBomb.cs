using System;
using System.Collections.Generic;
using System.Timers;

namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Bomb Component. Is a type of ITile. Any bomb entity needs this component.
    /// Represents a bomb at the current cell, will detonate after a given time OR
    /// when it comes into contact with another explosion.
    /// </summary>
    public class CBomb: Component, ITile
    {
        /// <summary>
        /// The size/radius of the explosion when detonated.
        /// </summary>
        private int Radius { get; set; }

        /// <summary>
        /// Constructor. Initialises the radius.
        /// </summary>
        /// <param name="self">The Entity this component belongs too.</param>
        /// <param name="rad">The size/radius of the explosion when detonated.</param>
        internal CBomb(Entity self, int rad): base (self) => Radius = rad;

        /// <summary>
        /// Event handler that is invoked once the bomb's "fuse" has run out.
        /// Instructs bomb to Explode.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void onExplode(object source, EventArgs e) => Explode(Radius);

        /// <summary>
        /// Handles what happense when this bomb is instructed to explode.
        /// SCENARIO 1: The bomb has detonated, triggers explosion in every direction
        /// of given size.
        /// SCENARIO 2: Another explosion has come into contact with this bomb.
        /// Detonates prematurely.
        /// </summary>
        /// <param name="size">The current size of the explosion.</param>
        /// <param name="dir">Direction the explosion is travelling. -1 if at epicentre.</param>
        public void Explode(int size, int dir = -1)
        {
            //assign cell new explosion entity
            CDraw pos = GetComponent<CDraw>();
            Entity exp = EntityFactory.Instance.CreateExplosion(pos.X, pos.Y);
            GetComponent<CLocation>().Location.Data = (ITile)exp.GetComponent<CExplosion>();

            if (size > 0) // has the explosion finished?
            {
                Cell location = GetComponent<CLocation>().Location; //get current cell

                //Send exlosion in every direction
                foreach (Cell c in location)
                {
                    if (location.IndexOf(c) == dir) //bomb was triggered by another bomb
                    {
                        //Syntax for '?' operator:
                        // (condition) ? (result if true): (result if false)
                        //Passes on the bigger of the two explosions in this direction.
                        c.Data.Explode((size > Radius ? size : Radius) - 1);
                    }
                    else
                    {
                        c.Data.Explode(Radius - 1); //Sends explosion of the bombs size in this direciton
                    }
                }
            }
        }
    }
}
