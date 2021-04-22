using System;
namespace BomberManGame
{
    public delegate void DrawObject();

    public abstract class Drawable
    {
        public static event DrawObject Drawables;

        public static void StartDrawing()
        {
            OnDraw();
            throw new NotImplementedException();
        }

        private static void OnDraw()
        {
            Drawables?.Invoke();
        }


        public Drawable(float x, float y)
        {
            X = x;
            Y = y;
            Drawables += Draw;
        }

        public virtual void Draw()
        {
            throw new NotImplementedException();
        }

        public float X { get; set; }
        public float Y { get; set; }
    }
}
