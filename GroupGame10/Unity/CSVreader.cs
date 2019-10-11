using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame10.Util
{
    /// <summary>
    /// 
    /// </summary>
    class CSVreader
    {
        private List<string[]> stringData;
        public CSVreader()
        {
            stringData = new List<string[]>();
        }
        public void Clear()
        {
            stringData.Clear();
        }
        public void Read(string filename, string path = "./")
        {
            Clear();

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

                        //出力する
                        foreach(var v in values)
                        {
                            System.Console.Write("{0}", v);
                        }
                        System.Console.WriteLine();
                    }
                }
            }catch(SystemException e)
            {
                System.Console.WriteLine(e.Message);
            }




        }


        public List<string[]> GetData()
        {
            return stringData;
        }

        public string[][] GetArrData()
        {
            return stringData.ToArray();
        }

        public int[][] GetIntData()
        {
            var data = GetArrData();
            int row = data.Count();
            int[][] intData = new int[row][];
            for (int i = 0; i < row; i++)
            {
                int col = data[i].Count();
                intData[i] = new int[col];
            }
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < intData[y].Count(); x++)
                {
                    intData[y][x] = int.Parse(data[y][x]);
                }
            }
            return intData;
        }

        public string[,] GetStrMatrix()
        {
            var data = GetArrData();
            int row = data.Count();//行
            int col = data[0].Count();//列
            string[,] result = new string[row, col];
            for (int y = 0; y < row; y++)
            {
                for(int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }
            return result;
        }

        public int[,] GetIntMatrix()
        {
            var data = GetIntData();
            int row = data.Count();
            int col = data[0].Count();
            int[,] result = new int[row, col];
            for (int y = 0; y <row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }
            return result;

        }
    }
}
