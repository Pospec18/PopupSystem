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

        [SerializeField] private List<PopupAction> popupActions = new List<PopupAction>();
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

        #endregion

        private void Generate(PopupButtonOption option, int i)
        {
            PopupButton popupButton = Instantiate(buttonPref, buttonsPanel);
            popupButton.textField.text = option.Name;
            popupButton.button.onClick.AddListener(delegate { OnButtonClick(i); });
            if (option.Col != null)
                popupButton.textField.color = (Color)option.Col;
            popupActions.Add(new PopupAction(option, popupButton.button));
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
