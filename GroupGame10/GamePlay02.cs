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
    class GamePlay02 : BaseScene
    {
        PhysicsManager spriteManager;
        RenderManager renderManager;
        MapManager mapManager;
        Player player;
        public GamePlay02(Game game)
        {

            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            spriteManager = (PhysicsManager)game.Components.First(b => b is PhysicsManager);
            mapManager = (MapManager)game.Components.First(b => b is MapManager);

        }
        public override void Draw(RenderManager render)
        {

        }

        public override void Inilized()
        {
            IsEndFlag = false;
            renderManager.ClearList();
            mapManager.ClearList();
            player = new Player();
            renderManager.Add(player);
            spriteManager.Add(player);
            renderManager.MapList = mapManager.MapList;
            renderManager.BackGrounds.Add(new BackGround("bg1", new Vector2(1024, 0), new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg1", Vector2.Zero, new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", Vector2.Zero, new Vector2(-1, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", new Vector2(1024, 0), new Vector2(-1, 0)));



        }

        public override ScenceManager.Scence Next()
        {
            return ScenceManager.Scence.Ending;
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        }
    }
}
