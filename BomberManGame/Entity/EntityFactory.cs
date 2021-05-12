﻿using System;
using BomberManGame.EntityComponents;
namespace BomberManGame
{
    public class EntityFactory
    {
        const int EXPLOSION_LENGTH = 200;
        const float PLAYER_SPEED = 2.0f;
        const int PLAYER_FUSE = 3000;


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

        public Entity CreateBomb(int x, int y, int size, int fuse, CPlayer owner)
        {
            Entity result = CreateBasicEntity(x, y, EntityType.Bomb);
            CBomb bomb = new CBomb(result, owner, size);
            result.AddComponent<CBomb>(bomb);
            result.AddComponent<CTimer>(new CTimer(result, fuse, bomb.onExplode));
            return result;
        }

        public Entity CreatePlayer(int num, int cellX, int cellY, float absX, float absY)
        {
            PlayerData data = new PlayerData
            {
                AbsoluteX = absX,
                AbsoluteY = absY,
                PlayerNum = num,
                isDead = false,
                BombSize = 1,
                Speed = PLAYER_SPEED,
                BombFuse = PLAYER_FUSE,
                BombCount = 1
            };

            Entity result = new Entity();
            result.AddComponent<CLocation>(new CLocation(result, Map.Instance[cellX, cellY]));
            result.AddComponent<CPlayer>(new CPlayer(result, data));

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