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

        enum State { Dive, Rise, Air, Surface ,Water}
        State currentState = State.Air;
        Vector2 _velocity;
        float rotation;
        Vector2 a = new Vector2(0, 1);
        int b = 1;
        List<IObserver> observers;
        int numObservers = 0;
        string currCenter;
        string preCenter;
        bool isClear=false;

        public Player()
        {
            Name = "player";
            Size = new Point(48, 48);
            Position = new Vector2(256, 300);
            _velocity = new Vector2(8, 0);
            velocity = _velocity;
            observers = new List<IObserver>();
        }
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

        public bool IsClear { get => isClear; set => isClear = value; }

        public override void Inilized()
        {
            observers.ForEach(ob => ob.OnNotify("begin"));
        }

        public override void Update(GameTime gameTime)
        {
            if (position.X > 98 * 64) { observers.ForEach(ob => ob.OnNotify("clear")); IsClear = true; IsDeadFlag = true; }

            if (preCenter != currCenter &&currentState!=State.Rise &&currCenter == "S386")
            {
                currentState = State.Surface;
                observers.ForEach(ob => ob.OnNotify("IntoWater"));
            }
            if (currentState != State.Rise && currCenter == "S417") currentState = State.Water;
            if (Input.IsKeyUp(Keys.Space)) currentState = State.Rise;
            if (currCenter != "S417"&& currCenter != "S386") currentState = State.Air;
            if (Input.GetKeyState(Keys.Space))
            {
                currentState = State.Dive;
            }


            switch (currentState)
            {
                case State.Dive:
                    velocity.Y = (velocity.Y < 0 ? 0 : velocity.Y);
                    velocity.Y +=1f * b;
                    break;
                case State.Rise:
                    velocity.Y = (velocity.Y > -10 ? -10 : velocity.Y);
                    velocity.Y -= 1f * b;

                   
                    break;
                case State.Air:

                    velocity.Y += 1f * b;
                    velocity.Y = (velocity.Y > 10 ? 10 : velocity.Y);
                    break;
                case State.Surface:
                    velocity.Y = 0;

                    break;
                case State.Water:
                    velocity.Y = -5;
                    break;
            }
            velocity.Y = (velocity.Y < -20 ? -20 : velocity.Y);
            Rotation = (float)Math.Atan2((double)velocity.Y, (double)velocity.X) / 2;
            Position += velocity;
            preCenter = currCenter;
            Console.WriteLine(velocity.Y);
            if(currCenter=="I84") Console.WriteLine(currCenter);

        }

        private Vector2 Center()
        {
            return new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
        }
        public override void Hit(BaseEntity other)
        {
            var rec = other.Rectangle;
            var _rec = new Rectangle(rec.Left, rec.Center.Y-24, 64, 48);
            if (_rec.Intersects(new Rectangle(Position.ToPoint(),new Point(1,1)))) currCenter = other.Name;
            switch (other)
            {
                case Block a:
                    if (Math.Abs((a.Rectangle.Center.ToVector2() - Rectangle.Center.ToVector2()).Length()) < 48)
                    {
                        observers.ForEach(ob => ob.OnNotify("dead"));
                        IsDeadFlag = true;
                    }

                    break;
                case Enemy b:
                    b.Hit(this);
                    observers.ForEach(ob => ob.OnNotify("GetEnemy"));
                    break;
                case Item c:
                    c.Hit(this);
                    observers.ForEach(ob => ob.OnNotify("GetCoin"));
                    break;
                case Sea d:
                    d.Hit(this);
                    break;
                case Space e:
                    break;
                default:
                    return;

            }

        }
        private void HitBlock(Block block)
        {

        }
        public void AddObserver(IObserver ob)
        {
            if (observers.Contains(ob)) return;
            observers.Add(ob);
            numObservers++;
        }
        public void RemoveObserver(IObserver ob)
        {

            if (!observers.Contains(ob)) return;
            observers.Remove(ob);
            numObservers--;
        }



    }
}
