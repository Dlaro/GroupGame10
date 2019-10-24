using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;

namespace GroupGame10.GameSystem
{
    class ScenceManager : GameComponent,IManager
    {
       public enum Scence { Title,GamePlay01,GamePlay02,Ending}
        Dictionary<Scence, BaseScene> scences;
        public BaseScene CurrentSence { set; get; }

        private RenderManager renderManager;
        private MapManager mapManager;
        private PhysicsManager physicsManager;

        public ScenceManager(Game game) : base(game)
        {
            scences = new Dictionary<Scence, BaseScene>();
           
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            mapManager = (MapManager)game.Components.First(b => b is MapManager);
            physicsManager = (PhysicsManager)game.Components.First(b => b is PhysicsManager);
            scences.Add(Scence.GamePlay01, new GamePlay01(game));
            scences.Add(Scence.GamePlay02, new GamePlay02(game));
            scences.Add(Scence.Title, new Title(game));
            scences.Add(Scence.Ending, new Ending(game));
            CurrentSence = scences[Scence.Title];
        }
        public override void Initialize()
        {


            CurrentSence.Inilized();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            NextScene();
            CurrentSence.Update(gameTime);
            base.Update(gameTime);
        }

   
        public void NextScene()
        {
            if (!CurrentSence.IsEndFlag) return;
            CurrentSence = scences[CurrentSence.Next()];
            renderManager.ClearList();
            mapManager.ClearList();
            physicsManager.ClearList();
            CurrentSence.Inilized();
            CurrentSence.Physics(physicsManager);
            if (CurrentSence is GamePlay01)
            {
                renderManager.MapList = mapManager.GetMap(CurrentSence.Name+".csv");
                physicsManager.MapList = renderManager.MapList;
            }
                CurrentSence.Draw(renderManager);
        }

        public void ClearList()
        {
            
        }
    }
}
