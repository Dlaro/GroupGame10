using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
using GroupGame10.GameSystem;
using Microsoft.Xna.Framework;

namespace GroupGame10
{
    class GamePlay : BaseScene
    {

        Player player;

        Game game;
        public GamePlay(Game game, string name)
        {
            Name = name;
            this.game = game;

        }

        public override void Inilized()
        {
            IsEndFlag = false;
            player = new Player((SoundManager)game.Components.First(b=>b is SoundManager));



        }

        public override void Update(GameTime gameTime)
        {

            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        }
        public override void Draw(RenderManager renderManager)
        {

            renderManager.Add(player);
            
        }
        public override void Physics(PhysicsManager physicsManager)
        {
            physicsManager.Add(player);

        }
    }
}

