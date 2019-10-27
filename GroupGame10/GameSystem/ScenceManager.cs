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
       
        
        Dictionary<Scecne, BaseScene> scences;
        public BaseScene CurrentSence { set; get; }

        private RenderManager renderManager;
        private MapManager mapManager;
        private PhysicsManager physicsManager;
        private Scecne curr;
       

        public ScenceManager(Game game) : base(game)
        {
            scences = new Dictionary<Scecne, BaseScene>();           
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            mapManager = (MapManager)game.Components.First(b => b is MapManager);
            physicsManager = (PhysicsManager)game.Components.First(b => b is PhysicsManager);
            scences.Add(Scecne.Title, new Title(game));
            scences.Add(Scecne.Ending, new Ending(game));
            for(int i = 1; i < Setting.MaxScene - 1; i++)
            {

                scences.Add((Scecne)i, new GamePlay(game, ((Scecne)i).ToString()));
            }
            curr = 0;
            
            CurrentSence = scences[curr];
        }
        public override void Initialize()
        {
            ChangeScene(Scecne.Title);
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            NextScene();
            //start
            if (!physicsManager.Enabled && Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                physicsManager.Enabled = true;
            }
            CurrentSence.Update(gameTime);
            //restart
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                ChangeScene(curr);
            }
            base.Update(gameTime);
        }

   
        public void NextScene()
        {
            if (!CurrentSence.IsEndFlag) return;
            curr = (int)curr + 1 >=Setting.MaxScene ? 0 : curr + 1;
            ChangeScene(curr);
        }
        public void ChangeScene(Scecne scence)
        {
            CurrentSence = scences[scence];
            renderManager.ClearList();
            mapManager.ClearList();
            physicsManager.ClearList();
            CurrentSence.Inilized();
            CurrentSence.Physics(physicsManager);
            if (CurrentSence is GamePlay)
            {
                renderManager.MapList = mapManager.GetMap(CurrentSence.Name + ".csv");
                physicsManager.MapList = renderManager.MapList;
            }
            CurrentSence.Draw(renderManager);
        }

        public void ClearList()
        {
            
        }
    }
}
