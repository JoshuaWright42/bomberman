using System;
using BomberManGame;
using BomberManGame.EntityComponents;
using SplashKitSDK;
using System.Collections.Generic;
using System.Xml;

namespace SplashKitUI
{
    public class SplashKitAdapter: UIAdapter
    {
        Dictionary<ControlType, KeyCode>[] _controls;
        XmlDocument _config;

        public SplashKitAdapter()
        {
            _config = new XmlDocument();
            _config.Load("config.xml");
            int numPlayers = Convert.ToInt32(_config.GetElementById("players").Attributes["num"].Value);
            _controls = new Dictionary<ControlType, KeyCode>[numPlayers];
            for (int i = 0; i < _controls.Length; i++)
            {
                _controls[i] = new Dictionary<ControlType, KeyCode>();
                XmlNodeList controls = _config.GetElementById(i.ToString()).GetElementsByTagName("control");
                foreach (XmlNode control in controls)
                {
                    ControlType type = Enum.Parse<ControlType>(control.Attributes["type"].Value);
                    KeyCode key = Enum.Parse<KeyCode>(control.Attributes["key"].Value);
                    _controls[i].Add(type, key);
                }
            }
        }

        public override XmlDocument Config => _config;

        public override void DrawEntity(CDraw toDraw) //draws regular entities (not players)
        {
            float absX = toDraw.X * Constants.CELL_WIDTH;
            float absY = toDraw.Y * Constants.CELL_HEIGHT;
            SplashKit.DrawBitmap(SplashKit.BitmapNamed("Air"), absX, absY);
            SplashKit.DrawBitmap(SplashKit.BitmapNamed(toDraw.Type.ToString()), absX, absY);
        }

        public override void DrawEntity(float x, float y, int playerNum) //draws players
        {
            SplashKit.DrawBitmap(SplashKit.BitmapNamed($"Player{playerNum}"), x, y);
        }

        public override bool GameExited()
        {
            return SplashKit.WindowCloseRequested("BomberMan - Wright Edition");
        }

        public override bool HasCollided(CPlayer plr, Component comp)
        {
            if (!comp.Self.HasComponent<CDraw>()) return false;
            CDraw toCheck = comp.Self.GetComponent<CDraw>();

            return SplashKit.BitmapCollision(
                SplashKit.BitmapNamed($"Player{plr.Data.PlayerNum}"),
                plr.Data.AbsoluteX,
                plr.Data.AbsoluteY,
                SplashKit.BitmapNamed(toCheck.Type.ToString()),
                toCheck.X * Constants.CELL_WIDTH,
                toCheck.Y * Constants.CELL_HEIGHT
                );
        }

        public override void LoadAssets()
        {
            SplashKit.LoadResourceBundle("Assets", "AllAssets.txt");
        }

        public override void OpenGameWindow(int cols, int rows)
        {
            new Window("BomberMan - Wright Edition", cols * Constants.CELL_WIDTH, rows * Constants.CELL_HEIGHT);
            SplashKit.ClearScreen(Color.Gray);
        }

        public override void ProcessInput()
        {
            SplashKit.ProcessEvents();
            for (int i = 0; i < _controls.Length; i++)
            {
                foreach (KeyValuePair<ControlType, KeyCode> keys in _controls[i])
                {
                    if (SplashKit.KeyDown(keys.Value))
                    {
                        EventPublisher.Instance.GetEvent<EInput>().InputReceived(i, keys.Key);
                    }
                }
            }
        }

        public override void RefreshScreen()
        {
            SplashKit.RefreshScreen(60);
        }
    }
}
