using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pospec.Popup
{
    /// <summary>
    /// MonoBehaviour for generating Buttons for actions
    /// </summary>
    public class ButtonInputPopup : BasePopup
    {
        [SerializeField] private PopupButton buttonPref;
        [SerializeField] private Transform buttonsPanel;
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
                blockRaycastsIMG.enabled = value;
                _blockRaycasts = value;
            }
        }

        [SerializeField] private List<IPopupAction> popupActions = new List<IPopupAction>();
        private List<KeyCode> confirmKeyCodes = new List<KeyCode>();

        #region Use Popup

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, params PopupOption[] options)
        {
            Use(text, null, _blockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, Image image, params PopupOption[] options)
        {
            Use(text, image, _blockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <param name="text">Popup text</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="options">Options for player to choose</param>
        public void Use(string text, bool blockRaycasts, params PopupOption[] options)
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
        public void Use(string text, Image image, bool blockRaycasts, params PopupOption[] options)
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
        public void Use<T>(string text, params IPopupOption[] options)
        {
            Use<T>(text, null, _blockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <typeparam name="T">Parametr Type of PopupOption</typeparam>
        /// <param name="text">Popup text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="options">Options for player to choose (can be of one generic type)</param>
        public void Use<T>(string text, Image image, params IPopupOption[] options)
        {
            Use<T>(text, image, _blockRaycasts, options);
        }

        /// <summary>
        /// Show and set Popup
        /// </summary>
        /// <typeparam name="T">Parametr Type of PopupOption</typeparam>
        /// <param name="text">Popup text</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="options">Options for player to choose (can be of one generic type)</param>
        public void Use<T>(string text, bool blockRaycasts, params IPopupOption[] options)
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
        public void Use<T>(string text, Image image, bool blockRaycasts, params IPopupOption[] options)
        {
            SetupPopup(text, image, blockRaycasts);
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].IsGeneric)
                    Generate((PopupOption<T>)options[i], i);
                else
                    Generate((PopupOption)options[i], i);
            }
        }

        #endregion

        private void SetupPopup(string text, Image image, bool blockRaycasts)
        {
            ResetPopup();
            gameObject.SetActive(true);
            textField.text = text;
        }

        void Generate<T>(PopupOption<T> option, int i)
        {
            GameObject button = GenerateButton(option, i);
            popupActions.Add(new PopupAction<T>(option.action, option.value, button));
        }

        void Generate(PopupOption option, int i)
        {
            GameObject button = GenerateButton(option, i);
            popupActions.Add(new PopupAction(option.action, button));
        }

        GameObject GenerateButton(IPopupOption option, int i)
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

        void ResetPopup()
        {
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
            Close();
            popupActions[index].OnClick();
        }
    }    
}
