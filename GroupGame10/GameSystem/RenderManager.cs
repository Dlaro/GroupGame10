using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using GroupGame10.Base;

namespace GroupGame10.GameSystem
{
    class RenderManager : DrawableGameComponent, IManager
    {
        private List<BaseEntity> entities;
        private List<List<BaseEntity>> mapList;
        private List<BackGround> backGrounds;
        private Game game;
        private SpriteBatch spriteBatch;
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private ContentManager contentManager;
        private Vector2 camera;
        Player player;

        internal List<BaseEntity> Entities { get => entities; set => entities = value; }
        internal List<List<BaseEntity>> MapList { get => mapList; set => mapList = value; }
        internal List<BackGround> BackGrounds { get => backGrounds; set => backGrounds = value; }

        public RenderManager(Game game) : base(game)
        {
            Entities = new List<BaseEntity>();
            this.game = game;
            contentManager = game.Content;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            camera = new Vector2();
            BackGrounds = new List<BackGround>();
            MapList = new List<List<BaseEntity>>();

        }
        public override void Initialize()
        {
           

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
           
            if (player is null) return;
            camera = player.Position - new Vector2(256, 0);
           


            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
           
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
          if(!(BackGrounds is null))  BackGrounds.ForEach(bg => DrawTexture(bg.Name, bg.Position));
            foreach (var list in MapList)
            {
                foreach (var a in list)
                {
                    DrawTexture(a.Name, a.Rectangle);
                }
            }
            if (!(Entities is null))
            {
                foreach (var e in Entities)
                {
                    if (e.IsDeadFlag) return;
                    DrawTexture(e.Name, e.Rectangle);
                }
            }
          if(!(player is null))  spriteBatch.Draw(textures["player"], destinationRectangle: new Rectangle((int)(player.Rectangle.X-camera.X+32), player.Rectangle.Y+32, player.Rectangle.Width, player.Rectangle.Height), origin: new Vector2(24, 24), rotation: player.Rotation);
          // DrawTexture("block", player.Rectangle);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void Add(BaseEntity entity)
        {
            if (entity is Player) { player = (Player)entity; return; }
            Entities.Add(entity);
        }
        public void LoadContent(string assetName, string filepath = "./")
        {
            //すでにキー（assetName：アセット名）が登録されているとき
            if (textures.ContainsKey(assetName))
            {
#if DEBUG //DEBUGモードの時のみ下記エラー分をコンソールへ表示
                Console.WriteLine(assetName + "はすでに読み込まれています。\n プログラムを確認してください。");
#endif

                //それ以上読み込まないのでここで終了
                return;
            }
            //画像の読み込みとDictionaryへアセット名と画像を登録
            textures.Add(assetName, contentManager.Load<Texture2D>(filepath + assetName));

        }

        #region 画像の描画関連
        /// <summary>
        /// 画像の描画（画像サイズはそのまま）
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="alpha">透明値（1.0f：不透明 0.0f：透明）</param>
        public void DrawTexture(string assetName, Vector2 position, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、" +
                "画像の読み込み自体できていません");

            spriteBatch.Draw(textures[assetName], position, Color.White * alpha);
        }
        public void DrawTexture(string assetName, Rectangle rectangle, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、" +
                "画像の読み込み自体できていません");
            rectangle.X = rectangle.X - (int)camera.X;
            spriteBatch.Draw(textures[assetName], rectangle, Color.White * alpha);
        }
        /// <summary>
        /// 画像の描画（画像を指定範囲内だけ描画）
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">指定範囲</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string assetName, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、" +
                "画像の読み込み自体できていません");

            spriteBatch.Draw(
                textures[assetName], //テクスチャ
                position,            //位置
                rect,                //指定範囲（矩形で指定：左上の座標、幅、高さ）
                Color.White * alpha);//透明値

        }


        /// <summary>
        /// 画像の描画
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="positoin">位置</param>
        /// <param name="rect">切り出し範囲</param>
        /// <param name="rotate">回転角度</param>
        /// <param name="rotatePosition">回転軸位置</param>
        /// <param name="scale">拡大縮小</param>
        /// <param name="effects">表示反転効果</param>
        /// <param name="depth">スプライト深度</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(
            string assetName,
            Vector2 positoin,
            Rectangle? rect, //nullを受け入れられるよう「？」で
            float rotate,
            Vector2 rotatePosition,
            Vector2 scale,
            SpriteEffects effects = SpriteEffects.None,
            float depth = 0.0f,
            float alpha = 1.0f)
        {
            spriteBatch.Draw(
                textures[assetName],//テクスチャ
                positoin,           //位置
                rect,               //切り取り範囲
                Color.White * alpha,//透明値
                rotate,             //回転角度
                rotatePosition,     //回転軸
                scale,              //拡大縮小
                effects,            //表示反転効果
                depth               //スプライト深度
                );
        }


        /// <summary>
        /// 画像の描画
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="color">色（通常はColor.White）</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string assetName, Vector2 position, Color color, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、" +
                "画像の読み込み自体できていません");

            spriteBatch.Draw(textures[assetName], position, color * alpha);
        }
        #endregion  画像の描画
        public void ClearList()
        {
            BackGrounds.Clear();
            
            Entities.Clear();

            MapList= new List<List<BaseEntity>>();
            player = null;
        }
     

    }
}
