using System;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;
using BomberManGame.Entities;
using static BomberManGame.Entities.ItemFactory;

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
        public static List<ItemData> Items { get; private set; }

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
            LoadItemsFromXML();
        }

        static void LoadItemsFromXML()
        {
            Items = new List<ItemData>();
            XmlNodeList items = UIAdapter.Instance.Config.GetElementsByTagName("item");
            foreach (XmlNode item in items)
            {
                int totalWeight = 0;
                if (Convert.ToBoolean(item.Attributes["enabled"].Value))
                {
                    string typeAsString = item.Attributes["type"].Value;
                    MethodInfo method = typeof(ItemFactory).GetMethod(typeAsString);
                    EntityType type = Enum.Parse<EntityType>(typeAsString);
                    if (method is not null)
                    {
                        totalWeight += Convert.ToInt32(item.Attributes["weight"].Value);
                        Items.Add(new ItemData(method, type, totalWeight));
                    }
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
