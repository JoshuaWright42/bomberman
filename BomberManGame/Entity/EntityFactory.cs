using System;
using System.Timers;
using System.Xml;
using BomberManGame.EntityComponents;

namespace BomberManGame
{
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
        /// Creates an explosion entity which is what happens when a bomb goes off.
        /// Can kill players.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Entity CreateExplosion(int x, int y, ElapsedEventHandler handler = null)
        {
            //Create basic Entity
            Entity result = CreateBasicEntity(x, y, EntityType.Explosion);

            //Build explosion entity
            CExplosion exp = new CExplosion(result);
            result.AddComponent<CExplosion>(exp);
            if (handler == null) handler = exp.onComplete;
            result.AddComponent<CTimer>(new CTimer(result, Constants.EXPLOSION_LENGTH, handler));

            //return
            return result;
        }

        /// <summary>
        /// Creates an Air entity which simply represents an empty cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Entity CreateAir(int x, int y)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Air);
            result.AddComponent<CAir>(new CAir(result));
            return result;
        }

        /// <summary>
        /// Creates a Brick entity which simply represents an solid cell/wall.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Entity CreateBrick(int x, int y)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Brick);
            result.AddComponent<CBrick>(new CBrick(result));
            result.AddComponent<CSolid>(new CSolid(result));
            return result;
        }

        public Entity CreateBomb(int x, int y, int size, int fuse, CPlayer owner)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Bomb);
            CBomb bomb = new CBomb(result, owner, size);
            result.AddComponent<CBomb>(bomb);
            result.AddComponent<CTimer>(new CTimer(result, fuse, bomb.onExplode));
            result.AddComponent<CSolid>(new CSolid(result));
            return result;
        }

        public Entity CreatePlayer(int num, int cellX, int cellY, float absX, float absY)
        {
            CPlayer.PlayerData data = new CPlayer.PlayerData
            {
                AbsoluteX = absX,
                AbsoluteY = absY,
                PlayerNum = num,
                isDead = false,
                BombSize = 1,
                Speed = Constants.PLAYER_SPEED,
                BombFuse = Constants.PLAYER_FUSE,
                BombCount = 1
            };

            Entity result = new Entity();
            result.AddComponent<CLocation>(new CLocation(result, Map.Instance[cellX, cellY]));
            result.AddComponent<CPlayer>(new CPlayer(result, data));

            return result;
        }

        public void CreatePlayersFromXML()
        {
            XmlNodeList players = UIAdapter.Instance.Config.GetElementsByTagName("player");
            foreach(XmlNode player in players)
            {
                int playerNum = Convert.ToInt32(player.Attributes["id"].Value);
                XmlNode spawnCell = player.SelectSingleNode("spawn").SelectSingleNode("cell");
                int cellX = Convert.ToInt32(spawnCell.Attributes["x"].Value);
                int cellY = Convert.ToInt32(spawnCell.Attributes["y"].Value);
                float absX = cellX * Constants.CELL_WIDTH;
                float absY = cellY * Constants.CELL_HEIGHT;
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

        public Entity CreateCrate(int x, int y)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Crate);
            result.AddComponent<CCrate>(new CCrate(result));
            result.AddComponent<CSolid>(new CSolid(result));
            return result;
        }

        public ITile CreateEntityForMap(int x, int y, Cell loc, int cols, int rows)
        {
            Entity newEnt = new Entity();
            EntityType type;
            Component result;

            //brick blocks
            if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1
                || (x % 2 == 0 && y % 2 == 0))
            {
                result = new CBrick(newEnt);    
                newEnt.AddComponent<CBrick>(result);
                newEnt.AddComponent<CSolid>(new CSolid(newEnt));
                type = EntityType.Brick;
            }
            else //air blocks
            {
                if (Game.RNG.NextDouble() > Constants.CRATE_CHANCE)
                {
                    result = new CAir(newEnt);
                    newEnt.AddComponent<CAir>(result);
                    type = EntityType.Air;
                }
                else
                {
                    result = new CCrate(newEnt);
                    newEnt.AddComponent<CCrate>(result);
                    newEnt.AddComponent<CSolid>(new CSolid(newEnt));
                    type = EntityType.Crate;
                }
            }
            newEnt.AddComponent<CDraw>(new CDraw(newEnt, x, y, type));
            newEnt.AddComponent<CLocation>(new CLocation(newEnt, loc));

            return (ITile)result;
        }

        public ITile CreatePowerUp(int x, int y)
        {
            return CreateAir(x, y).GetComponent<CAir>(); //replace later
        }

        private Entity CreateBasicEntity(int x, int y, EntityType type)
        {
            Entity result = new Entity();
            result.AddComponent<CDraw>(new CDraw(result, x, y, type));
            result.AddComponent<CLocation>(new CLocation(result, Map.Instance[x, y]));
            return result;
        }
    }
}
