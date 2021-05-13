using System;

namespace BomberManGame.EntityComponents
{
    public class CPlayer: Component
    {
        public record PlayerData
        {
            public float AbsoluteX, AbsoluteY, Speed;
            public bool isDead;
            public int BombSize, BombFuse, BombCount;
            public int PlayerNum;
        }

        public PlayerData Data { get; set; }

        public CPlayer(Entity self, PlayerData data): base(self)
        {
            Data = data;
            EventPublisher.Instance.GetEvent<ECollisions>().AddPlayer(this);
            EventPublisher.Instance.GetEvent<EDraw>().Subscribe(this);
            EventPublisher.Instance.GetEvent<EInput>().Subscribe(onKeyboardInput);
        }

        public override void Destroy()
        {
            EventPublisher.Instance.GetEvent<ECollisions>().RemovePlayer(this);
            EventPublisher.Instance.GetEvent<EDraw>().Unsubscribe(this);
            EventPublisher.Instance.GetEvent<EInput>().Unsubscribe(onKeyboardInput);
        }

        public void onDraw()
        {
            UIAdapter.Instance.DrawEntity(Data.AbsoluteX, Data.AbsoluteY, Data.PlayerNum);
        }

        public void onKeyboardInput(int plrNum, ControlType type)
        {
            if (plrNum == Data.PlayerNum)
            {
                switch(type)
                {
                    case ControlType.Left or ControlType.Right or ControlType.Up or ControlType.Down:
                        EventPublisher.Instance.GetEvent<ECollisions>().TryPlayerMove(this, (Direction)(int)type);
                        break;
                    case ControlType.Place:
                        PlaceBomb();
                        break;
                }
            }
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left: Data.AbsoluteX -= Data.Speed; break;
                case Direction.Right: Data.AbsoluteX += Data.Speed; break;
                case Direction.Up: Data.AbsoluteY -= Data.Speed; break;
                case Direction.Down: Data.AbsoluteY += Data.Speed; break;
            }
            UpdatePlayerCell();
        }

        private void Move(ControlType type)
        {
            /*if (!HasCollidedWithSolid(type))
            {*/
                
            /*}
            else if (Self.GetComponent<CLocation>().Location[(int)type]?.Data is not ISolid)
            {
                int[] cellPos = GetCellPos();
                if ((int)type >= 2)
                {
                    CentrePosition(ref Data.AbsoluteX, cellPos[0] * UIAdapter.CELL_WIDTH, type);
                }
                else
                {
                    CentrePosition(ref Data.AbsoluteY, cellPos[1] * UIAdapter.CELL_HEIGHT, type);
                }
                
            }*/
            UpdatePlayerCell();
        }

        private void CentrePosition(ref float coord, int cellPos, ControlType type)
        {
            if (coord < cellPos)
            {
                coord += Data.Speed;
            }
            else
            {
                coord -= Data.Speed;
            }
        }

        /*private bool HasCollidedWithSolid(ControlType type)
        {
            Cell nextCell = Self.GetComponent<CLocation>().Location[(int)type];
            if (nextCell != null)
            {
                if (nextCell.Data is ISolid && UIAdapter.Instance.HasCollided(this, nextCell.Data))
                {
                    return true;
                }
                else
                {
                    foreach (Cell c in nextCell)
                    {
                        if (c?.Data is ISolid && UIAdapter.Instance.HasCollided(this, c.Data))
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }*/

        private void PlaceBomb()
        {
            Cell loc = Self.GetComponent<CLocation>().Location;
            if (loc.Data is not CBomb && Data.BombCount > 0)
            {
                Data.BombCount--;
                CDraw pos = ((Component)loc.Data).Self.GetComponent<CDraw>();
                loc.Data = EntityFactory.Instance.CreateBomb(pos.X, pos.Y, Data.BombSize, Data.BombFuse, this).GetComponent<CBomb>();
            }
            
        }

        private void UpdatePlayerCell()
        {
            int[] cellLoc = GetCellPos();
            Self.GetComponent<CLocation>().Location = Map.Instance[cellLoc[0], cellLoc[1]];
        }

        private int[] GetCellPos()
        {
            int[] result = new int[2];
            int w = UIAdapter.CELL_WIDTH;
            int h = UIAdapter.CELL_HEIGHT;
            float plrX = Data.AbsoluteX + (w / 2);
            float plrY = Data.AbsoluteY + (h / 2);
            result[0] = (int)(plrX / w);
            result[1] = (int)(plrY / h);
            return result;
        }
    }
}
