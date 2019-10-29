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
    class Title : BaseScene
    {
        UIEntity title,start;
        
        public Title(Game game)
        {

        }

        public override void Draw(RenderManager renderManager)
        {
            renderManager.UIEntities.Add(title);
            renderManager.UIEntities.Add(start);
        }

        public override void Inilized()
        {
            IsEndFlag = false;
            title = new UIEntity("logo", Vector2.Zero);
            start = new UIEntity("start", Vector2.Zero);

        }

   
        public override void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)) IsEndFlag = true;
        }
        public override void Physics(PhysicsManager physicsManager)
        {

        }
    }
}