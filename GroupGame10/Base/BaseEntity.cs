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
        public string Name { get; protected set; }
        private Vector2 position;
        public Vector2 Position
        {
            get => position;
            set
            {
                if (value.Y > 768) { value.Y = 768; velocity = Vector2.Zero; }
                if (value.Y < 0) { value.Y = 0; velocity = Vector2.Zero; }
                position = value;
                Rectangle = new Rectangle((int)value.X - Size.X / 2, (int)value.Y - Size.Y / 2, Size.X, Size.Y);
            }
        }

        public Rectangle Rectangle { get; set; }

        public Point Size { get; protected set; }

        public abstract void Update(GameTime gameTime);

        public abstract void Inilized();


       
    }
}
