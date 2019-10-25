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
        Vector2 _velocity;
        bool isInWater = false;
        bool preIsInWater = false;
        SoundManager soundManager;
        float rotation;
        Vector2 a = new Vector2(0, 1);
        int b = 1;
        List<IObserver> observers;
        int numObservers = 0;
        string currCenter;
        string preCenter;

        public Player()
        {
            Name = "player";
            Size = new Point(48, 48);
            Position = new Vector2(256, 300);
            _velocity = new Vector2(8, 0);
            velocity = _velocity;
            observers = new List<IObserver>();
        }

        public override void Inilized()
        {

        }

        public override void Update(GameTime gameTime)
        {


            if (preCenter != currCenter && currCenter == "GroupGame10.Sea")
            {
                currentState = State.Surface;
                observers.ForEach(ob => ob.OnNotify("IntoWater"));
            }
            if (Input.IsKeyUp(Keys.Space) ) currentState = State.Rise;
            if (currCenter =="GroupGame10.Space") currentState = State.Air;
            if (Input.GetKeyState(Keys.Space))
            {
                currentState = State.Dive;
            }


            switch (currentState)
            {
                case State.Dive:
                    velocity.Y = (velocity.Y < 0 ? 0 : velocity.Y);
                    velocity.Y += 1f * b;
                    break;
                case State.Rise:
                    velocity.Y = (velocity.Y > -15 ? -15 : velocity.Y);
                    velocity.Y -= 1f * b;

                    velocity.Y = (velocity.Y < -22 ? -22 : velocity.Y);
                    break;
                case State.Air:

                    velocity.Y += 1f * b;
                    velocity.Y = (velocity.Y > 10 ? 10 : velocity.Y);
                    break;
                case State.Surface:
                    velocity.Y = 0;

                    break;
            }
            Rotation = (float)Math.Atan2((double)velocity.Y, (double)velocity.X) / 2;
            Position += velocity;
            preIsInWater = isInWater;
                  preCenter = currCenter;
        }

        private Vector2 Center()
        {
            return new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
        }
        public override void Hit(BaseEntity other)
        {if (other.Rectangle.Intersects(new Rectangle(Position.ToPoint(), Size))) currCenter=other.GetType().ToString();
            switch (other)
            {
                case Block a:
                    if (Math.Abs((a.Rectangle.Center.ToVector2() - Rectangle.Center.ToVector2()).Length()) < 64)
                        IsDeadFlag = true;

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
                    if (d.Rectangle.Intersects(new Rectangle(this.Rectangle.Center, new Point(1, 1))))
                    {
                        isInWater = true;
                    }
                    break;
                case Space e:
                    if (e.Rectangle.Intersects(new Rectangle(this.Rectangle.Center, new Point(1, 1))))
                    {
                        isInWater = false;
                    }
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
