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
    class GamePlay01 : BaseScene
    {
        RenderManager renderManager;
        MapManager mapManager;
        Player player;

        Game game;
        public GamePlay01(Game game)
        {
            Name = "GamePlay01";
            this.game = game;
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            mapManager = (MapManager)game.Components.First(b => b is MapManager);

        }
 
        public override void Inilized()
        {
            IsEndFlag = false;
            renderManager.ClearList();
            mapManager.ClearList();
            player = new Player();



        }

        public override ScenceManager.Scence Next()
        {
            return ScenceManager.Scence.GamePlay02;
        }

        public override void Update(GameTime gameTime)
        {
                     
           
            
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        
        }
        public override void Draw(RenderManager renderManager)
        {

            renderManager.Add(player);
            renderManager.BackGrounds.Add(new BackGround("bg1", new Vector2(1024, 0), new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg1", Vector2.Zero, new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", Vector2.Zero, new Vector2(-1, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", new Vector2(1024, 0), new Vector2(-1, 0)));
        }
        public override void Physics(PhysicsManager physicsManager)
        {
            physicsManager.Add(player);

        }
    }
}
