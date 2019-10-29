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
        private List<UIEntity> _UIEntities;
        private Game game;
        private SpriteBatch spriteBatch;
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private ContentManager contentManager;
        Player player;
        Camera camera2D;

        internal List<BaseEntity> Entities { get => entities; set => entities = value; }
        internal List<List<BaseEntity>> MapList { get => mapList; set => mapList = value; }
        internal List<BackGround> BackGrounds { get => backGrounds; set => backGrounds = value; }
        internal List<UIEntity> UIEntities { get => _UIEntities; set => _UIEntities = value; }

        public RenderManager(Game game) : base(game)
        {
            Entities = new List<BaseEntity>();
            UIEntities = new List<UIEntity>();
            this.game = game;
            contentManager = game.Content;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            camera2D = new Camera(game);
            game.Components.Add(camera2D);
            BackGrounds = new List<BackGround>();
            MapList = new List<List<BaseEntity>>();

        }
        public override void Initialize()
        {

            BackGrounds.Add(new BackGround("bg1", new Vector2(-256, -320), Vector2.Zero));
            BackGrounds.Add(new BackGround("bg2", Vector2.Zero, new Vector2(-1, 0)));
            BackGrounds.Add(new BackGround("bg2", new Vector2(1024, 0), new Vector2(-1, 0)));
            BackGrounds.Add(new BackGround("bg3", new Vector2(0, 640), new Vector2(-2, 0)));
            BackGrounds.Add(new BackGround("bg3", new Vector2(1024, 640), new Vector2(-2, 0)));
            BackGrounds.Add(new BackGround("bg4", new Vector2(0, 0), new Vector2(-2, 0)));
            BackGrounds.Add(new BackGround("bg4", new Vector2(1024, 0), new Vector2(-2, 0)));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            if (!(BackGrounds is null)) BackGrounds.ForEach(bg => bg.Update(gameTime));
            foreach (var list in MapList)
            {
                foreach (var a in list)
                {
                    if (a is Space) continue;
                    a.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(transformMatrix:
                  camera2D.Transform);
            if (!(BackGrounds is null)) BackGrounds.ForEach(bg => DrawTexture(bg.Name, new Vector2(bg.Position.X + camera2D.Position.X - 512, bg.Position.Y)));

            foreach (var list in MapList)
            {
                foreach (var a in list)
                {
                    if (a is Space) continue;
                    DrawTextureWithCamera(a.Name, a.Rectangle);
                }
            }
            if (!(Entities is null))
            {
                foreach (var e in Entities)
                {

                    if (e.IsDeadFlag) continue;
                    DrawTextureWithCamera(e.Name, e.Rectangle);
                }
            }
            if (!(player is null)) spriteBatch.Draw(textures["player"], destinationRectangle: new Rectangle((int)(player.Rectangle.X), player.Rectangle.Y, 64, 64), origin: new Vector2(24, 24), rotation: player.Rotation);
            spriteBatch.End();

            spriteBatch.Begin(blendState: BlendState.AlphaBlend);




            if (UIEntities != null)
            {
                foreach (var ui in UIEntities)
                {
                    if (ui is Score)
                    {
                        DrawNumber(ui.Position, ((Score)ui).Num);
                        continue;
                    }
                        DrawTexture(ui.Name, ui.Position);

                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void Add(BaseEntity entity)
        {
            if (entity is Player) { player = (Player)entity; player.AddObserver(camera2D); camera2D.GetPlayer(player); return; }
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
        public void DrawTextureWithCamera(string assetName, Rectangle rectangle, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、" +
                "画像の読み込み自体できていません");

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
        #region 数字の描画
        /// <summary>
        /// 数字の描画（整数のみ）
        /// </summary>
        /// <param name="assetName">数字画像の名前</param>
        /// <param name="position">位置</param>
        /// <param name="number">表示したい整数値</param>
        /// <param name="alpha">透明値</param>
        public void DrawNumber(
           
            Vector2 position,
            int number,
            float alpha = 1.0f)
        {
         
            //マイナスの数は0
            if (number < 0)
            {
                number = 0;
            }

            int width = 32;//画像横幅

            //数字を文字列化し、1文字ずつ取り出す
            foreach (var n in number.ToString())
            {
                //数字のテクスチャが数字1つにつき幅32高さ64
                //文字と文字を引き算し、整数値を取得している
                spriteBatch.Draw(
                    textures[n.ToString()],
                    position,
                   
                    Color.White);

                //1文字描画したら1桁分右にずらす
                position.X += width;
            }
        }

        /// <summary>
        /// 数字の描画（実数、小数点以下は2桁表示）
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="position"></param>
        /// <param name="number"></param>
        /// <param name="alpha"></param>
        public void DrawNumber(
            string assetName,
            Vector2 position,
            float number,
            float alpha = 1.0f)
        {
            //マイナスは０へ
            if (number < 0.0f)
            {
                number = 0.0f;
            }

            int width = 32;
            //小数部は2桁まで、整数部が1桁の時は0で埋める
            foreach (var n in number.ToString("00.00"))
            {
                //小数の「.」か？
                if (n == '.')
                {
                    spriteBatch.Draw(
                        textures[assetName],
                        position,
                        new Rectangle(10 * width, 0, width, 64),//ピリオドは10番目
                        Color.White * alpha);
                }
                else
                {
                    //数字の描画
                    spriteBatch.Draw(
                        textures[assetName],
                        position,
                        new Rectangle((n - '0') * width, 0, width, 64),
                        Color.White * alpha);
                }

                //1文字描画したら1桁分右にずらす
                position.X += width;
            }
        }
        #endregion 数字の描画

        public void ClearList()
        {

            Entities.Clear();
            UIEntities.Clear();
            MapList = new List<List<BaseEntity>>();
            player = null;
        }


    }
}
