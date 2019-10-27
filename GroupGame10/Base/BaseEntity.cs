using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.GameSystem;
using Microsoft.Xna.Framework.Design;
namespace GroupGame10.Base
{
    abstract class BaseEntity 
    {

        protected Vector2 velocity;

        private bool isDeadFlag;
        public string Name { get; protected set; }
        protected Vector2 position;
        public virtual  Vector2 Position
        {
            get => position;
            set
            {
                
                position = value;
                Rectangle = new Rectangle((int)value.X - Size.X / 2, (int)value.Y - Size.Y / 2, Size.X, Size.Y);
            }
        }

        public Rectangle Rectangle { get; set; }

        public Point Size { get; protected set; }
        public bool IsDeadFlag { get => isDeadFlag; set => isDeadFlag = value; }

        public abstract void Update(GameTime gameTime);

        public abstract void Inilized();

        public abstract void Hit(BaseEntity other);


       
    }
}
