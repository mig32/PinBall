using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs.Common
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class ToggleButtonImage : MonoBehaviour
    {
        [SerializeField] private Sprite _imageOn;
        [SerializeField] private Sprite _imageOff;
        
        private bool _isEnabled;
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

        public Button.ButtonClickedEvent OnClick => _button.onClick;

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = !isEnabled;
            ToggleButton();
        }
        
        private void Awake()
        {
            Button.onClick.AddListener(ToggleButton);
        }

        private void ToggleButton()
        {
            _isEnabled = !_isEnabled;
            var image = GetComponent<Image>();
            image.sprite = _isEnabled ? _imageOn : _imageOff;
            var color = image.color;
            color.a = _isEnabled ? 1 : 0.5f;
            image.color = color;
        }
    }
}