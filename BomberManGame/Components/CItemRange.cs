using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomberManGame.Entities;
using BomberManGame.Interfaces;

namespace BomberManGame.Components
{
    public class CItemRange: Component, ITile, IAffectPlayer, IAmItem
    {
        public CItemRange(Entity self) : base(self) { }

        public void onCollide(CPlayer plr)
        {
            if (UIAdapter.Instance.HasCollided(plr, this))
            {
                plr.Data.BombSize++;
                ((IAmItem)this).ReplaceSelfWithAir();
            }
        }
    }
}
