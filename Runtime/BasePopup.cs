using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pospec.Popup
{
    /// <summary>
    /// Abstract class of Popup Object
    /// </summary>
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private Image popupIMG;
        [SerializeField] private Image blockRaycastsIMG;

        private bool _blockRaycasts;
        public bool BlockRaycasts
        {
            get
            {
                return _blockRaycasts;
            }
            set
            {
                if (blockRaycastsIMG != null)
                    blockRaycastsIMG.enabled = value;
                _blockRaycasts = value;
            }
        }

        public virtual void Close()
        {
            ResetPopup();
            gameObject.SetActive(false);
        }

        protected void SetupPopup(string text, Sprite image, bool blockRaycasts)
        {
            gameObject.SetActive(true);
            textField.text = text;
            BlockRaycasts = blockRaycasts;
            if (popupIMG != null)
                popupIMG.sprite = image;
        }

        protected virtual void ResetPopup()
        {

        }

        #region Popup Actions

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

        #endregion
    }
}
