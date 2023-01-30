using UnityEngine;

namespace LazyBalls.GameField.Boosters
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