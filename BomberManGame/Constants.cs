using System;
namespace BomberManGame
{
    public class Constants
    {
        public static readonly int CELL_WIDTH = Convert.ToInt32(UIAdapter.Instance.Config.GetElementById("CELL_WIDTH").InnerText);
        public static readonly int CELL_HEIGHT = Convert.ToInt32(UIAdapter.Instance.Config.GetElementById("CELL_HEIGHT").InnerText);
        public static readonly double ITEM_DROP_CHANCE = Convert.ToDouble(UIAdapter.Instance.Config.GetElementById("ITEM_DROP_CHANCE").InnerText);
        public static readonly double CRATE_CHANCE = Convert.ToDouble(UIAdapter.Instance.Config.GetElementById("CRATE_CHANCE").InnerText);
        public static readonly int EXPLOSION_LENGTH = Convert.ToInt32(UIAdapter.Instance.Config.GetElementById("EXPLOSION_LENGTH").InnerText);
        public static readonly float PLAYER_SPEED = (float)Convert.ToDouble(UIAdapter.Instance.Config.GetElementById("PLAYER_SPEED").InnerText);
        public static readonly int PLAYER_FUSE = Convert.ToInt32(UIAdapter.Instance.Config.GetElementById("PLAYER_FUSE").InnerText);
    }
}
