using System;
namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Draw component. Any entity which can be drawn needs this.
    /// Responsible for knowing where an entity is and handling any related draw
    /// functionality.
    /// </summary>
    public class CDraw: Component
    {
        public int X { get; set; } // X
        public int Y { get; set; } // Y

        /// <summary>
        /// Constructor. Initialises X and Y using given parameters.
        /// </summary>
        /// <param name="self">The Entity this component belongs too.</param>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        internal CDraw(Entity self, int x, int y): base (self)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Event handler for the Draw Event. When subcribed will execute when
        /// a draw event is next invoked.
        /// </summary>
        public void onDraw()
        {
            UIAdapter.DrawEntity(x, y, EntityType);
        }

        /// <summary>
        /// Subscribe this specific Draw Component to the Draw Event.
        /// </summary>
        public void Subscribe() => EventPublisher.Instance.GetEvent<EDraw>().Subscribe(this);
    }
}
