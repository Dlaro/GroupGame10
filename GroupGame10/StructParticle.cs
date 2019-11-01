using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    class StructParticle
    {
        private Point size;
        private int curTime;
        private Vector2 position;
        Rectangle rectangle;
        private bool isDead;
        public StructParticle(Vector2 player)
        {
            size = new Point(32, 32);
            curTime = 0;
            position = player;
            isDead = false;
            position += new Vector2(-64, -32);
        }

        public bool IsDead { get => isDead; set => isDead = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Point Size { get => size; set => size = value; }

        public void Update()
        {

            size.X -= 1;
            size.Y -= 1;
            position += new Vector2(0.5f,0.5f);
            if (size.X < 5)
                isDead = true;
        }
    }
}
