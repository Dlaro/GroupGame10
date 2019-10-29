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
        int current;
        public Enemy(string name, Vector2 position, Point size)
        {
            Size = size;
            Name = name;
            Position = position;
        }

        public Enemy(Enemy other)
        {
            this.Size = other.Size;
            this.Name = other.Name;
            this.Position = other.Position;

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
            if (current >= 10)
            {
                if (Name.EndsWith("a"))
                {
                    Name = Name.Replace("a", "b");

                    return;
                }
                if (Name.EndsWith("b"))
                {
                    Name = Name.Replace("b", "a");
                    return;
                }
                current = 0;
            }

        }
        public override void Hit(BaseEntity other)
        {
            IsDeadFlag = true;
        }
    }
}
