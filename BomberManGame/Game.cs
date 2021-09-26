using System;
using BomberManGame.Entities;

namespace BomberManGame
{
    public class Game
    {
        public static readonly Random RNG = new Random();

        public Game()
        {
            Map.CreateNewInstance(21, 11);
            UIAdapter.Instance.OpenGameWindow(21, 11);
            UIAdapter.Instance.LoadAssets();
            EntityFactory.Instance.CreatePlayersFromXML();
        }

        public void StartGame()
        {
            do
            {
                UIAdapter.Instance.ProcessInput();
                EventPublisher.Instance.GetEvent<ECollisions>().CheckCollisions();
                EventPublisher.Instance.GetEvent<EDraw>().Start();
                //UIAdapter.Instance.DrawDebug(plr);
            } while (!UIAdapter.Instance.GameExited());
        }
    }
}
