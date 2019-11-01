using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    class Score : UIEntity
    {
        int num=0;
        public Score(string name, Vector2 position) : base(name, position)
        {

        }
        public Score(string name, Vector2 position,int num) : base(name, position)
        {
            this.num = num;
        }

        public int Num { get => num; set => num = value; }
    }
}
