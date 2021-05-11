using System;

namespace BomberManGame.EntityComponents
{
    public record PlayerData
    {
        public float AbsoluteX, AbsoluteY;
        public bool isDead;
        public int Speed, BombSize, BombFuse;
        public int PlayerNum;
    }

    public class CPlayer: Component
    {
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
            throw new NotImplementedException();
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
            CheckCollisions();
            UpdatePlayerCell();
        }

        private void CheckCollisions()
        {
            foreach (Cell c1 in Self.GetComponent<CLocation>().Location)
            {
                if (c1.Data is IAffectPlayer && UIAdapter.Instance.HasCollided(this, c1.Data))
                {
                    ((IAffectPlayer)c1).ApplyEffect(this);
                }
                foreach (Cell c2 in c1)
                {
                    if (c2.Data is IAffectPlayer && UIAdapter.Instance.HasCollided(this, c2.Data))
                    {
                        ((IAffectPlayer)c2).ApplyEffect(this);
                    }

                }
            }
        }

        private bool HasCollidedWithSolid(ControlType type)
        {
            Cell nextCell = Self.GetComponent<CLocation>().Location[(int)type];
            if (nextCell.Data is ISolid && UIAdapter.Instance.HasCollided(this, nextCell.Data))
            {
                return true;
            }
            else
            {
                foreach (Cell c in nextCell)
                {
                    if (c.Data is ISolid && UIAdapter.Instance.HasCollided(this, c.Data))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void PlaceBomb()
        {
            Cell loc = GetComponent<CLocation>().Location;
            if (loc.Data is not CBomb)
            {
                CDraw pos = ((Entity)loc.Data).GetComponent<CDraw>();
                loc.Data = EntityFactory.Instance.CreateBomb(pos.X, pos.Y, Data.BombSize, Data.BombFuse).GetComponent<CBomb>();
            }
            
        }

        private void UpdatePlayerCell()
        {
            int w = UIAdapter.CELL_WIDTH;
            int h = UIAdapter.CELL_HEIGHT;
            float plrX = Data.AbsoluteX + (w / 2);
            float plrY = Data.AbsoluteY + (h / 2);
            int cellX = (int)Math.Round(plrX / w);
            int cellY = (int)Math.Round(plrY / h);
            GetComponent<CLocation>().Location = Map.Instance[cellX, cellY];
        }
    }
}
