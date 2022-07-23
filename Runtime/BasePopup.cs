using UnityEngine;
using TMPro;
using System;

namespace Pospec.Popup
{
    /// <summary>
    /// Abstract class of Popup Object
    /// </summary>
    public abstract class BasePopup : MonoBehaviour
    {
        public TextMeshProUGUI textField;

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Abstract class holding Button with its OnClick Action
        /// </summary>
        protected interface IPopupAction
        {
            /// <summary>
            /// Triggers Action
            /// </summary>
            void OnClick();

            GameObject Button { get; set; }
        }

        protected class PopupAction : IPopupAction
        {
            public Action action;
            public GameObject Button { get; set; }

            public PopupAction(Action _action, GameObject _button)
            {
                action = _action;
                Button = _button;
            }

            public void OnClick()
            {
                action?.Invoke();
            }
        }

        protected class PopupAction<T> : IPopupAction
        {
            public Action<T> action;
            public T value;
            public GameObject Button { get; set; }

            public PopupAction(Action<T> _action, T _value, GameObject _button)
            {
                action = _action;
                value = _value;
                Button = _button;
            }

            public void OnClick()
            {
                action?.Invoke(value);
            }
        }
    }
}
