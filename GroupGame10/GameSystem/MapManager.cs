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

        List<List<BaseEntity>> mapList;
        Dictionary<string, List<string[]>> mapData;
        Dictionary<string, ICloneable> entityDict ;
        CSVreader reader;
        Player player;

        internal List<List<BaseEntity>> MapList { get => mapList;private set => mapList = value; }
        internal Dictionary<string, List<string[]>> MapData { get => mapData; set => mapData = value; }

        public MapManager(Game game) : base(game)
        {
            MapList = new List<List<BaseEntity>>();
            MapData = new Dictionary<string, List<string[]>>();
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
           foreach(var entity in Setting.MapBlock)
            {
                if (entity.StartsWith("S")) { entityDict.Add(entity, new Sea(entity, new Vector2(32, 32), new Point(64, 64))); continue; }
                if (entity.StartsWith("B")) { entityDict.Add(entity, new Block(entity, new Vector2(32, 32), new Point(64, 64))); continue; }
                if (entity.StartsWith("E")) { entityDict.Add(entity, new Enemy(entity, new Vector2(32, 32), new Point(64, 64))); continue; }
                if (entity.StartsWith("I")) { entityDict.Add(entity, new Item(entity, new Vector2(32, 32), new Point(64, 64))); continue; }
                if (entity.StartsWith("0")) { entityDict.Add(entity, new Space(entity, new Vector2(32, 32), new Point(64, 64))); continue; }
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        private List<BaseEntity> Add(int lineCnt, string[] line)
          
        {

            List<BaseEntity> workList = new List<BaseEntity>();
            int colCnt = 0;
            foreach (var s in line)
            {
 
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
          
            MapData.Add(filename, data);
        }
        public List<List<BaseEntity>> GetMap(string name)
        {
            MapList = new List<List<BaseEntity>>();
            for (int linCnt = 0; linCnt < MapData[name].Count(); linCnt++)
            {
                MapList.Add(Add(linCnt, MapData[name][linCnt]));
            }
            return MapList;
        }

        public void ClearList()
        {
            
        }
    }
}

