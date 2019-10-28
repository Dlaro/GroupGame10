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
        UIEntity star;
        bool isShaking=false;
        float current;
        float cyc=0.8f;
        int sum = 0;
        int max = 0;
        public int Sum { get => sum; set => sum = value; }
        public int Max { get => max; set => max = value; }
        public UIManager(Game game) : base(game)
        {
            gameover = new UIEntity("GAMEOVER",Vector2.Zero);
            gameclear = new UIEntity("GAMECLEAR", Vector2.Zero);
            star = new UIEntity("2star", Vector2.Zero);
            renderManager =(RenderManager) game.Components.First(c => c is RenderManager);
        }

        public override void Draw(GameTime gameTime)
        {
            if (isShaking)
            {
                current +=(float) gameTime.ElapsedGameTime.TotalSeconds;
                if (current >= cyc)
                {
                    renderManager.UIEntities.Add(gameover);
                    renderManager.UIEntities.Add(star);
                    isShaking = false;
                }
                
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if(isShaking)
            base.Update(gameTime);
        }

        public void ClearList()
        {
            throw new NotImplementedException();
        }

        public void OnNotify(string file)
        {
            switch (file)
            {
                case "dead":

                    isShaking = true;
                    current = 0;
                    break;
                case "clear":
                    renderManager.UIEntities.Add(gameclear);
                    renderManager.UIEntities.Add(star);
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
