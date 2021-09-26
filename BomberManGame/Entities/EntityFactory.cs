using System;
using System.Timers;
using System.Xml;
using BomberManGame.Components;
using BomberManGame.Interfaces;

namespace BomberManGame.Entities
{
    /// <summary>
    /// Singleton. Factory. Responsible for creation of any and all entities.
    /// </summary>
    public class EntityFactory
    {
        private static EntityFactory _instance;

        private EntityFactory() { }

        public static EntityFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new EntityFactory();
                return _instance;
            }
        }

        /// <summary>
        /// Creates an Air entity which simply represents an empty cell.
        /// </summary>
        public Entity CreateAir(int x, int y, Cell loc = null)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Air, loc);
            result.AddComponent<CAir>(new CAir(result));
            return result;
        }

        /// <summary>
        /// Used by most other entity "constructors". Generates entity with commonly
        /// used components.
        /// </summary>
        public Entity CreateBasicEntity(int x, int y, EntityType type, Cell loc = null)
        {
            Entity result = new Entity();
            if (loc == null) loc = Map.Instance[x, y];
            result.AddComponent<CDraw>(new CDraw(result, x, y, type));
            result.AddComponent<CLocation>(new CLocation(result, loc));
            return result;
        }

        /// <summary>
        /// Creates a Brick entity which simply represents an solid cell/wall.
        /// </summary>
        public Entity CreateBrick(int x, int y, Cell loc = null)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Brick, loc);
            result.AddComponent<CBrick>(new CBrick(result));
            result.AddComponent<CSolid>(new CSolid(result));
            return result;
        }

        /// <summary>
        /// Creates a Bomb entity, bomb will explode after given time (fuse).
        /// </summary>
        public Entity CreateBomb(int x, int y, int size, int fuse, CPlayer owner)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Bomb);
            CBomb bomb = new CBomb(result, owner, size);
            result.AddComponent<CBomb>(bomb);
            result.AddComponent<CTimer>(new CTimer(result, fuse, bomb.onExplode));
            result.AddComponent<CSolid>(new CSolid(result));
            return result;
        }

        /// <summary>
        /// Creates a Crate entity. When destroyed, has a chance of dropping an item.
        /// </summary>
        public Entity CreateCrate(int x, int y, Cell loc = null)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Crate, loc);
            result.AddComponent<CCrate>(new CCrate(result));
            result.AddComponent<CSolid>(new CSolid(result));
            return result;
        }

        /// <summary>
        /// Creates an explosion entity which is what happens when a bomb goes off.
        /// Can kill players.
        /// </summary>
        /// <param name="handler">Optional method to call instead of default when explosion is complete.</param>
        public Entity CreateExplosion(int x, int y, ElapsedEventHandler handler = null)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Explosion);
            CExplosion exp = new CExplosion(result);
            result.AddComponent<CExplosion>(exp);
            if (handler == null) handler = exp.onComplete;
            result.AddComponent<CTimer>(new CTimer(result, Settings.ExplosionLength, handler));
            return result;
        }

        /// <summary>
        /// Creates a player entity for the game. Although the entity is returned, it doesnt
        /// get collected anywhere as the components take care of managing the player using events.
        /// </summary>
        public Entity CreatePlayer(int num, int cellX, int cellY, float absX, float absY)
        {
            CPlayer.PlayerData data = new CPlayer.PlayerData
            {
                AbsoluteX = absX,
                AbsoluteY = absY,
                PlayerNum = num,
                isDead = false,
                BombSize = 1,
                Speed = Settings.PlayerSpeed,
                BombFuse = Settings.PlayerFuse,
                BombCount = 1
            };

            Entity result = new Entity();
            result.AddComponent<CLocation>(new CLocation(result, Map.Instance[cellX, cellY]));
            result.AddComponent<CPlayer>(new CPlayer(result, data));

            return result;
        }

        /// <summary>
        /// Will generate a random powerup/item. Used by crates when destroyed.
        /// </summary>
        public ITile CreatePowerUp(int x, int y)
        {
            return ItemFactory.Instance.GenerateItem(x, y);
        }


        ///=====================================================================///
        ///                                                                     ///
        ///             METHODS BELOW USED FOR MAP/GAME CREATION                ///
        ///                                                                     ///
        ///=====================================================================///

        /// <summary>
        /// Will generate the correct default entity for a given cell when map is being
        /// created. Only returns the corresponding ITile component however.
        /// </summary>
        public ITile CreateEntityForMap(int x, int y, Cell loc, int cols, int rows)
        {
            if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1
                || (x % 2 == 0 && y % 2 == 0))
            {
                return CreateBrick(x, y, loc).GetComponent<CBrick>(); //bricks
            }
            if (Game.RNG.NextDouble() < Settings.CrateChance)
            {
                return CreateCrate(x, y, loc).GetComponent<CCrate>(); //crates
            }
            return CreateAir(x, y, loc).GetComponent<CAir>(); //air
        }

        /// <summary>
        /// This will create all the player entities for a game with default settings
        /// loaded from an XML file provided by the UIAdapter.
        /// </summary>
        public void CreatePlayersFromXML()
        {
            XmlNodeList players = UIAdapter.Instance.Config.GetElementsByTagName("player");
            foreach (XmlNode player in players)
            {
                int playerNum = Convert.ToInt32(player.Attributes["id"].Value);
                XmlNode spawnCell = player.SelectSingleNode("spawn").SelectSingleNode("cell");
                int cellX = Convert.ToInt32(spawnCell.Attributes["x"].Value);
                int cellY = Convert.ToInt32(spawnCell.Attributes["y"].Value);
                float absX = cellX * Settings.CellWidth;
                float absY = cellY * Settings.CellHeight;
                CreatePlayer(playerNum, cellX, cellY, absX, absY);

                XmlNodeList spawnCells = player.SelectSingleNode("spawn").SelectNodes("cell");
                foreach (XmlNode cell in spawnCells)
                {
                    int x = Convert.ToInt32(cell.Attributes["x"].Value);
                    int y = Convert.ToInt32(cell.Attributes["y"].Value);
                    Map.Instance[x, y].Data = CreateAir(x, y).GetComponent<CAir>();
                }
            }
        }
    }
}
