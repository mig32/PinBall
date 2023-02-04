using LazyBalls.Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class CreditsDialog : DialogBase
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private TMP_Text creditsText;

        protected override void Start()
        {
            base.Start();
            
            exitButton.onClick.AddListener(ExitToMenu);
        }

        private void ExitToMenu()
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.ButtonClick);
            Destroy(gameObject);
        }

        public override DialogType GetDialogType() => DialogType.Credits;
    }
}