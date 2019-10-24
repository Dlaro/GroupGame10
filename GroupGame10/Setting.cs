using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
namespace GroupGame10
{

    public enum Scecne //順番
    {
        Title,
        GamePlay01,
        GamePlay02,
        Ending
    }
    static class Setting
    {
        public static readonly int MaxScene = 4;//合計
        public static readonly string[] TexturesLoad =
            {
            "player",
            "block",
            "enemy",
            "item",
            "bg1",
            "bg2",
             "B310",
             "E320",
             "S200",
             "B210",
             "B211",
             "E220",
             "S100",
             "B110",
             "E120",
             "I999",
        };
        public static readonly string[] MapLoad =
            {
            "GamePlay01.csv",
            "GamePlay02.csv"
        };

        public static readonly string[] MapBlock =
        {

             "B310",
             "E320",
             "S200",
             "B210",
             "B211",
             "E220",
             "S100",
             "B110",
             "E120",
             "I999",
        };





    }
}
