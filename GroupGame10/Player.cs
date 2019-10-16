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
    class Player : BaseEntity
    {
        float rotation;
        Vector2 a = new Vector2(0, 1);
        int b = 1;
        enum State { Dive, Rise, Air, Surface }
        State currentState = State.Air;

        public float Rotation
        {
            get => rotation;
            set
            {
                if (value > 0.5f) value = 0.5f;
                if (value < -0.5f) value = -0.5f;
                rotation = value;
            }
        }
        public override Vector2 Position
        {
            get => base.Position; set
            {
                if (value.Y > Screen.Height - Size.Y / 2)
                { value.Y = Screen.Height - Size.Y / 2; velocity.Y = 0; }
                if (value.Y < Size.Y / 2) { value.Y = Size.Y / 2; velocity.Y = 0; }
                base.Position = value;
            }
        }
        int line = 0;
        public Player()
        {
            line = 484;
            Name = "player";
            Size = new Point(64, 64);
            Position = new Vector2(256, line);
            velocity = new Vector2(10, 0);
        }

        public override void Inilized()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Input.GetKeyState(Keys.Space)) currentState = State.Dive;
            if (Input.IsKeyUp(Keys.Space)) currentState = State.Rise;
            if (Position.Y < line - 32) currentState = State.Air;
            if (currentState == State.Air && Position.Y > line) currentState = State.Surface;
            switch (currentState)
            {
                case State.Dive:
                    velocity.Y = (velocity.Y < 0 ? 0 : velocity.Y);
                    velocity.Y += 0.5f * b;
                    break;
                case State.Rise:
                    velocity.Y = (velocity.Y > 0 ? 0 : velocity.Y);
                    velocity.Y -= 2f * b;
                    break;
                case State.Air:
                    velocity.Y += 1.5f * b;
                    break;
                case State.Surface:
                    velocity.Y = b * (Position.Y > line ? -2 : 0);

                    break;
            }
            Rotation = (float)Math.Atan2((double)velocity.Y, (double)velocity.X) / 2;
            Position += velocity;
        }

        public override void Hit(BaseEntity other)
        {
            switch  (other)
            {
                case Block a:
                    IsDeadFlag = true;
                    
                    break;
                case Enemy b:
                    b.Hit(this);
                    break;
                case Item c:
                    c.Hit(this);
                    break;
                default:
                    return;
            }
        }
        private void HitBlock(Block block)
        {

        }
    }
}
