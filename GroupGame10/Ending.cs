using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
using GroupGame10.GameSystem;
using Microsoft.Xna.Framework;
namespace GroupGame10
{
    class Ending : BaseScene
    {
        UIEntity ending;
        List<string[]> stringData;
        UIManager uIManager;
        bool isRank;
        int rank = 0;
        int total;
        public Ending(Game game)
        {
            uIManager = (UIManager)game.Components.First(com => com is UIManager);
            stringData = new List<string[]>();

        }


        public override void Inilized()
        {
            IsEndFlag = false;
            Read("rank.csv");
            total = uIManager.Total;
            for (int i = stringData.Count() - 1; i >= 0; i--)
            {
                var num = Int32.Parse(stringData[i][1]);
                if (num < total)
                {
                    stringData[i][1] = total.ToString();

                    rank = i;
                    if (i >= stringData.Count() - 1) continue;
                    stringData[i + 1][1] = num.ToString();
                }
                
            }
            if(total<=Int32.Parse(stringData.Last()[1]))
                {
                rank = stringData.Count();
                string[] word = { (rank+1).ToString(), total.ToString() };
                stringData.Add(word);
                
            }

            Write("rank.csv");
            ending = new UIEntity("ending", Vector2.Zero);

        }



        public override void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        }
        public override void Draw(RenderManager renderManager)
        {
            renderManager.UIEntities.Add(ending);

            for (int i = 0; i < 5; i++)
            {
                renderManager.UIEntities.Add(new SmallScore("rank", new Vector2(420, 20 * i +280), Int32.Parse(stringData[i][0])));
                renderManager.UIEntities.Add(new SmallScore("score", new Vector2(560, 20 * i +280), Int32.Parse(stringData[i][1])));
            }

            if(rank>4)
            {
                renderManager.UIEntities.Add(new UIEntity("point", new Vector2(490, 20 * 5 + 280)));
                renderManager.UIEntities.Add(new SmallScore("rank", new Vector2(420, 20 * 6+280), Int32.Parse(stringData[rank][0])));
                renderManager.UIEntities.Add(new SmallScore("score", new Vector2(560, 20 * 6 +280), Int32.Parse(stringData[rank][1])));
                renderManager.UIEntities.Add(new UIEntity("arr", new Vector2(380, 20 * 6 + 280)));
            }
            else
            {
                renderManager.UIEntities.Add(new UIEntity("arr", new Vector2(380, 64 *rank + 288)));
            }



        }
        public override void Physics(PhysicsManager physicsManager)
        {

        }
        public void Read(string filename, string path = "./")
        {


            stringData.Clear();
            try
            {
                using (var sr = new System.IO.StreamReader(@"Content/" + path + filename))
                {
                    while (!sr.EndOfStream)
                    {

                        //1行読み込む
                        var line = sr.ReadLine();

                        //カンマごとに分けてリストに格納する
                        var values = line.Split(',');

                        //リストに読み込んだ1行を追加
                        stringData.Add(values);



                    }
                    sr.Close();
                }
            }
            catch (SystemException e)
            {
                System.Console.WriteLine(e.Message);
            }




        }
        public void Write(string filename, string path = "./")
        {
            Stream fs = File.Open(@"Content/" + path + filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var strarr in stringData)
            {
                sw.WriteLine(strarr[0] + "," + strarr[1]);

            }

            sw.Close();
            fs.Close();

        }
    }
}
