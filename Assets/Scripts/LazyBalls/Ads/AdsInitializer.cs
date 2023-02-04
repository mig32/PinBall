using UnityEngine;
using UnityEngine.Advertisements;

namespace LazyBalls.Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string androidGameId;
        [SerializeField] private string iOSGameId;
        [SerializeField] private bool testMode = true;
        private string _gameId;
        public static bool IsAdsInitialized { get; private set; }
 
        private void Awake()
        {
#if UNITY_IOS || UNITY_ANDROID
            InitializeAds();
#endif
        }
 
        private void InitializeAds()
        {
            if (IsAdsInitialized)
            {
                return;
            }
            
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSGameId
                : androidGameId;
            Advertisement.Initialize(_gameId, testMode, this);
        }
 
        public void OnInitializationComplete()
        {
            IsAdsInitialized = true;
            UnityEngine.Debug.Log("Unity Ads initialization complete.");
        }
 
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            UnityEngine.Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
