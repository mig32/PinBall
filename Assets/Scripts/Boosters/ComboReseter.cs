using UnityEngine;

namespace LazyBalls.Boosters
{
    public class ComboReseter : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }
            
            BoosterBase.ResetCombo();
        }
    }
}