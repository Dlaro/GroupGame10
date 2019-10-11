using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GroupGame10.Base;
using Microsoft.Xna.Framework.Input;

namespace GroupGame10.GameSystem
{
 class InputManager : GameComponent, IManager
    {
        private static KeyboardState currentKey;//現在のキーの状態
        private static KeyboardState previousKey;//1フレーム前のキーの状態
        public InputManager(Game game) : base(game)
        {

        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            base.Update(gameTime);
        }
    }
}
