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
    
        private const string MusicKey = "music";
        
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
            _isEnabled = PlayerPrefs.GetInt(MusicKey, _isEnabled ? 1 : 0) > 0;
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
                
                PlayerPrefs.SetInt(MusicKey, _isEnabled ? 1 : 0);
            }
        }
    }
}