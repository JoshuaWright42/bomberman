using System;
using BomberManGame.EntityComponents;
namespace BomberManGame
{
    public class EntityFactory
    {
        const int EXPLOSION_LENGTH = 200;
        const int COLUMNS = 20;
        const int ROWS = 10;


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
        public Entity CreateExplosion(int x, int y)
        {
            //Create basic Entity
            Entity result = CreateBasicEntity(x, y, EntityType.Explosion);

            //Build explosion entity
            CExplosion exp = new CExplosion(result);
            result.AddComponent<CExplosion>(exp);
            result.AddComponent<CTimer>(new CTimer(result, EXPLOSION_LENGTH, exp.onComplete));

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
            return result;
        }

        public Entity CreateBomb(int x, int y, int size, int fuse)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Bomb);
            CBomb bomb = new CBomb(result, size);
            result.AddComponent<CBomb>(bomb);
            result.AddComponent<CTimer>(new CTimer(result, fuse, bomb.onExplode));
            return result;
        }

        public ITile CreateEntityForMap(int x, int y, Cell loc)
        {
            Entity newEnt = new Entity();
            EntityType type;
            Component result;

            //brick blocks
            if (x == 0 || y == 0 || x == COLUMNS - 1 || y == ROWS - 1
                || (x % 2 == 0 && y % 2 == 0))
            {
                result = new CBrick(newEnt);    
                newEnt.AddComponent<CBrick>(result);
                type = EntityType.Brick;
            }
            else //air blocks
            {
                result = new CAir(newEnt);
                newEnt.AddComponent<CAir>(result);
                type = EntityType.Air;
            }
            newEnt.AddComponent<CDraw>(new CDraw(newEnt, x, y, type));
            newEnt.AddComponent<CLocation>(new CLocation(newEnt, loc));

            return (ITile)result;
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
