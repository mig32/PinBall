using UnityEngine;

namespace LazyBalls.Singletons
{
    public static class LocalStorage
    {
        private const string MusicKey = "music";
        private const string SoundKey = "sound";
        private const string SelectedLanguageKey = "lang";
        private const string MaxScoreKey = "maxScore";
        private const string PrevScoreKey = "prevScore";

        public static bool IsMusicEnabled 
        { 
            get => PlayerPrefs.GetInt(MusicKey, 1) > 0;
            set => PlayerPrefs.SetInt(MusicKey, value ? 1 : 0);
        }

        public static bool IsSoundEnabled
        {
            get => PlayerPrefs.GetInt(SoundKey, 1) > 0;
            set => PlayerPrefs.SetInt(SoundKey, value ? 1 : 0);
        }

        public static int SelectedLanguage
        {
            get => PlayerPrefs.GetInt(SelectedLanguageKey, -1);
            set => PlayerPrefs.SetInt(SelectedLanguageKey, value);
        } 

        public static int MaxScore
        {
            get => PlayerPrefs.GetInt(MaxScoreKey, 0);
            set => PlayerPrefs.SetInt(MaxScoreKey, value);
        }  

        public static int PrevScore
        {
            get => PlayerPrefs.GetInt(PrevScoreKey, 0);
            set => PlayerPrefs.SetInt(PrevScoreKey, value);
        } 
    }
}
