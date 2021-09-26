using System;
using BomberManGame.Entities;

namespace BomberManGame.Components
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
                        EventPublisher.Instance.GetEvent<EMove>().TryPlayerMove(this, (Direction)(int)type);
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

        private void UpdatePlayerCell()
        {
            int w = Settings.CellWidth;
            int h = Settings.CellHeight;
            float plrX = Data.AbsoluteX + (w / 2);
            float plrY = Data.AbsoluteY + (h / 2);
            int cellX = (int)(plrX / w);
            int cellY = (int)(plrY / h);
            Self.GetComponent<CLocation>().Location = Map.Instance[cellX, cellY];
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
    }
}
