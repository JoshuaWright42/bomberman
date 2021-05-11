using System;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
    public enum EntityType
    {
        Explosion,
        Bomb,
        Player,
        Air,
        Brick
    }

    public enum ControlType
    {
        Left,
        Right,
        Up,
        Down,
        Place
    }

    /*public record Controls<T>
    {
        T Left;
        T Right;
        T Up;
        T Down;
        T Place;
    }*/

    public abstract class UIAdapter
    {
        public const int CELL_WIDTH = 30;
        public const int CELL_HEIGHT = 30;



        private static UIAdapter _instance;
        protected UIAdapter() => Init();
        public static UIAdapter Instance
        {
            get
            {
                if (_instance is null) throw new NullReferenceException("No runtime UI adapter has been provided.");
                return _instance;
            }
        }

        protected void Init()
        {
            _instance = this;
        }

        public abstract void DrawEntity(CDraw toDraw);
        public abstract void DrawEntity(float x, float y, int playerNum);
        public abstract void ProcessInput();
        public abstract void RefreshScreen();
        public abstract bool HasCollided(CPlayer plr, ITile tile);
        public abstract void LoadAssets();
        public abstract void OpenGameWindow(int cols, int rows);
        public abstract bool GameExited();
    }
}
