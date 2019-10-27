using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using GroupGame10.Base;
using GroupGame10.GameSystem;
using Microsoft.Xna.Framework.Input;

namespace GroupGame10
{
    class UIEntity : BaseEntity
    {
        
        Vector2 _position;
        Rectangle moveLimit;
        public UIEntity(string name, Vector2 position)
        {
            Name = name;
            Size = new Point(Screen.Width, Screen.Height);
            velocity = Vector2.Zero;
            Position = position;
            _position = position;

        }
        public UIEntity(string name, Vector2 position, Vector2 vel,Rectangle rectangle)
        {
            Name = name;
            Size = new Point(Screen.Width, Screen.Height);
            velocity = vel;
            Position = position;
            _position = position;
            moveLimit = rectangle;

        }

        public override void Inilized()
        {
            Position = _position;
        }

        public override void Update(GameTime gameTime)
        {
            
            Position += velocity;
            if (Position.Y > moveLimit.Bottom || Position.Y < moveLimit.Top) velocity.Y = -velocity.Y;
            if (Position.X > moveLimit.Right || Position.X < moveLimit.Left) velocity.X = -velocity.X;


        }
        public override void Hit(BaseEntity other)
        {

        }
    }
}
