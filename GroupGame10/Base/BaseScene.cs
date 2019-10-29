using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.GameSystem;
namespace GroupGame10.Base
{
    abstract class BaseScene 
    {
        public string Name { get; protected set; }
        public bool IsEndFlag { get => isEndFlag; set => isEndFlag = value; }
        public bool IsPlayer { get => isPlayer; set => isPlayer = value; }
        public bool IsClear { get => isClear; set => isClear = value; }

        private bool isEndFlag=false;

        private bool isPlayer = false;

        private bool isClear = true;


        public abstract void Update(GameTime gameTime);

        public abstract void Physics(PhysicsManager physicsManager);

        public abstract void Draw(RenderManager renderManager);
        public abstract void Inilized();

    }
}
