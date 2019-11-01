using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    class Item : BaseEntity,ICloneable
    {
        int current = 0;
        bool isMove=false;
        Vector2 target;
        BaseEntity other;

        public bool IsMove { get => isMove; set => isMove = value; }

        public Item(string name,Vector2 position ,Point size)
        {
            Size = size;
            Name = name;
            Position = position;
        }
        public Item(Item other)
        {
            Size = other.Size;
            Name = other.Name;
            Position = other.Position;
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
            if (current >= 5)
            {
                current = 0;
                if (Name.EndsWith("a"))
                {
                    Name=Name.Replace("a", "b");
                    
                    return;
                }
                if (Name.EndsWith("b"))
                {
                    Name = Name.Replace("b", "c");
                    return;
                }
                if (Name.EndsWith("c"))
                {
                    Name = Name.Replace("c", "d");
                    return;
                }
                if (Name.EndsWith("d"))
                {
                    Name = Name.Replace("d", "a");
                    return;
                }
              
            }
            if (IsMove)
            {
                target = new Vector2(other.Position.X-120, 0);
                if (target.X > 92 * 64 - 7 * 64) target.X = 92 * 64 - 7 * 64;
                velocity =(target - Position);
                Position += velocity / 10f;
                if (Position.Y<64&&Position.X<target.X) IsDeadFlag = true;
                if (current >= 4)
                {
                    Size = new Point((int)(Size.X *0.8));
                }

            }
        }
        public override void Hit(BaseEntity other)
        {
            IsMove = true;
            velocity = new Vector2(1, -1);
            this.other = other;
        }
    }
}
