using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using GroupGame10.Base;

namespace GroupGame10.GameSystem
{
    class UIManager : DrawableGameComponent, IManager,IObserver
    {
        RenderManager renderManager;
        UIEntity gameover;
        UIEntity gameclear;
        Score score;
        UIEntity coin;
        UIEntity ready;
        UIEntity xx;
        bool isShaking=false;
        float current;
        float cyc=0.8f;
        int sum = 0;
        int total = 0;
        public int Sum { get => sum; set => sum = value; }
        public int Total { get => total; set => total = value; }
        public bool IsShaking { get => isShaking; set => isShaking = value; }

        public UIManager(Game game) : base(game)
        {
            score = new Score("score", new Vector2(128, 0));
            gameover = new UIEntity("GAMEOVER",Vector2.Zero);
            gameclear = new UIEntity("GAMECLEAR", Vector2.Zero);
            ready = new UIEntity("ready", Vector2.Zero);
            coin = new UIEntity("I84", Vector2.Zero,Vector2.Zero,new Rectangle(Point.Zero,new Point(128,128)));
            xx = new UIEntity("X", new Vector2(64, 0));
            renderManager =(RenderManager) game.Components.First(c => c is RenderManager);
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsShaking)
            {
                current +=(float) gameTime.ElapsedGameTime.TotalSeconds;
                if (current >= cyc)
                {
                    renderManager.UIEntities.Add(gameover);
  
                    IsShaking = false;
                }
                
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            score.Num=sum+total;
            base.Update(gameTime);
        }

        public void ClearList()
        {

            total = 0;
            sum = 0;
        }

        public void OnNotify(string file)
        {
            switch (file)
            {
                case "begin":
                    renderManager.UIEntities.Add(score);
                    renderManager.UIEntities.Add(coin);
                    renderManager.UIEntities.Add(xx);
                   
                   
                    break;

                case "dead":

                    IsShaking = true;
                    current = 0;
                    sum = 0;
                    break;
                case "clear":
                    renderManager.UIEntities.Add(gameclear);
                    total += sum;
                    sum = 0;
                    break;
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

        public void OnNotify(string file, Vector2 position)
        {
            switch (file)
            {
                case "IntoWater":
                    renderManager.Effects.Add((BaseEntity)new IntoWater(position));
                    break;
                        
                case "begin":
                    renderManager.UIEntities.Add(score);
                    renderManager.UIEntities.Add(coin);
                    renderManager.UIEntities.Add(xx);


                    break;

                case "dead":

                    IsShaking = true;
                    current = 0;
                    sum = 0;
                    break;
                case "clear":
                    renderManager.UIEntities.Add(gameclear);
                    total += sum;
                    sum = 0;
                    break;
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
    }
}
