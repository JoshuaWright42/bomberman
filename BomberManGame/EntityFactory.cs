using System;
using BomberManGame.EntityComponents;
namespace BomberManGame
{
    public class EntityFactory
    {
        const int EXPLOSION_LENGTH = 200;



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
            Entity result = new Entity();

            //Build explosion entity
            result.AddComponent<CDraw>(new CDraw(result, x, y));
            CExplosion exp = new CExplosion(result);
            result.AddComponent<CExplosion>(exp);
            result.AddComponent<CTimer>(new CTimer(result, EXPLOSION_LENGTH, exp.onComplete));

            //return
            return result;
        }
    }
}
