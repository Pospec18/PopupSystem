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
        private IPopupAction startEdit;
        private IPopupAction exit;

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
        /// <param name="onExitAction">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEditAction">Action on Start of text edit</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        public void Use(string text, Action<string> onConfirmAction, Action onExitAction, Action onStartEditAction, int maxInputChars = -1, Sprite image = null, bool blockRaycasts = true)
        {
            action = onConfirmAction;
            startEdit = new PopupAction(onStartEditAction, null);
            exit = new PopupAction(onExitAction, null);
            SetGameObject(text, image, blockRaycasts, maxInputChars);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExit">Generic Action on exiting Popup using Exit Button (ingore name)</param>
        /// <param name="onStartEditAction">Action on Start of text edit</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        public void Use<T>(string text, Action<string> onConfirmAction, PopupOption<T> onExit, Action onStartEditAction, int maxInputChars = -1, Sprite image = null, bool blockRaycasts = true)
        {
            action = onConfirmAction;
            startEdit = new PopupAction(onStartEditAction, null);
            exit = new PopupAction<T>(onExit.action, onExit.value, null);
            SetGameObject(text, image, blockRaycasts, maxInputChars);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExitAction">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEdit">Generic Action on Start of text edit (ignore name)</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        public void Use<T>(string text, Action<string> onConfirmAction, Action onExitAction, PopupOption<T> onStartEdit, int maxInputChars = -1, Sprite image = null, bool blockRaycasts = true)
        {
            action = onConfirmAction;
            startEdit = new PopupAction<T>(onStartEdit.action, onStartEdit.value, null);
            exit = new PopupAction(onExitAction, null);
            SetGameObject(text, image, blockRaycasts, maxInputChars);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExit">Generic Action on exiting Popup using Exit Button (ingore name)</param>
        /// <param name="onStartEdit">Generic Action on Start of text edit (ignore name)</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        public void Use<T, Q>(string text, Action<string> onConfirmAction, PopupOption<T> onExit, PopupOption<Q> onStartEdit, int maxInputChars = -1, Sprite image = null, bool blockRaycasts = true)
        {
            action = onConfirmAction;
            startEdit = new PopupAction<Q>(onStartEdit.action, onStartEdit.value, null);
            exit = new PopupAction<T>(onExit.action, onExit.value, null);
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
