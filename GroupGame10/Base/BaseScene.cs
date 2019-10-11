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

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(RenderManager render);

        public abstract void Inilized();

    }
}
