using System.Collections.Generic;
using LazyBalls.Ads;
using UnityEngine;

namespace LazyBalls.Singletons
{
    public class AdsController : MonoBehaviour
    {
        private Dictionary<AdType, AdProviderBase> _createdProviders = new ();

        private static AdsController _instance;
        public static AdsController Instance() => _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "AdsController must be single");
                return;
            }

            _instance = this;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public void ShowAd(AdType type)
        {
            if (_createdProviders.ContainsKey(type))
            {
                _createdProviders[type].LoadAndShowAd();
                return;
            }

            var providerType = AdHelper.GetComponentTypeByAdType(type);
            var provider = (AdProviderBase)gameObject.AddComponent(providerType);
            _createdProviders[type] = provider;
            provider.LoadAndShowAd();
        }
    }
}