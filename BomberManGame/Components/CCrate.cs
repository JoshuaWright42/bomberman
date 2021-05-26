using System;

namespace BomberManGame.EntityComponents
{
    public class CCrate: Component, ITile
    {
        public CCrate(Entity self): base(self) { }

        public void Explode(int size, int dir = -1)
        {
            //assign cell new explosion entity
            Component compSelf = (Component)this;
            CDraw pos = compSelf.Self.GetComponent<CDraw>();

            ITile tile;
            if (Game.RNG.NextDouble() < Settings.ItemDropChance)
            {
                tile = EntityFactory.Instance.CreateExplosion(pos.X, pos.Y, onDestroyed).GetComponent<CExplosion>(); ;
            }
            else
            {
                tile = EntityFactory.Instance.CreateExplosion(pos.X, pos.Y).GetComponent<CExplosion>();
            }
            compSelf.Self.GetComponent<CLocation>().Location.Data = tile;
        }

        public void onDestroyed(object sender, EventArgs e)
        {
            Component compSelf = (Component)this;
            CDraw pos = compSelf.Self.GetComponent<CDraw>();
            ITile tile = EntityFactory.Instance.CreatePowerUp(pos.X, pos.Y);
            compSelf.Self.GetComponent<CLocation>().Location.Data = tile;
        }
    }
}
