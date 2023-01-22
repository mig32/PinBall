using UnityEngine;

namespace LazyBalls.Dialogs
{
    public abstract class DialogBase : MonoBehaviour
    {
        public abstract DialogType GetDialogType();
    }
}