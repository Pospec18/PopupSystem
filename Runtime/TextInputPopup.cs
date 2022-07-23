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
        public TMP_InputField inputField;
        public Button exitButton;
        public Button confirmButton;
        Action<string> action;
        IPopupAction startEdit;
        IPopupAction exit;

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

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExitAction">Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEditAction">Action on Start of text edit</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        public void Use(string text, Action<string> onConfirmAction, Action onExitAction, Action onStartEditAction, int maxInputChars = -1)
        {
            action = onConfirmAction;
            startEdit = new PopupAction(onStartEditAction, null);
            exit = new PopupAction(onExitAction, null);
            SetGameObject(text, maxInputChars);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExitAction">Generic Action on exiting Popup using Exit Button</param>
        /// <param name="onExitValue">Value of parameter of onExitAction</param>
        /// <param name="onStartEditAction">Action on Start of text edit</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        public void Use<T>(string text, Action<string> onConfirmAction, Action<T> onExitAction, T onExitValue, Action onStartEditAction, int maxInputChars = -1)
        {
            action = onConfirmAction;
            startEdit = new PopupAction(onStartEditAction, null);
            exit = new PopupAction<T>(onExitAction, onExitValue, null);
            SetGameObject(text, maxInputChars);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExitAction">Generic Action on exiting Popup using Exit Button</param>
        /// <param name="onStartEditAction">Action on Start of text edit</param>
        /// <param name="onStartValue">Value of parameter of onStartEditAction</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        public void Use<T>(string text, Action<string> onConfirmAction, Action onExitAction, Action<T> onStartEditAction, T onStartValue, int maxInputChars = -1)
        {
            action = onConfirmAction;
            startEdit = new PopupAction<T>(onStartEditAction, onStartValue, null);
            exit = new PopupAction(onExitAction, null);
            SetGameObject(text, maxInputChars);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="onConfirmAction">Action on End of text edit</param>
        /// <param name="onExitAction">Generic Action on exiting Popup using Exit Button</param>
        /// <param name="onExitValue">Value of parameter of onExitAction</param>
        /// <param name="onStartEditAction">Action on Start of text edit</param>
        /// <param name="onStartValue">Value of parameter of onStartEditAction</param>
        /// <param name="maxInputChars">limit of input characters (negative values for no limit)</param>
        public void Use<T, Q>(string text, Action<string> onConfirmAction, Action<T> onExitAction, T onExitValue, Action<Q> onStartEditAction, Q onStartValue, int maxInputChars = -1)
        {
            action = onConfirmAction;
            startEdit = new PopupAction<Q>(onStartEditAction, onStartValue, null);
            exit = new PopupAction<T>(onExitAction, onExitValue, null);
            gameObject.SetActive(true);
            SetGameObject(text, maxInputChars);
        }

        private void SetGameObject(string text, int maxInputChars)
        {
            gameObject.SetActive(true);
            textField.text = text;
            inputField.characterLimit = maxInputChars;
        }

        private void Confirm(string text)
        {
            action?.Invoke(text);
            ResetPopup();
            gameObject.SetActive(false);
        }

        private void ExitPopup()
        {
            exit.OnClick();
            ResetPopup();
            gameObject.SetActive(false);
        }

        private void ResetPopup()
        {
            action = null;
            startEdit = null;
            exit = null;
        }
    }
}
