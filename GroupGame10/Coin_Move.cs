using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;

namespace GroupGame10
{
    class Coin_Move : BaseEntity
    {
        int current = 0;
        Vector2 target;
        public Coin_Move(string name, Vector2 position, Point size)
        {
            Size = size;
            Name = name;
            Position = position;
            velocity = new Vector2(5,-5);
        }
        public override void Hit(BaseEntity other)
        {
            
        }

        public override void Inilized()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            velocity += target - Position;
            position += velocity;
        }
    }
}
