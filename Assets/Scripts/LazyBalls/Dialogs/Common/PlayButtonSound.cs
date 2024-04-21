using LazyBalls.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs.Common
{
    [RequireComponent(typeof(Button))]
    public class PlayButtonSound : MonoBehaviour
    {
        [SerializeField] private SoundController.SoundType _sound = SoundController.SoundType.ButtonClick;
        
        private Button _button;
        private Button Button
        {
            get
            {
                if (_button == null)
                {
                    _button = GetComponent<Button>();
                }

                return _button;
            }
        }
        
        private void Awake()
        {
            Button.onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            SoundController.Instance().PlaySound(_sound);
        }
    }
}