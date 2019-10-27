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
            title = new UIEntity("logo", new Vector2(Screen.Width / 2, Screen.Height / 2));
            start = new UIEntity("start", new Vector2(Screen.Width / 2, Screen.Height / 2));
        }

        public override void Draw(RenderManager renderManager)
        {
            renderManager.Entities.Add(title);
            renderManager.Entities.Add(start);
        }

        public override void Inilized()
        {
            IsEndFlag = false;
            title = new UIEntity("logo",new Vector2(Screen.Width/2,Screen.Height/2));
            start = new UIEntity("start", new Vector2(Screen.Width / 2, Screen.Height / 2));

        }

   
        public override void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        }
        public override void Physics(PhysicsManager physicsManager)
        {

        }
    }
}