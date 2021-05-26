using System;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;

namespace BomberManGame
{
    public record Settings
    {
        public static int CellWidth { get; private set; }
        public static int CellHeight { get; private set; }
        public static double ItemDropChance { get; private set; }
        public static double CrateChance { get; private set; }
        public static int ExplosionLength { get; private set; }
        public static float PlayerSpeed { get; private set; }
        public static int PlayerFuse { get; private set; }
        public static Dictionary<Type, int> Items { get; private set; }

        static Settings()
        {
            LoadSettingsFromXML();
        }

        static void LoadSettingsFromXML()
        {
            CellWidth = ReadInteger("CELL_WIDTH");
            CellHeight = ReadInteger("CELL_HEIGHT");
            ItemDropChance = ReadDouble("ITEM_DROP_CHANCE");
            CrateChance = ReadDouble("CRATE_CHANCE");
            ExplosionLength = ReadInteger("EXPLOSION_LENGTH");
            PlayerSpeed = ReadFloat("PLAYER_SPEED");
            PlayerFuse = ReadInteger("PLAYER_FUSE");
            //LoadItemsFromXML();
        }

        static void LoadItemsFromXML()
        {
            XmlNodeList items = UIAdapter.Instance.Config.GetElementsByTagName("item");
            Assembly assem = typeof(IAffectPlayer).Assembly;
            foreach (XmlNode item in items)
            {
                if (ReadBoolean("enabled"))
                {
                    Type itemType = assem.GetType($"BomberManGame.C{item.Attributes["type"].Value}");
                    int weight = Convert.ToInt32(item.Attributes["weight"].Value);
                    Items.Add(itemType, weight);
                }
            }
        }

        static string ReadSetting(string setting) => UIAdapter.Instance.Config.GetElementById(setting).InnerText;
        static int ReadInteger(string setting) => Convert.ToInt32(ReadSetting(setting));
        static float ReadFloat(string setting) => (float)Convert.ToDouble(ReadSetting(setting));
        static double ReadDouble(string setting) => Convert.ToDouble(ReadSetting(setting));
        static bool ReadBoolean(string setting) => Convert.ToBoolean(ReadSetting(setting));
    }
}
