using System;
using BomberManGame;
using BomberManGame.EntityComponents;
using SplashKitSDK;
using System.Collections.Generic;

namespace SplashKitUI
{
    public class SplashKitAdapter: UIAdapter
    {
        Dictionary<ControlType, KeyCode>[] _controls;

        public SplashKitAdapter()
        {
            _controls = new Dictionary<ControlType, KeyCode>[1];
            _controls[0] = new Dictionary<ControlType, KeyCode>();
            _controls[0].Add(ControlType.Left, KeyCode.LeftKey);
            _controls[0].Add(ControlType.Right, KeyCode.RightKey);
            _controls[0].Add(ControlType.Up, KeyCode.UpKey);
            _controls[0].Add(ControlType.Down, KeyCode.DownKey);
            _controls[0].Add(ControlType.Place, KeyCode.ZKey);

        }

        public override void DrawEntity(CDraw toDraw) //draws regular entities (not players)
        {
            float absX = toDraw.X * CELL_WIDTH;
            float absY = toDraw.Y * CELL_HEIGHT;
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

        public override bool HasCollided(CPlayer plr, ITile tile)
        {
            CDraw tileComp = ((Component)tile).GetComponent<CDraw>();

            return SplashKit.BitmapCollision(
                SplashKit.BitmapNamed($"Player{plr.Data.PlayerNum}"),
                plr.Data.AbsoluteX,
                plr.Data.AbsoluteY,
                SplashKit.BitmapNamed(tileComp.Type.ToString()),
                tileComp.X * CELL_WIDTH,
                tileComp.Y * CELL_HEIGHT
                );
        }

        public override void LoadAssets()
        {
            SplashKit.LoadResourceBundle("Assets", "AllAssets.txt");
        }

        public override void OpenGameWindow(int cols, int rows)
        {
            new Window("BomberMan - Wright Edition", cols * CELL_WIDTH, rows * CELL_HEIGHT);
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
