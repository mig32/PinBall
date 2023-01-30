using System;
using System.Collections.Generic;
using System.Linq;
using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.Configs
{
    [Serializable]
    public struct SoundInfo
    {
        public SoundController.SoundType type;
        public AudioClip[] sounds;
        public bool isLooped;
    }
    
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Sound Config", order = 51)]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField] private List<SoundInfo> soundList;

        private void OnValidate()
        {
            if (soundList == null)
            {
                soundList = new List<SoundInfo>();
            }
            
            var enumTypes = Enum.GetValues(typeof(SoundController.SoundType));
            foreach (var typeValue in enumTypes)
            {
                var type = (SoundController.SoundType)typeValue;
                if (soundList.Any(it => it.type == type))
                {
                    var soundInfo = soundList.FirstOrDefault(it => it.type == type);
                    if (soundInfo.sounds == null || 
                        soundInfo.sounds.Length == 0 || 
                        soundInfo.sounds.All(it => it == null))
                    {
                        UnityEngine.Debug.LogError($"SoundConfig -> Sound for type {type} is missing");
                    }
                    continue;
                }
                
                soundList.Add(new SoundInfo{type = type});
                UnityEngine.Debug.LogError($"SoundConfig -> Sound for type {type} is missing");
            }
        }
        
        public SoundInfo GetSoundInfo(SoundController.SoundType type) =>
            soundList.FirstOrDefault(it => it.type == type);

    }
}