using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;

namespace GroupGame10
{
    class Score : GameComponent, IObserver
    {
        int sum=0;
        int max=0;
        Game game;
        public Score(Game game) : base(game)
        {
            this.game = game;
        }

        public int Sum { get => sum; set => sum = value; }
        public int Max { get => max; set => max = value; }

        public void OnNotify(string file)
        {
            switch (file)
            {
                case "GetCoin":
                    sum++;
                    break;
                case "GetEnemy":
                    sum++;
                    break;
                default:
                    break;
            }
         
        }

        public override void Update(GameTime gameTime)
        {
            game.Window.Title = Sum.ToString() + " / " + Max.ToString();
            base.Update(gameTime);
        }
    }
}
