using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Popup
{
    /// <summary>
    /// MonoBehaviour for generating Buttons for actions
    /// </summary>
    public class ButtonInputPopup : BasePopup
    {
        [SerializeField] private PopupButton buttonPref;
        [SerializeField] private Transform buttonsPanel;

        [SerializeField] private List<IPopupAction> popupActions = new List<IPopupAction>();
        private List<KeyCode> confirmKeyCodes = new List<KeyCode>();

        #region Use Popup

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, params PopupButtonOption[] options)
        {
            Use(text, null, BlockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, Sprite image, params PopupButtonOption[] options)
        {
            Use(text, image, BlockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, bool blockRaycasts, params PopupButtonOption[] options)
        {
            Use(text, null, blockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, Sprite image, bool blockRaycasts, params PopupButtonOption[] options)
        {
            SetupPopup(text, image, blockRaycasts);
            for (int i = 0; i < options.Length; i++)
            {
                Generate(options[i], i);
            }
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <typeparam name="T">Parametr Type of PopupOption</typeparam>
        /// <param name="text">Popup text</param>
        /// <param name="options">Options for player to choose (can be of one generic type)</param>
        public void Use<T>(string text, params IPopupButtonOption[] options)
        {
            Use<T>(text, null, BlockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <typeparam name="T">Parametr Type of PopupOption</typeparam>
        /// <param name="text">Popup text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="options">Options for player to choose (can be of one generic type)</param>
        public void Use<T>(string text, Sprite image, params IPopupButtonOption[] options)
        {
            Use<T>(text, image, BlockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <typeparam name="T">Parametr Type of PopupOption</typeparam>
        /// <param name="text">Popup text</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="options">Options for player to choose (can be of one generic type)</param>
        public void Use<T>(string text, bool blockRaycasts, params IPopupButtonOption[] options)
        {
            Use<T>(text, null, blockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <typeparam name="T">Parametr Type of PopupOption</typeparam>
        /// <param name="text">Popup text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="options">Options for player to choose (can be of one generic type)</param>
        public void Use<T>(string text, Sprite image, bool blockRaycasts, params IPopupButtonOption[] options)
        {
            SetupPopup(text, image, blockRaycasts);
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].IsGeneric)
                    Generate((PopupButtonOption<T>)options[i], i);
                else
                    Generate((PopupButtonOption)options[i], i);
            }
        }

        #endregion

        private void Generate<T>(PopupButtonOption<T> option, int i)
        {
            GameObject button = GenerateButton(option, i);
            popupActions.Add(new PopupAction<T>(option, button));
        }

        private void Generate(PopupButtonOption option, int i)
        {
            GameObject button = GenerateButton(option, i);
            popupActions.Add(new PopupAction(option, button));
        }

        private GameObject GenerateButton(IPopupButtonOption option, int i)
        {
            PopupButton button = Instantiate(buttonPref, buttonsPanel);
            button.textField.text = option.Name;
            button.button.onClick.AddListener(delegate { OnButtonClick(i); });
            if (option.Col != null)
                button.textField.color = (Color)option.Col;
            return button.gameObject;
        }

        private void Update()
        {
            if (popupActions == null || popupActions.Count == 0)
                return;

            foreach (KeyCode key in confirmKeyCodes)
                if (Input.GetKeyDown(key))
                    OnButtonClick(0);
        }

        protected override void ResetPopup()
        {
            base.ResetPopup();
            foreach (var item in popupActions)
            {
                Destroy(item.Button);
            }
            popupActions.Clear();
        }

        /// <summary>
        /// OnClick of PopupButton
        /// </summary>
        /// <param name="index">Index in popupActions list</param>
        private void OnButtonClick(int index)
        {
            popupActions[index].OnClick();
            Close();
        }
    }    
}
