using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GroupGame10.Base;

namespace GroupGame10.GameSystem
{
    class Camera :GameComponent ,IObserver
    {
       
            private Vector2 _position;
            protected float _viewportHeight;
            protected float _viewportWidth;

            public Camera(Game game)
                : base(game)
            { }

            #region Properties

            public Vector2 Position
            {
                get { return _position; }
                set {
                    _position = value;
            }
            }
            public float Rotation { get; set; }
            public Vector2 Origin { get; set; }
            public float Scale { get; set; }
            public Vector2 ScreenCenter { get; protected set; }
            public Matrix Transform { get; set; }
            public Player Focus { get; set; }
            public float MoveSpeed { get; set; }

            #endregion

            /// <summary>
            /// Called when the GameComponent needs to be initialized. 
            /// </summary>
            public override void Initialize()
            {
                _viewportWidth = Game.GraphicsDevice.Viewport.Width;
                _viewportHeight = Game.GraphicsDevice.Viewport.Height;

                ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
                Scale = 1;
                MoveSpeed = 5f;
            Position = new Vector2(512, 320);
                base.Initialize();
            }

            public override void Update(GameTime gameTime)
            {
            // Create the Transform used by any
            // spritebatch process
            
            Shake(gameTime);
            Transform = Matrix.Identity *
                            Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                            Matrix.CreateRotationZ(Rotation) *
                            Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                            Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

                Origin = ScreenCenter / Scale;

            if (Focus != null)
            {
                // Move the Camera to the position that it needs to go
                var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                _position.X += (Focus.Position.X - Position.X+312) * MoveSpeed * delta;
                if (_position.X < 512) _position.X = 512;
                if (_position.X >92*64 ) _position.X = 92*64;
            }
            Shake(gameTime);
                base.Update(gameTime);
            }

            /// <summary>
            /// Determines whether the target is in view given the specified position.
            /// This can be used to increase performance by not drawing objects
            /// directly in the viewport
            /// </summary>
            /// <param name="position">The position.</param>
            /// <param name="texture">The texture.</param>
            /// <returns>
            ///     <c>true</c> if [is in view] [the specified position]; otherwise, <c>false</c>.
            /// </returns>
            public bool IsInView(Vector2 position, Texture2D texture)
            {
                // If the object is not within the horizontal bounds of the screen

                if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
                    return false;

                // If the object is not within the vertical bounds of the screen
                if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
                    return false;

                // In View
                return true;
            }
        int cycleCount = 4;
         float cycleTime = 0.2f;
         int curCount =6;
        float curTime=0;
        float curAngle;
        private void Shake(GameTime gameTime)
        {
            if (curCount >= cycleCount) return;
           
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            curTime += delta;
            if (curTime > cycleTime)
            {
                curTime -=cycleTime ;
                curCount++;
             if(curCount >= cycleCount)
                {
                    Rotation = 0;
                    return;
                }
                curAngle = (cycleCount - curCount) * 0.2f/ cycleCount;
                
            }

            float offsetScale = (float)Math.Sin(2f * Math.PI *( curTime / cycleTime)-Math.PI/2f);
            Rotation = curAngle * offsetScale;




        }
        public void DoShake()
        {
            curCount = 0;
            curTime = 0;
        }

        public void OnNotify(string file)
        {
            if (file == "dead") DoShake();
        }
    }
}
