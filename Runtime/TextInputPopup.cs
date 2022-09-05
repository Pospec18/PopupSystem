using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pospec.Popup
{
    /// <summary>
    /// MonoBehaviour triggering Action with parametr of TextInputField text
    /// </summary>
    public class TextInputPopup : BasePopup
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button confirmButton;
        private Action<string> action;
        private PopupAction startEdit;
        private PopupAction exit;

        private void Start()
        {
            confirmButton?.onClick.AddListener(delegate { Confirm(inputField.text); });
            inputField.onSelect.AddListener(delegate { startEdit.OnClick(); });
            exitButton.onClick.AddListener(ExitPopup);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                Confirm(inputField.text);
        }

        #region Use Popup

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExit">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEdit">Action on Start of text edit</param>
        public void Use(string text, Action<string> onConfirmAction, Action onExit, Action onStartEdit)
        {
            Use(text, onConfirmAction, onExit, onStartEdit, -1, null, BlockRaycasts);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExit">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEdit">Action on Start of text edit</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        public void Use(string text, Action<string> onConfirmAction, Action onExit, Action onStartEdit, int maxInputChars)
        {
            Use(text, onConfirmAction, onExit, onStartEdit, maxInputChars, null, BlockRaycasts);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExit">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEdit">Action on Start of text edit</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        public void Use(string text, Action<string> onConfirmAction, Action onExit, Action onStartEdit, Sprite image, bool blockRaycasts = true)
        {
            Use(text, onConfirmAction, onExit, onStartEdit, -1, image, blockRaycasts);

        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExit">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEdit">Action on Start of text edit</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        public void Use(string text, Action<string> onConfirmAction, Action onExit, Action onStartEdit, int maxInputChars, Sprite image, bool blockRaycasts = true)
        {
            action = onConfirmAction;
            startEdit = new PopupAction(onStartEdit, null);
            exit = new PopupAction(onExit, null);
            SetGameObject(text, image, blockRaycasts, maxInputChars);
        }

        #endregion

        private void SetGameObject(string text, Sprite image, bool blockRaycasts, int maxInputChars)
        {
            SetupPopup(text, image, blockRaycasts);
            inputField.characterLimit = maxInputChars;
        }

        private void Confirm(string text)
        {
            action?.Invoke(text);
            Close();
        }

        private void ExitPopup()
        {
            exit.OnClick();
            Close();
        }

        protected override void ResetPopup()
        {
            base.ResetPopup();
            action = null;
            startEdit = null;
            exit = null;
        }
    }
}
