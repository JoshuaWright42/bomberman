using System;
using System.Text.Json;
using BomberManGame.EntityComponents;
using System.Xml;

namespace BomberManGame
{
    public abstract class UIAdapter
    {
        public abstract XmlDocument Config { get; }

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
        public abstract bool HasCollided(CPlayer plr, Component comp);
        public abstract void LoadAssets();
        public abstract void OpenGameWindow(int cols, int rows);
        public abstract bool GameExited();
        //public abstract void DrawDebug(Entity plr);
    }
}
