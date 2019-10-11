using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;

namespace GroupGame10.GameSystem
{
    class ScenceManager : DrawableGameComponent,IManager
    {
       public enum Scence { Title,GamePlay01,Ending}
        public BaseScene CurrentSence { set; get; }

        private RenderManager renderManager;

        public ScenceManager(Game game) : base(game)
        {
            CurrentSence = new GamePlay01(game);
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
        }
        public override void Initialize()
        {
            CurrentSence.Inilized();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            CurrentSence.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            CurrentSence.Draw(renderManager);
            base.Draw(gameTime);
        }
        public void NextScene()
        {

        }

    }
}
