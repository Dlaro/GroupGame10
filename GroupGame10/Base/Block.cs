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
    class Block : BaseEntity,ICloneable
    {
        public Block(string name,Vector2 position,Point size) 
        {
            Size = size;
            Name = name;
            Position = position;


        }
        public Block(Block other)
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
           
        }
        public override void Hit(BaseEntity other)
        {

        }
    }
}
