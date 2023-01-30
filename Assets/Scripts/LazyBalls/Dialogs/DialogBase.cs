using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.Dialogs
{
    public abstract class DialogBase : MonoBehaviour
    {
        public abstract DialogType GetDialogType();

        protected virtual void Start()
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.DialogShow);
        }

        protected virtual void OnDestroy()
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.DialogHide);
        }
    }
}