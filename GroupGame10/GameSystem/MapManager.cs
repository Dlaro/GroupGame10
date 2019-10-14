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
        List<Block> blockList;
        List<Enemy> enemyList;
        List<Item> itemList;
        List<List<BaseEntity>> mapList;
        Dictionary<string, ICloneable> entityDict ;
        CSVreader reader;
        Player player;

        internal List<List<BaseEntity>> MapList { get => mapList;private set => mapList = value; }

        public MapManager(Game game) : base(game)
        {
            enemyList = new List<Enemy>();
            MapList = new List<List<BaseEntity>>();
            itemList = new List<Item>();
            blockList = new List<Block>();
            entityDict = new Dictionary<string, ICloneable>();
            reader = new CSVreader();
        }
        public override void Initialize()
        {
            AddSample();

            base.Initialize();
        }

        private void AddSample()
        {
            entityDict.Add("101", new Block("block", new Vector2(32, 32), new Point(64, 64)));
            entityDict.Add("201", new Enemy("enemy", new Vector2(32, 32), new Point(64,64))) ;
            entityDict.Add("301", new Item("item", new Vector2(32, 32), new Point(64, 64)));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public List<List<BaseEntity>> GetMap()
        {
            return MapList;
        }

        private List<BaseEntity> Add(int lineCnt, string[] line)
          
        {

            List<BaseEntity> workList = new List<BaseEntity>();
            int colCnt = 0;
            foreach (var s in line)
            {
                if (s == "") { colCnt += 1; continue; }
                try
                {
                    BaseEntity work = (BaseEntity)entityDict[s].Clone();

                    //何行何列のデータの座標を計算し登録
                    work.Position = (new Vector2(colCnt * work.Size.X+32, lineCnt * work.Size.Y+32));

                    //座標設置したのでリストに登録
                    workList.Add(work);

                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);

                }
                colCnt += 1;

            }

            return workList;
        }

        public void Load(string filename, string path = "./")
        {

            CSVreader cSVreader = new CSVreader();
            cSVreader.Read(filename, path);
            var data = cSVreader.GetData();

            for (int linCnt = 0; linCnt < data.Count(); linCnt++)
            {
                MapList.Add(Add(linCnt, data[linCnt]));
            }
        }

        public void ClearList()
        {
            
        }
    }
}

