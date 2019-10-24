using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10. Base;
using Microsoft.Xna.Framework;
namespace GroupGame10.GameSystem
{
    class PhysicsManager : GameComponent, IManager
    {
        List<BaseEntity> entities;
        List<Block> blockList;
        List<Enemy> enemyList;
        List<Item> itemList;
        List<List<BaseEntity>> mapList;
        Player player;

        internal List<List<BaseEntity>> MapList { get => mapList; set => mapList = value; }

        public PhysicsManager(Game game) : base(game)
        {
            
        }
        public override void Initialize()
        {
            entities = new List<BaseEntity>();
            MapList = new List<List<BaseEntity>>();
            player = null;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (entities != null)  entities.ForEach(b => b.Update(gameTime));
            if(mapList != null)
            {
                foreach (var list in mapList)
                {
                    foreach (var c in list)
                    {
                        if (c.Rectangle.Intersects(player.Rectangle))
                            player.Hit(c);
                    }
                }
            }
            foreach (var list in mapList)
            {
                list.RemoveAll(a => a.IsDeadFlag);
            }
            if (player != null)
            {
                if (player.IsDeadFlag)
                {
                    //Enabled = false;

                }
            }
            base.Update(gameTime);
        }

        public void Add(BaseEntity entity)
        {
            entities.Add(entity);
            if (entity is Player) player =(Player) entity;
        }



        public void ClearList()
        {
            Initialize();
        }
    }
}
