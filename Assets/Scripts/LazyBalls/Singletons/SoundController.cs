using System.Collections.Generic;
using System.Linq;
using LazyBalls.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LazyBalls.Singletons
{
    public class SoundController : MonoBehaviour
    {
        public enum SoundType
        {
            RoundBooster,
            DirectionalBooster,
            PortalEnter,
            PortalExit,
            ButtonClick,
            DialogShow,
            DialogHide,
            LauncherCharge,
            LauncherRelease,
            WingFlapUp,
            WingFlapDown,
            BallHit,
            BallDestroyed,
            BallAppear
        }
        
        [SerializeField] private SoundConfig soundConfig;

        private readonly Dictionary<int, AudioSource> _audioSourcePool = new ();
        
        private static SoundController _instance;
        public static SoundController Instance() => _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "SoundController must be single");
                return;
            }

            _instance = this;
            _isEnabled = LocalStorage.IsSoundEnabled;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        
        public int PlaySound(SoundType type)
        {
            if (!IsEnabled)
            {
                return -1;
            }

            var audioSource = GetAudioSource();
            var soundInfo = soundConfig.GetSoundInfo(type);
            if (soundInfo.sounds == null || soundInfo.sounds.Length == 0)
            {
                UnityEngine.Debug.Log($"SoundController -> no sound for type {type}");
                return -1;
            }

            var audioClip = soundInfo.sounds[Random.Range(0, soundInfo.sounds.Length)];
            audioSource.clip = audioClip;
            audioSource.loop = soundInfo.isLooped;
            audioSource.Play();
            return audioSource.GetInstanceID();
        }

        public void StopSound(int instanceId)
        {
            if (!_audioSourcePool.ContainsKey(instanceId))
            {
                return;
            }

            var audioSource = _audioSourcePool[instanceId];
            if (!audioSource.isPlaying)
            {
                return;
            }
            
            audioSource.Stop();
        }
        
        private bool _isEnabled;
        public bool IsEnabled 
        { 
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                LocalStorage.IsSoundEnabled = _isEnabled;
            }
        }

        private AudioSource GetAudioSource()
        {
            var audioSource = _audioSourcePool.FirstOrDefault(it => !it.Value.isPlaying).Value;
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                _audioSourcePool.Add(audioSource.GetInstanceID(), audioSource);
            }
            
            return audioSource;
        }
    }
}