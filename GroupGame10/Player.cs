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
        
        Vector2 a = new Vector2(0, 1);
        enum State { Dive, Rise, Air , Surface }
        State currentState = State.Air;
        public Player()
        {
            Name = "player";
            Size = new Point(128, 64);
            Position = new Vector2(128, 384);
            velocity = Vector2.Zero;
        }

        public override void Inilized()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Input.GetKeyState(Keys.Space)) currentState = State.Dive;
            if (Input.IsKeyUp(Keys.Space)) currentState = State.Rise;
            if (Position.Y < 350) currentState = State.Air;
            if (currentState==State.Air&& Position.Y>368) currentState = State.Surface;
            switch (currentState)
            {
                case State.Dive:
                    velocity = (velocity.Y < 0 ? Vector2.Zero : velocity);
                    velocity += 2 * a;
                    break;
                case State.Rise:
                    velocity -= 2 * a;
                    break;
                case State.Air:
                    velocity += 1 * a;
                    break;
                case State.Surface:
                    velocity = a*(Position.Y>368?-1:0);
                    
                    break;
            }


            
            // velocity *= 0.8f;
            Position += velocity;
        }
    }
}
