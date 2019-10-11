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
        public Item(string name,Vector2 position ,Point size)
        {
        }
        public Item(Item other)
        {
            this.Size = other.Size;
            this.Name = other.Name;
            this.Position = other.Position;
        }
        public object Clone()
        {
            return new Item(this);
        }

        public override void Inilized()
        {
        }

        public override void Update(GameTime gameTime)
        {
           
        }
    }
}
