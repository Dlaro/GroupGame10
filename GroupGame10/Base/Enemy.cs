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
    class Enemy : BaseEntity, ICloneable
    {
        int current=0,currentVel=0;
        public Enemy(string name, Vector2 position, Point size)
        {
            Size = size;
            Name = name;
            Position = position;
            velocity = new Vector2(-1, 1);
        }

        public Enemy(Enemy other)
        {
            this.Size = other.Size;
            this.Name = other.Name;
            this.Position = other.Position;
            velocity = new Vector2(-1, 1);

        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override void Inilized()
        {

        }

        public override void Update(GameTime gameTime)
        {
            current++;
            currentVel++;
            if (current >= 20)
            {
                if (Name.EndsWith("a"))
                {
                    Name = Name.Replace("a", "b");

                    
                }
                else if (Name.EndsWith("b"))
                {
                    Name = Name.Replace("b", "a");
                    
                }
                current = 0;
            }
            if (currentVel >= 120)
            {
                velocity.Y = -velocity.Y;
                currentVel = 0;
            }
            Position += velocity;

        }
        public override void Hit(BaseEntity other)
        {
            
        }
    }
}
