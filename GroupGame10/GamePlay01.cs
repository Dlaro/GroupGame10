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
        PhysicsManager spriteManager;
        RenderManager renderManager;
        Player player;
        public GamePlay01(Game game)
        {
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
            spriteManager = (PhysicsManager) game.Components.First(b => b is PhysicsManager);
           

        }
        public override void Draw(RenderManager render)
        {
           
        }

        public override void Inilized()
        {
            player = new Player();
            renderManager.Add(player);
            spriteManager.Add(player);
        }

        public override void Update(GameTime gameTime)
        {
           
        }
    }
}
