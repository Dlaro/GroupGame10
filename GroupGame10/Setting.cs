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
        GamePlay03,
        GamePlay04,
        GamePlay05,
        GamePlay06,
        GamePlay07,
        Ending
    }
    static class Setting
    {
        public static readonly int MaxScene = 9;//scene合計
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
            "GamePlay02.csv",
            "GamePlay03.csv",
            "GamePlay04.csv",
            "GamePlay05.csv",
            "GamePlay06.csv",
            "GamePlay07.csv",
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
             "0"
        };

        public static readonly string[] SELoad =
        {
            "water",
            "coin"
        };

        public static readonly string[] BGMLoad =
{

        };





    }
}
