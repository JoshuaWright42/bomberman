using System;
namespace BomberManGame.EntityComponents
{
    public class CSolid: Component
    {
        public CSolid(Entity self): base (self)
        {
            EventPublisher.Instance.GetEvent<EMove>().Subscribe(onPlayerMove);
        }

        public void onPlayerMove(CPlayer sender, Direction dir, ref bool success)
        {
            if (UIAdapter.Instance.HasCollided(sender, this))
            {
                Cell plrCell = sender.Self.GetComponent<CLocation>().Location;
                foreach (Cell c in plrCell)
                {
                    if (plrCell.IndexOf(c) == (int)dir)
                    {
                        Cell self = Self.GetComponent<CLocation>().Location;
                        if (c == self) success = false;
                        switch(dir)
                        {
                            case Direction.Left or Direction.Right:
                                if (!CheckVerticalCells(sender, c, self)) success = false;
                                break;
                            case Direction.Down or Direction.Up:
                                if (!CheckHorizontalCells(sender, c, self)) success = false;
                                break;
                        }
                    }
                }
            }
        }

        public override void Destroy()
        {
            EventPublisher.Instance.GetEvent<EMove>().UnSubscribe(onPlayerMove);
        }

        private bool CheckVerticalCells(CPlayer plr, Cell centre, Cell self)
        {
            if (centre.Up == self)
            {
                plr.Data.AbsoluteY += plr.Data.Speed;
                return false;
            }
            if (centre.Down == self)
            {
                plr.Data.AbsoluteY -= plr.Data.Speed;
                return false;
            }
            return true;
        }

        private bool CheckHorizontalCells(CPlayer plr, Cell centre, Cell self)
        {
            if (centre.Left == self)
            {
                plr.Data.AbsoluteX += plr.Data.Speed;
                return false;
            }
            if (centre.Right == self)
            {
                plr.Data.AbsoluteX -= plr.Data.Speed;
                return false;
            }
            return true;
        }
    }
}
