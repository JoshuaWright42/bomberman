using System;
using System.Timers;

namespace BomberManGame.EntityComponents
{
    /// <summary>
    /// Timer component. Any entity which needs to update after an elapsed time needs this.
    /// Creates timer that will go for given time (milliseconds) then call/invoke the passed
    /// in event handler.
    /// </summary>
    public class CTimer: Component
    {
        /// <summary>
        /// The Timer. From System.Timers
        /// </summary>
        private Timer Timer { get; init; }

        /// <summary>
        /// Constructor. Automatically creates and starts the timer for a given length (milliseconds).
        /// </summary>
        /// <param name="self">The Entity this component belongs too.</param>
        /// <param name="length">The timed interval (milliseconds)</param>
        /// <param name="handler">The method/handler to call once time has passed.</param>
        internal CTimer(Entity self, int length, ElapsedEventHandler handler): base (self)
        {
            Timer = new Timer(length); //create timer
            Timer.Elapsed += handler; //subscribe the passed in handler
            Timer.AutoReset = false; //will stop after one interval has passed
            Timer.Start(); //starts timer
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
