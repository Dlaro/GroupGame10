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
        //GamePlay06,
        //GamePlay07,
        Ending
    }
    static class Setting
    {
        public static readonly int MaxScene = 7;//scene合計
        public static readonly string[] TexturesLoad =
            {
            "player",
            "block",
            "enemy",
            "item",
            "bg1",
            "bg2",
            "bg3",
            "bg4",           
             "B74",
             "B75",
             "B163",
             "S386",
             "S417",
             "E88a",
             "E88b",
             "A394",
             "C270",
             "D181a",
             "D181b",
             "I84",
             "I84a",
             "I84b",
             "I84c",
             "I84d",
             "0",
             "1",
             "2",
             "3",
             "4",
             "5",
             "6",
             "7",
             "8",
             "9",
             "logo",
             "start",
             "GAMECLEAR",
             "GAMEOVER",
             "1star",
             "2star",
             "3star",
             "ready",
        };
        public static readonly string[] MapLoad =
            {
            "GamePlay01.csv",
            "GamePlay02.csv",
            "GamePlay03.csv",
            "GamePlay04.csv",
            "GamePlay05.csv",
            //"GamePlay06.csv",
            //"GamePlay07.csv",
        };

        public static readonly string[] MapBlock =
        {

                  "B74",
             "B75",
             "B163",
             "S386",
             "S417",
             "E88a",
             "E88b",
             "A394",
             "C270",
             "D181a",
             "D181b",
             "I84",
             "0"
        };

        public static readonly string[] SELoad =
        {
            "water",
            "coin",
            "dead",
            "clear",
            "over"
        };

        public static readonly string[] BGMLoad =
{
            "gamebgm"
        };





    }
}
