﻿// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GroupGame10.GameSystem;
using GroupGame10.Base;
/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace GroupGame10
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
      
        
        private RenderManager renderManager;
        private MapManager mapManager;
        private ScenceManager scenceManager;
        private PhysicsManager spriteManager;
        private SoundManager soundManager;
        private UIManager uIManager;

        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
            
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述

            renderManager = new RenderManager(this);
            Components.Add(renderManager);

            uIManager = new UIManager(this);
            Components.Add(uIManager);

            mapManager = new MapManager(this);
            Components.Add(mapManager);

            spriteManager = new PhysicsManager(this);
            Components.Add(spriteManager); 
            
            scenceManager = new ScenceManager(this);
            Components.Add(scenceManager);

            soundManager = new SoundManager(this);
            Components.Add(soundManager);

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成


            // この下にロジックを記述
            foreach (var map in Setting.MapLoad)
            {
                mapManager.Load(map);
            }
            foreach(var texture in Setting.TexturesLoad)
            {
                renderManager.LoadContent(texture);
            }
            foreach (var song in Setting.BGMLoad)
            {
                soundManager.LoadBGM(song);
            }
            foreach (var se in Setting.SELoad)
            {
                soundManager.LoadSE(se);
            }

            soundManager.PlayBGM("gamebgm");
            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }

            // この下に更新ロジックを記述
            Input.Update(gameTime);
            if (Input.IsKeyDown(Keys.Enter))
            {
                scenceManager.Enabled = true;
                scenceManager.Initialize();
            }
            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述


            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
