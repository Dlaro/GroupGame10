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
        public PhysicsManager(Game game) : base(game)
        {
        }
        public override void Initialize()
        {
            entities = new List<BaseEntity>();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (entities== null) return;
            entities.ForEach(b => b.Update(gameTime));
            base.Update(gameTime);
        }

        public void Add(BaseEntity entity)
        {
            entities.Add(entity);
        }

        public void ClearList()
        {
            
        }
    }
}
