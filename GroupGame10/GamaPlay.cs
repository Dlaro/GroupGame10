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
        Score score;
        Game game;
        public GamePlay(Game game, string name)
        {
            Name = name;
            this.game = game;

        }

        public override void Inilized()
        {
            score = new Score(game);
            IsEndFlag = false;
            player = new Player();
            player.AddObserver((IObserver)game.Components.First(com=>com is SoundManager));
            player.AddObserver(score);


        }

        public override void Update(GameTime gameTime)
        {
            score.Update(gameTime);
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F)) IsEndFlag = true;
        }
        public override void Draw(RenderManager renderManager)
        {

            renderManager.Add(player);
            renderManager.BackGrounds.Add(new BackGround("bg1", new Vector2(1024, 0), new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg1", Vector2.Zero, new Vector2(-2, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", Vector2.Zero, new Vector2(-1, 0)));
            renderManager.BackGrounds.Add(new BackGround("bg2", new Vector2(1024, 0), new Vector2(-1, 0)));
        }
        public override void Physics(PhysicsManager physicsManager)
        {
            physicsManager.Add(player);

        }
    }
}

