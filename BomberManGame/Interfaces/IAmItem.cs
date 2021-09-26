using BomberManGame.Components;
using BomberManGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberManGame.Interfaces
{
    public interface IAmItem
    {
        public void ReplaceSelfWithAir()
        {
            Entity self = ((Component)this).Self;
            Cell loc = self.GetComponent<CLocation>().Location;
            CDraw pos = self.GetComponent<CDraw>();
            loc.Data = EntityFactory.Instance.CreateAir(pos.X, pos.Y).GetComponent<CAir>();
        }
    }
}
