using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Pospec.Popup
{
    public class PopupButton : MonoBehaviour
    {
        public TextMeshProUGUI textField;
        public Button button;

        private void Reset()
        {
            textField = GetComponentInChildren<TextMeshProUGUI>();
            button = GetComponentInChildren<Button>();
        }
    }
}
