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
    class Block_Move : Block
    {
        public Block_Move(string name, Vector2 position, Point size) : base(name, position, size)
        {
            velocity = new Vector2(0, -2);
           
        }

        public override void Inilized()
        {
            float a= Position.X;
            base.Inilized();
        }
        public override void Update(GameTime gameTime)
        {
            Position += velocity;
            if (Position.Y < 200||Position.Y>500) { velocity = -velocity; }
            base.Update(gameTime);
        }
    }
}
