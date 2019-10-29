using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;


namespace GroupGame10.GameSystem
{
    class ScenceManager : GameComponent, IManager
    {


        Dictionary<Scecne, BaseScene> scences;
        public BaseScene CurrentSence { set; get; }

        private RenderManager renderManager;
        private MapManager mapManager;
        private PhysicsManager physicsManager;
        private UIManager uIManager;
        private Scecne curr;
        private Camera camera;
        int currentCamera = 0,currentPhy=0;
        bool isChanging = false;

        public ScenceManager(Game game) : base(game)
        {
            scences = new Dictionary<Scecne, BaseScene>();
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            mapManager = (MapManager)game.Components.First(b => b is MapManager);
            physicsManager = (PhysicsManager)game.Components.First(b => b is PhysicsManager);
            uIManager = (UIManager)game.Components.First(b => b is UIManager);
            camera = (Camera)game.Components.First(b => b is Camera);
            scences.Add(Scecne.Title, new Title(game));
            scences.Add(Scecne.Ending, new Ending(game));
            for (int i = 1; i < Setting.MaxScene - 1; i++)
            {

                scences.Add((Scecne)i, new GamePlay(game, ((Scecne)i).ToString()));
            }
            curr = 0;

            CurrentSence = scences[curr];
        }
        public override void Initialize()
        {
           NextScene(Scecne.Title);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {


            NextScene();
            //start

            if (!isChanging && !physicsManager.Enabled && CurrentSence.IsPlayer && Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)) { physicsManager.Enabled = true;renderManager.UIEntities.RemoveAll(ui=>ui.Name=="ready"); }
            //next
            if (!isChanging && !physicsManager.Enabled && CurrentSence.IsClear && Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)) { CurrentSence.IsEndFlag=true; return; }
            //restart
            if (!isChanging&&!physicsManager.Enabled &&!CurrentSence.IsPlayer&&!uIManager.IsShaking&& Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                NextScene(curr);
            }
            CurrentSence.Update(gameTime);
            base.Update(gameTime);
        }


        public void NextScene()
        {

            if (currentCamera >= 60)
            {
                currentCamera = 0;
                isChanging = false;
                camera.Focus = new Vector2(512, 320);

                ChangeScene(curr);
                return;
            }
            if (isChanging) { currentCamera++; return; }
            if (!CurrentSence.IsEndFlag) return;
            CameraDown();
            curr = (int)curr + 1 >= Setting.MaxScene ? 0 : curr + 1;
            renderManager.ClearList();
            mapManager.ClearList();
            physicsManager.ClearList();
            

        }
        public void NextScene(Scecne scence)
        {

            CameraDown();
            renderManager.ClearList();
            mapManager.ClearList();
            physicsManager.ClearList();

        }

        public void ChangeScene(Scecne scence)
        {

            CurrentSence = scences[scence];
            renderManager.ClearList();
            mapManager.ClearList();
            physicsManager.ClearList();
            uIManager.ClearList();
            CurrentSence.Inilized();
            if (CurrentSence is GamePlay)
            {
                renderManager.MapList = mapManager.GetMap(CurrentSence.Name + ".csv");
                physicsManager.MapList = renderManager.MapList;
                CurrentSence.Physics(physicsManager);
                physicsManager.Enabled = false;
            }
            CurrentSence.Draw(renderManager);

        }

        private void CameraDown()
        {
            camera.Focus = new Vector2(512, 960);
            currentCamera = 0;
            isChanging = true;
        }

        public void ClearList()
        {

        }
    }
}
