using System;
namespace BomberManGame
{
    /// <summary>
    /// Generic data type for anything that can/should be drawn in the game.
    /// Responsible publisher for the Drawables/Draw event which instructs all subscribers to draw themselves.
    /// All Drawable objects are automatically subscribed to the Drawables event upon instantiation.
    /// Event publication is static.
    /// </summary>
    public abstract class Drawable
    {
        /// <summary>
        /// Delegate for Drawables event.
        /// </summary>
        private delegate void DrawObject();

        /// <summary>
        /// Event reponsible for instructing every Drawable object to Draw.
        /// </summary>
        private static event DrawObject Drawables;

        /// <summary>
        /// Subscribe this Drawable object to the Drawables event.
        /// </summary>
        public void Register()
        {
            Drawables += Draw;
        }

        /// <summary>
        /// Starts a drawing event.
        /// </summary>
        public static void StartDrawing()
        {
            OnDraw();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Triggers OnDraw event for all subscribers/drawable objects.
        /// </summary>
        private static void OnDraw()
        {
            Drawables?.Invoke();
            Drawables = null; //clears subscribers, only objects that have changed are subscribed
        }

        /// <summary>
        /// Instantiates a Drawable obejct. Subscribers to Drawables event.
        /// </summary>
        /// <param name="x">X position of object.</param>
        /// <param name="y">y position of object.</param>
        public Drawable(float x, float y)
        {
            X = x;
            Y = y;
            Register();
        }

        /// <summary>
        /// Draws the object, can be overidden.
        /// </summary>
        protected virtual void Draw()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Property for X
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Property for Y
        /// </summary>
        public float Y { get; set; }
    }
}
