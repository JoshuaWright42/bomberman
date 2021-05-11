using System;
namespace BomberManGame
{
    public class Game
    {
        public Game()
        {
            Map.CreateNewInstance(21, 11);
            UIAdapter.Instance.OpenGameWindow(21, 11);
            UIAdapter.Instance.LoadAssets();
        }

        public void StartGame()
        {
            do
            {
                UIAdapter.Instance.ProcessInput();
                EventPublisher.Instance.GetEvent<EDraw>().Start();
            } while (!UIAdapter.Instance.GameExited());
        }
    }
}
