﻿using System;
using BomberManGame.EntityComponents;

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

        /// <summary>
        /// Subscribes the Draw method of the passed in CDraw component to Drawables.
        /// </summary>
        /// <param name="sub">The Draw compoenent that wishes to subscribe.</param>
        public void Subscribe(CDraw sub) => Drawables += sub.onDraw;

        /// <summary>
        /// Start a draw event. Then clears subscribers.
        /// </summary>
        public void Start()
        {
            Drawables?.Invoke();
            Drawables = null;
        }
    }
}
