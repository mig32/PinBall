using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace LazyBalls.Ads
{
    public class InterstitialAd : AdProviderBase, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private enum LoadStatus
        {
            NotLoaded,
            InProgress,
            Loaded,
            Failed
        }
        
        [SerializeField] private string androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string iOsAdUnitId = "Interstitial_iOS";
        private string _adUnitId;
        private LoadStatus _loadStatus = LoadStatus.NotLoaded;
        
        private void Awake()
        {
            // Get the Ad Unit ID for the current platform:
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOsAdUnitId
                : androidAdUnitId;
        }

        public override async Task<bool> LoadAndShowAd()
        {
#if UNITY_IOS || UNITY_ANDROID
            if (_loadStatus is LoadStatus.NotLoaded or LoadStatus.Failed)
            {
                if (!AdsInitializer.IsAdsInitialized)
                {
                    UnityEngine.Debug.Log($"Error loading Ad Unit {_adUnitId}: Ads not initialized");
                    _loadStatus = LoadStatus.Failed;
                    return false;
                }
                
                LoadAd();
            }
            
            while (_loadStatus == LoadStatus.InProgress)
            {
                await Task.Yield();
            }
            
            if (_loadStatus != LoadStatus.Loaded)
            {
                return false;
            }

            ShowAd();

#endif
            return true;
        }
        
        // Load content to the Ad Unit:
        private void LoadAd()
        {
            _loadStatus = LoadStatus.InProgress;
            UnityEngine.Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
 
        // Show the loaded content in the Ad Unit:
        private void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            UnityEngine.Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }
 
        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            _loadStatus = LoadStatus.Loaded;
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            _loadStatus = LoadStatus.Failed;
            UnityEngine.Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        }
 
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            UnityEngine.Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }
 
        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
    }
}
