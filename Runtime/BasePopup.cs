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

        private bool _blockRaycasts = true;
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

        protected class PopupAction
        {
            public Action Callback { get; set; }
            public Button Button { get; set; }

            public PopupAction(Action action, Button button)
            {
                Callback = action;
                Button = button;
            }

            public PopupAction(PopupButtonOption option, Button button)
            {
                if (option == null)
                    Callback = null;
                else
                    Callback = option.Callback;

                Button = button;
            }

            /// <summary>
            /// Triggers Action
            /// </summary>
            public void OnClick()
            {
                Callback?.Invoke();
            }
        }
    }
}
