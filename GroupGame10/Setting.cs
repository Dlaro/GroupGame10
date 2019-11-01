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
        GamePlay08,
        Ending
    }
    static class Setting
    {
        public static readonly int MaxScene = 10;//scene合計
        public static readonly string[] TexturesLoad =
            {
            "player0",
            "player1",
            "player2",
            "player3",
            "player4",
            "player5",
            "player6",
            "player7",
            "player8",
            "player9",
            "player10",
            "player11",
            "player12",
            "player13",
            "player14",
            "player15",
            "player16",
            "player17",
            "player18",
            "player19",
            "player20",            
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
             "E88c",
             "E88d",
             "E88e",
             "A394",
             "C270",
             "D181a",
             "D181b",
             "D181c",
             "D181d",
             "D181e",
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
              "X",
              "ending",
              "F1",
             "F2",
             "F3",
             "F4",
             "F5",
             "F6",
             "F7",
             "F8",
             "arr",
             "point",
             "white",
             "water0",
             "water1",
                "water2",
                "water3",
                "water4",
                "water5",
                "water6",

                "water7",
                "water8"

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
            "GamePlay08.csv"
        };

        public static readonly string[] MapBlock =
        {

             "B74",
             "B75",
             "B163",
             "S386",
             "S417",
             "E88",
           
             "A394",
             "C270",
             "D181",
             
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
