using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    struct StructParticle
    {
        Point size;
        float curTime;
       
        public StructParticle(Vector2 player)
        {
            size = new Point(32, 32);
            curTime = 0;
            

        }
        public void Update(GameTime gameTime)
        {
            curTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
         //   if (curTime >= 0.2f)size= size * 0.8f;
        }
    }
}
