using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;
using GroupGame10.Util;

namespace GroupGame10.GameSystem
{
    class MapManager : GameComponent, IManager
    {
        List<Block> mapList;
        List<Enemy> enemyList;
        List<Item> itemList;
        Dictionary<string, BaseEntity> entityDict ;
        CSVreader reader;
        public MapManager(Game game) : base(game)
        {
            enemyList = new List<Enemy>();
            mapList = new List<Block>();
            itemList = new List<Item>();
            entityDict = new Dictionary<string, BaseEntity>();
            reader = new CSVreader();
        }
        public override void Initialize()
        {
            AddSample();

            base.Initialize();
        }

        private void AddSample()
        {
            entityDict.Add("101", new Block("block", Vector2.Zero, new Point(64, 64)));
            entityDict.Add("201", new Enemy("enemy", Vector2.Zero,new Point(64,64))) ;
            entityDict.Add("301", new Item("item", Vector2.Zero, new Point(64, 64)));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

