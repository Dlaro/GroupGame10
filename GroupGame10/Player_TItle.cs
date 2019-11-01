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
    class Player_Title : BaseEntity
    {

        enum State { Dive, Rise, Air, Surface, Water }
        State currentState = State.Air;
        Vector2 _velocity;
        float rotation;
        Vector2 a = new Vector2(0, 1);
        int b = 1;
        List<IObserver> observers;
        int numObservers = 0;
        string currCenter;
        string preCenter;
        bool isClear = false;
        bool turn = false;
        int current = 0;
        string _name;
        int currentMove=0;
        public Player_Title()
        {

            _name = "player";
            Name = "player0";
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
        public bool Turn { get => turn; set => turn = value; }

        public override void Inilized()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            current++;

            if (current >= 420)
            {

                current = 0;
            }
            Name = _name + (current % 20).ToString();
            if (Rectangle.Center.Y > 64 * 6) currCenter = "water";
            else currCenter = "air";
            if (preCenter == "air" && currCenter == "water")
            {
                currentState = State.Surface;
                observers.ForEach(ob => ob.OnNotify("IntoWater",position));
            }
            if (preCenter == "water" && currCenter == "air") currentState = State.Air;

            currentMove++;
            if (currentMove >= 200)
            {
                currentState = State.Dive;
                if (currentMove >= 300)
                {
                    currentState = State.Rise;
                    currentMove = 0;
                }
            }
            
            switch (currentState)
            {
                case State.Dive:
                    velocity.Y = (velocity.Y < 0 ? 0 : velocity.Y);
                    velocity.Y += 1f * b;
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
            if(turn) Rotation = (float)Math.Atan2((double)velocity.Y, (double)-velocity.X) / 2;
            if (position.X > 1200 || position.X < -100) { Turn = !Turn; velocity.X = -velocity.X; }
            Position += velocity;
            preCenter = currCenter;


        }

        private Vector2 Center()
        {
            return new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
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

        public override void Hit(BaseEntity other)
        {
           
        }
    }
}