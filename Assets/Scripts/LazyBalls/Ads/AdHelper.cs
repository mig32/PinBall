using System;

namespace LazyBalls.Ads
{
    public static class AdHelper
    {
        public static Type GetComponentTypeByAdType(AdType type) =>
            type switch
            {
                AdType.Basic => typeof(InterstitialAd),
                _ => throw new NotImplementedException($"Ad provider for type({type} not implemented)")
            };
    }
}