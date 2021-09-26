using BomberManGame.Components;
using BomberManGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BomberManGame.Entities
{
    public class ItemFactory
    {
        public struct ItemData
        {
            public ItemData(MethodInfo dataType, EntityType gameType, int weight)
            {
                Constructor = dataType;
                Type = gameType;
                Weight = weight;
            }

            public MethodInfo Constructor;
            public EntityType Type;
            public int Weight;
        }

        private static ItemFactory _instance;
        private Random _rnd = new Random();

        private ItemFactory()
        {
        }

        public static ItemFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new ItemFactory();
                return _instance;
            }
        }

        public ITile GenerateItem(int x, int y)
        {
            if (Settings.Items.Count > 0)
            {
                int roll = _rnd.Next(0, Settings.Items.Last().Weight);
                foreach (ItemData item in Settings.Items)
                {
                    if (roll <= item.Weight)
                    {
                        return item.Constructor.Invoke(this, new object[] { x, y, item.Type }) as ITile;
                    }
                }
            }
            return EntityFactory.Instance.CreateAir(x, y).GetComponent<CAir>();
        }

        public ITile ItemRange(int x, int y, EntityType type)
        {
            Entity result = EntityFactory.Instance.CreateBasicEntity(x, y, type);
            result.AddComponent<CItemRange>(new CItemRange(result));
            return result.GetComponent<CItemRange>();
        }
    }
}
