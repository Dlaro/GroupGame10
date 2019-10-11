﻿using System;
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
            return new Block(this);
        }

        public override void Inilized()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
