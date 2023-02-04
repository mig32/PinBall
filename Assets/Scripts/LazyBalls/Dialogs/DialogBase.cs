using System;
using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.Dialogs
{
    public abstract class DialogBase : MonoBehaviour
    {
        public abstract DialogType GetDialogType();
        public event Action OnClose;
        
        protected virtual void Start()
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.DialogShow);
        }

        protected virtual void OnDestroy()
        {
            if (SoundController.Instance() != null)
            {
                SoundController.Instance().PlaySound(SoundController.SoundType.DialogHide);
            }
            
            OnClose?.Invoke();
        }
    }
}