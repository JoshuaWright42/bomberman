using System;
using BomberManGame;
using BomberManGame.EntityComponents;
using SplashKitSDK;
using System.Collections.Generic;

namespace SplashKitUI
{
    public class SplashKitAdapter: UIAdapter
    {
        //Controls<KeyCode> p1 = new Controls<KeyCode>();
        Dictionary<ControlType, KeyCode>[] _controls;

        public SplashKitAdapter()
        {
        }

        public override void DrawEntity(CDraw toDraw) //draws regular entities (not players)
        {
            SplashKit.DrawBitmap(SplashKit.BitmapNamed(toDraw.Type.ToString()), toDraw.X * CELL_WIDTH, toDraw.Y * CELL_HEIGHT);
        }

        public override void DrawEntity(float x, float y, int playerNum) //draws players
        {
            SplashKit.DrawBitmap(SplashKit.BitmapNamed($"player{playerNum}"), x, y);
        }

        public override bool HasCollided(CPlayer plr, ITile tile)
        {
            CDraw tileComp = ((Component)tile).GetComponent<CDraw>();

            return SplashKit.BitmapCollision(
                SplashKit.BitmapNamed($"player{plr.Data.PlayerNum}"),
                plr.Data.AbsoluteX,
                plr.Data.AbsoluteY,
                SplashKit.BitmapNamed(tileComp.Type.ToString()),
                tileComp.X * CELL_WIDTH,
                tileComp.Y * CELL_HEIGHT
                );
        }

        public override void LoadAssets()
        {
            throw new NotImplementedException();
        }

        public override void ProcessInput()
        {
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
            SplashKit.RefreshScreen();
        }
    }
}
