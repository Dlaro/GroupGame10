using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupGame10.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace GroupGame10.GameSystem
{
   class SoundManager : GameComponent, IManager,IObserver
    {
        Dictionary<string, SoundEffect> SEs;
        Dictionary<string, Song> BGMs;
        string currentBGM;
        ContentManager contentManager;
        public SoundManager(Game game) : base(game)
        {
            contentManager = game.Content;
            SEs = new Dictionary<string, SoundEffect>();
            BGMs = new Dictionary<string, Song>();
        }
        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

        public void Add(BaseEntity entity)
        {

        }



        public void ClearList()
        {
            Initialize();
  
        }

        #region BGM(MP3:MediaPlayer)関連

        /// <summary>
        /// BGM（MP3）の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filepath">ファイルパス</param>
        public void LoadBGM(string name, string filepath = "./")
        {
            //すでに登録されているか？
            if (BGMs.ContainsKey(name))
            {
                return;
            }
            //MP3の読み込みとDictionaryへ登録
            BGMs.Add(name, contentManager.Load<Song>(filepath + name));
        }

        /// <summary>
        /// BGMが停止中か？
        /// </summary>
        /// <returns>停止中ならtrue</returns>
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        /// <summary>
        /// BGMが再生中か？
        /// </summary>
        /// <returns>再生中ならtrue</returns>
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        /// <summary>
        /// BGMが一時停止中か？
        /// </summary>
        /// <returns>一時停止中ならtrue</returns>
        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        /// <summary>
        /// BGMを停止
        /// </summary>
        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        /// <summary>
        /// BGM再生
        /// </summary>
        /// <param name="name"></param>
        public void PlayBGM(string name)
        {
            //アセット名がディクショナリに登録されているか？
            Debug.Assert(BGMs.ContainsKey(name),name);

            //同じ曲か？
            if (currentBGM == name)
            {
                //同じ曲だったら何もしない
                return;
            }

            //BGMは再生中か？
            if (IsPlayingBGM())
            {
                //再生中なら、停止処理
                StopBGM();
            }

            //ボリューム設定（BGMはSEに比べて音量半分が普通）
            MediaPlayer.Volume = 0.5f;

            //現在のBGM名を設定
            currentBGM = name;

            //再生開始
            MediaPlayer.Play(BGMs[currentBGM]);
        }

        /// <summary>
        /// BGMの一時停止
        /// </summary>
        public void PauseBGM()
        {
            if (IsPlayingBGM())
            {
                MediaPlayer.Pause();
            }
        }

        /// <summary>
        /// 一時停止からの再生
        /// </summary>
        public void ResumeBGM()
        {
            if (IsPausedBGM())
            {
                MediaPlayer.Resume();
            }
        }


        /// <summary>
        /// BGMループフラグを変更
        /// </summary>
        /// <param name="loopFlag"></param>
        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }
        #endregion BGM(MP3:MediaPlayer)関連



        #region WAV(SE:SoundEffect)関連

        public void LoadSE(string name, string filepath = "./")
        {
            //すでに登録されていれば何もしない
            if (SEs.ContainsKey(name))
            {
                return;
            }
            SEs.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void PlaySE(string name)
        {
            //アセット名が登録されているか？
            Debug.Assert(SEs.ContainsKey(name),name);

            //再生
            SEs[name].Play();

        }
        public void PlaySE(string name, float volume, float pitch, float pan)
        {
            //アセット名が登録されているか？
            Debug.Assert(SEs.ContainsKey(name), name);

            //再生
            SEs[name].Play(volume, pitch, pan);

        }
        #endregion //WAV(SE:SoundEffect)関連
        public void OnNotify(string file)
        {
            switch (file)
            {
                case "IntoWtaer":
                    PlaySE("water");
                    break;
                case "GetCoin":
                    PlaySE("coin");
                    break;
                default:
                    break;
            }
        }
        
    }
}

