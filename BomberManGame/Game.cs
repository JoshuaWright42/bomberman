using System;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
    public class Game
    {
        private Entity plr;

        public Game()
        {
            Map.CreateNewInstance(21, 11);
            UIAdapter.Instance.OpenGameWindow(21, 11);
            UIAdapter.Instance.LoadAssets();
            plr = EntityFactory.Instance.CreatePlayer(0, 1, 1, 32, 32);
        }

        public void StartGame()
        {
            do
            {
                UIAdapter.Instance.ProcessInput();
                EventPublisher.Instance.GetEvent<EDraw>().Start();
                //UIAdapter.Instance.DrawDebug(plr);
            } while (!UIAdapter.Instance.GameExited());
        }
    }
}
