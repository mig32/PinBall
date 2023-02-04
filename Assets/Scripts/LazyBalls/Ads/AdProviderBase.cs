using System.Threading.Tasks;
using UnityEngine;

namespace LazyBalls.Ads
{
    public abstract class AdProviderBase: MonoBehaviour
    {
        public abstract Task<bool> LoadAndShowAd();
    }
}