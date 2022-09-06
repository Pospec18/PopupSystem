using System.Collections.Generic;
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

        private List<Transform> activated = new List<Transform>();

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
            foreach (Transform item in activated)
            {
                item.gameObject.SetActive(false);
            }
        }

        protected void SetupPopup(string text, Sprite image, bool blockRaycasts)
        {
            ResetPopup();
            activated.Clear();
            TunrActive(transform);
            textField.text = text;
            BlockRaycasts = blockRaycasts;
            if (popupIMG != null)
                popupIMG.sprite = image;
        }

        private void TunrActive(Transform parent)
        {
            if (parent == null)
                return;

            if(!parent.gameObject.activeInHierarchy)
            {
                activated.Add(parent);
                parent.gameObject.SetActive(true);
            }
            TunrActive(parent.parent);
        }

        protected virtual void ResetPopup()
        {

        }
    }
}
