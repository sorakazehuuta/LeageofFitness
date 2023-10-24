
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MusicManager : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("ロビーBGM設定")]
        [Tooltip("ロビー用BGM")] [SerializeField] AudioClip LobbyMusic;
        //---------------------------------------
        [Header("戦闘BGM設定")]
        [Tooltip("戦闘中のBGM")] public AudioClip BattleMusic;
        //---------------------------------------
        [Header("ゲーム終了BGM設定")]
        [Tooltip("ゲーム終了時のBGM")] public AudioClip GameEndMusic;
        [Tooltip("指定秒数が経過したら、ロビーBGMに戻す")] public float GameEndMusicDuration;
        //---------------------------------------
        AudioSource audioSource;
        float maxVolume;
        //---------------------------------------
        bool fading = false;
        float fadeStartTime;
        float fadeDuration;
        //---------------------------------------
        private void Update()
        {
            float newTime = Time.time;
            if (newTime > fadeStartTime + fadeDuration)
            {
                fading = false;
            }

            if (fading)
            {
                audioSource.volume = maxVolume - maxVolume * (newTime - fadeStartTime) / fadeDuration;
            }
        }
        //---------------------------------------
        void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
            maxVolume = audioSource.volume;

            if (LobbyMusic) { PlayLobbyMusic(); }            
        }
        //---------------------------------------
        public void DelayedSecond_PlayBattleMusic()
        {
            if (!BattleMusic) { return; }            

            audioSource.volume = maxVolume;
            audioSource.clip = BattleMusic;
            audioSource.Play();
            fading = false;
            audioSource.loop = true;
        }
        //---------------------------------------
        public void PlayGameEndMusicOneShot()
        {
            if (GameEndMusic)
            {
                fading = false;
                audioSource.loop = false;
                audioSource.volume = maxVolume;
                audioSource.clip = GameEndMusic;
                audioSource.Play();
            }

            SendCustomEventDelayedSeconds("DelayedSecond_PlayLobbyMusic", GameEndMusicDuration);
        }
        //---------------------------------------
        public void DelayedSecond_PlayLobbyMusic()
        {
            PlayLobbyMusic();
        }
        //---------------------------------------
        public void PlayLobbyMusic()
        {
            if (!LobbyMusic) { return; }

            audioSource.volume = maxVolume;
            audioSource.clip = LobbyMusic;
            audioSource.Play();
            fading = false;
            audioSource.loop = true;
        }
        //---------------------------------------
        public void FadeOut(float delay)
        {
            fading = true;
            fadeStartTime = Time.time;
            fadeDuration = delay;
        }
        //---------------------------------------
        public bool AutoBuild()
        {
            //特に処理なし
            return true;
        }
        //---------------------------------------
    }
}

