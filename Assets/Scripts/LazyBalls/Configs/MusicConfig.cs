using System;
using System.Collections.Generic;
using System.Linq;
using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.Configs
{
    [CreateAssetMenu(fileName = "MusicConfig", menuName = "Music Config", order = 52)]
    public class MusicConfig : ScriptableObject
    {
        [Serializable]
        public struct MusicInfo
        {
            public MusicController.MusicType type;
            public AudioClip music;
        }
        
        [SerializeField] private List<MusicInfo> musicList;

        private void OnValidate()
        {
            if (musicList == null)
            {
                musicList = new List<MusicInfo>();
            }
            
            var enumTypes = Enum.GetValues(typeof(MusicController.MusicType));
            foreach (var typeValue in enumTypes)
            {
                var type = (MusicController.MusicType)typeValue;
                if (musicList.Any(it => it.type == type))
                {
                    var musicInfo = musicList.FirstOrDefault(it => it.type == type);
                    if (musicInfo.music == null)
                    {
                        UnityEngine.Debug.LogWarning($"MusicConfig -> Music for type {type} is missing");
                    }
                    continue;
                }
                
                musicList.Add(new MusicInfo{type = (MusicController.MusicType)type});
                UnityEngine.Debug.LogWarning($"MusicConfig -> Music for type {type} is missing");
            }
        }

        public AudioClip GetMusicClip(MusicController.MusicType type) =>
            musicList.FirstOrDefault(it => it.type == type).music;

    }
}