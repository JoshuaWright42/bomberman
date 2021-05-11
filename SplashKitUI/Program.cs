using System;
using SplashKitSDK;
using BomberManGame;

namespace SplashKitUI
{
    public class Program
    {
        public static void Main()
        {
            new SplashKitAdapter();
            Game game = new Game();
            game.StartGame();
        }
    }

}
