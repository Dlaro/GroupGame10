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
    class IntoWater : BaseEntity
    {

        Vector2 _position;
        Rectangle moveLimit;
        string _name;
        int current;
        public IntoWater(Vector2 position)
        {
            _name = "water";
            Name =_name+0;
            Size = new Point(96, 96);
            velocity = Vector2.Zero;
            Position = position+new Vector2(0,-10);
            _position = position;

        }
        

        public override void Inilized()
        {
            Position = _position;
        }

        public override void Update(GameTime gameTime)
        {

            current++;
            
            Name = _name + current / 3;
            if (current >= 26) IsDeadFlag=true;



        }
        public override void Hit(BaseEntity other)
        {

        }

       
    }
}
