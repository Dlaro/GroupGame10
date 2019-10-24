using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
using GroupGame10.GameSystem;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    class GamePlay : BaseScene
    {
        PhysicsManager spriteManager;
        RenderManager renderManager;
        MapManager mapManager;
        Player player;
        Game game;
        string name;
        public GamePlay(Game game,string name)
        {
            this.game = game;
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            spriteManager = (PhysicsManager)game.Components.First(b => b is PhysicsManager);
            mapManager = (MapManager)game.Components.First(b => b is MapManager);
            this.name = name;


        }
  
        public override void Inilized()
        {
            IsEndFlag = false;
            renderManager.ClearList();
            mapManager.ClearList();
            player = new Player();
            renderManager.Add(player);

            (renderManager.MapList) = mapManager.GetMap("GamePlay01.csv");
            renderManager.BackGrounds.Add(new BackGround("bg1", new Vector2(1024, 0), new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg1", Vector2.Zero, new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", Vector2.Zero, new Vector2(-1, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", new Vector2(1024, 0), new Vector2(-1, 0)));


        }

        public override ScenceManager.Scence Next()
        {
            return ScenceManager.Scence.GamePlay02;
        }

        public override void Update(GameTime gameTime)
        {
            Hit();

            player.Update(gameTime);
            renderManager.BackGrounds.ForEach(a => a.Update(gameTime));
            if (player.IsDeadFlag)
            {
                ScenceManager sM = (ScenceManager)game.Components.First(b => b is ScenceManager);
                sM.Enabled = false;

            }

            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
            foreach (var list in renderManager.MapList)
            {
                list.RemoveAll(a => a.IsDeadFlag);
            }
        }
        private void Hit()
        {
            foreach (var list in renderManager.MapList)
            {
                foreach (var c in list)
                {
                    if (c.Rectangle.Intersects(player.Rectangle))
                        player.Hit(c);
                }
            }
        }
        public override void Draw(RenderManager renderManager)
        {

        }
        public override void Physics(PhysicsManager physicsManager)
        {

        }
    }
}

