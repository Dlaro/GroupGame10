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

        }
        public override void Hit(BaseEntity other)
        {
            IsDeadFlag = true;
        }
    }
}
