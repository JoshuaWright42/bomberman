using System;
using System.Collections.Generic;
using System.Timers;

namespace BomberManGame.Entities
{
    public class Explosion: Entity, IDraw, ITile
    {
        private Timer _timer;

        public Explosion(int length, Dictionary<Type, Component> comps): base (comps)
        {
            _timer = new Timer(length);
            _timer.Elapsed += new ElapsedEventHandler(onComplete);
            _timer.AutoReset = false;
            _timer.Start();
        }

        private void onComplete(object source, EventArgs e)
        {
            throw new NotImplementedException();
            //TODO: assign cell new air/background entity
        }

        public void Explode(int size, int dir = -1)
        {
            _timer.Stop();
            _timer.Start();
            GetComponent<CTile>().Location[dir].Data.Explode(size - 1, dir);
        }
    }
}
