using System;
using System.Collections.Generic;
using System.Timers;

namespace BomberManGame
{
    public class Bomb: Entity, IDraw, ITile
    {
        private int Radius { get; set; }

        public Bomb(int rad, int fuse, Dictionary<Type, Component> comps) : base(comps)
        {
            Radius = rad;

            Timer timer = new Timer(fuse);
            timer.Elapsed += new ElapsedEventHandler(onExplode);
            timer.AutoReset = false;
            timer.Start();
        }

        private void onExplode(object source, EventArgs e)
        {
            Explode(Radius);
        }

        public void Explode(int size, int dir = -1)
        {
            throw new NotImplementedException();
            //TODO: assign cell result of asking factory for explosion entitiy
            if (Radius > 0)
            {
                Cell location = GetComponent<CTile>().Location;
                foreach (Cell c in location)
                {
                    if (location.IndexOf(c) == dir)
                    {
                        //Syntax for '?' operator:
                        // (condition) ? (result if true): (result if false)
                        c.Data.Explode((size > Radius ? size : Radius) - 1);
                    }
                    else
                    {
                        c.Data.Explode(Radius - 1);
                    }
                }
            }
        }
    }
}
