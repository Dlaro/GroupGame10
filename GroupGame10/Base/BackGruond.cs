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
    class BackGround : BaseEntity
    {
        Vector2 _position;
        public BackGround(string name,Vector2 position,Vector2 vel)
        {
            Name = name;
            Size = new Point(Screen.Width, Screen.Height);
            velocity = vel;
            Position = position;
            _position=position;

        }

        public override void Inilized()
        {
            Position = _position;
        }

        public override void Update(GameTime gameTime)
        {
            Position += velocity;
            if (Position.X <_position.X -1024) Inilized();
        }
        public override void Hit(BaseEntity other)
        {

        }
    }
}
