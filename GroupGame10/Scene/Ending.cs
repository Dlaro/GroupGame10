﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
using GroupGame10.GameSystem;
using Microsoft.Xna.Framework;
namespace GroupGame10
{
    class Ending : BaseScene
    {
        RenderManager renderManager;
        public Ending(Game game)
        {
            renderManager = (RenderManager)game.Components.First(b => b is RenderManager);
        }

 
        public override void Inilized()
        {
            IsEndFlag = false;
            renderManager.ClearList();
            renderManager.BackGrounds.Add(new BackGround("block", Vector2.Zero, Vector2.Zero));
            
        }



        public override void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        }
        public override void Draw(RenderManager renderManager)
        {

        }
        public override void Physics(PhysicsManager physicsManager)
        {
            
        }
    }
}
