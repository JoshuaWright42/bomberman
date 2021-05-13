using System;
namespace BomberManGame
{
    public class EInput: Event
    {
        public delegate void ProcessInput(int plrNum, ControlType control);

        private event ProcessInput Players;

        public void Subscribe(ProcessInput sub) => Players += sub;

        public void Unsubscribe(ProcessInput sub) => Players -= sub;

        public void InputReceived(int plrNum, ControlType control)
        {
            Players?.Invoke(plrNum, control);
        }
    }
}
