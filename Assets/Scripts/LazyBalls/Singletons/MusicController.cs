using LazyBalls.Configs;
using UnityEngine;

namespace LazyBalls.Singletons
{
    public class MusicController : MonoBehaviour
    {
        public enum MusicType
        {
            Menu,
            Game
        }
        
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private MusicConfig musicConfig;
        
        private static MusicController _instance;
        public static MusicController Instance() => _instance;
        
        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "MusicController must be single");
                return;
            }

            _instance = this;
            _isEnabled = LocalStorage.IsMusicEnabled;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        
        public void PlayMusic(MusicType type)
        {
            var audioClip = musicConfig.GetMusicClip(type);
            if (audioClip == null)
            {
                UnityEngine.Debug.Log($"MusicController -> no music for type {type}");
                musicSource.Stop();
                return;
            }

            if (musicSource.isPlaying && musicSource.clip == audioClip)
            {
                return;
            }
            
            musicSource.clip = audioClip;
            if (!IsEnabled)
            {
                return;
            }
            
            musicSource.Play();
        }

        private bool _isEnabled;
        public bool IsEnabled 
        { 
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                if (_isEnabled)
                {
                    musicSource.Play();
                }
                else
                {
                    musicSource.Stop();
                }
                
                LocalStorage.IsMusicEnabled = _isEnabled;
            }
        }
    }
}