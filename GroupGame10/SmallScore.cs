using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    class SmallScore : UIEntity
    {
        int num = 0;
        public SmallScore(string name, Vector2 position) : base(name, position)
        {

        }
        public SmallScore(string name, Vector2 position, int num) : base(name, position)
        {
            this.num = num;
            
            Rectangle = new Rectangle((int)position.X,(int) position.Y, 32, 32);
        }

        public int Num { get => num; set => num = value; }
    }
}