using System;
namespace BomberManGame
{
    public class EDraw
    {
        private delegate void DrawObject();
        private event DrawObject Drawables;

        public void Subscribe(IDraw sub)
        {
            Drawables += sub.onDraw;
        }

        public void Start()
        {
            Drawables?.Invoke();
            Drawables = null;
        }
    }
}
