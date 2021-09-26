using System;
using BomberManGame.Components;

namespace BomberManGame
{
    /// <summary>
    /// Defines a draw event. Invoked once per frame.
    /// However, clears subscibers after it is invoked.
    /// </summary>
    public class EDraw: Event
    {
        /// <summary>
        /// Delegate (function pointer) definition for a "Draw" method.
        /// </summary>
        private delegate void DrawObject();

        /// <summary>
        /// Observer/event that tracks subscribers.
        /// </summary>
        private event DrawObject Drawables;

        private event DrawObject Players;

        /// <summary>
        /// Subscribes the Draw method of the passed in CDraw component to Drawables.
        /// </summary>
        /// <param name="sub">The Draw compoenent that wishes to subscribe.</param>
        public void Subscribe(CDraw sub) => Drawables += sub.onDraw;

        public void Unsubscribe(CDraw sub) => Drawables -= sub.onDraw;

        public void Subscribe(CPlayer sub) => Players += sub.onDraw;

        public void Unsubscribe(CPlayer sub) => Players -= sub.onDraw;

        /// <summary>
        /// Start a draw event. Then clears subscribers.
        /// </summary>
        public void Start()
        {
            Drawables?.Invoke();
            //Drawables = null;
            Players?.Invoke();
            UIAdapter.Instance.RefreshScreen();
        }
    }
}
