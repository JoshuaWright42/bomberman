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
            EventPublisher.Instance.GetEvent<EDraw>().Subscribe(this);
            EventPublisher.Instance.GetEvent<EInput>().Subscribe(onKeyboardInput);
        }

        public void onDraw()
        {
            UIAdapter.Instance.DrawEntity(Data.AbsoluteX, Data.AbsoluteY, Data.PlayerNum);
        }

        public void onKeyboardInput(int plrNum, ControlType type)
        {
            if (plrNum == Data.PlayerNum)
            {
                if ((int)type < 4) Move(type);
                switch(type)
                {
                    case ControlType.Place: PlaceBomb(); break;
                }
            }
        }

        private void Move(ControlType type)
        {
            if (!HasCollidedWithSolid(type))
            {
                switch (type)
                {
                    case ControlType.Left: Data.AbsoluteX -= Data.Speed; break;
                    case ControlType.Right: Data.AbsoluteX += Data.Speed; break;
                    case ControlType.Up: Data.AbsoluteY -= Data.Speed; break;
                    case ControlType.Down: Data.AbsoluteY += Data.Speed; break;
                }
            }
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
                
            }
            CheckCollisions();
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

        private void CheckCollisions()
        {
            foreach (Cell c1 in Self.GetComponent<CLocation>().Location)
            {
                if (c1 != null)
                {
                    //((Component)c1.Data).GetComponent<CDraw>().Subscribe();
                    if (c1.Data is IAffectPlayer && UIAdapter.Instance.HasCollided(this, c1.Data))
                    {
                        ((IAffectPlayer)c1).ApplyEffect(this);
                    }
                    foreach (Cell c2 in c1)
                    {
                        //((Component)c2?.Data)?.GetComponent<CDraw>().Subscribe();
                        if (c2?.Data is IAffectPlayer && UIAdapter.Instance.HasCollided(this, c2.Data))
                        {
                            ((IAffectPlayer)c2).ApplyEffect(this);
                        }
                    }
                }
            }
        }

        private bool HasCollidedWithSolid(ControlType type)
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
        }

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
