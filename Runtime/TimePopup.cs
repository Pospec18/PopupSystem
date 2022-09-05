using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Pospec.Popup
{
    /// <summary>
    /// Info Popup that will close automatically in time
    /// </summary>
    public class TimePopup : BasePopup
    {
        private const int defaultTime = 2;

        [SerializeField] private Button closeButton;

        private Coroutine closeCoroutine;

        private void Start()
        {
            closeButton?.onClick.AddListener(Close);
        }

        #region Use

        /// <summary>
        /// Show and setup popup
        /// </summary>
        /// <param name="text">Info text</param>
        /// <param name="time">Time after will popup automatically close</param>
        public void Use(string text, float time = defaultTime)
        {
            Use(text, null, BlockRaycasts, time);
        }

        /// <summary>
        /// Show and setup popup
        /// </summary>
        /// <param name="text">Info text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="time">Time after will popup automatically close</param>
        public void Use(string text, Sprite image, float time = defaultTime)
        {
            Use(text, image, BlockRaycasts, time);
        }

        /// <summary>
        /// Show and setup popup
        /// </summary>
        /// <param name="text">Info text</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="time">Time after will popup automatically close</param>
        public void Use(string text, bool blockRaycasts, float time = defaultTime)
        {
            Use(text, null, blockRaycasts, time);
        }

        /// <summary>
        /// Show and setup popup
        /// </summary>
        /// <param name="text">Info text</param>
        /// <param name="image">Image to be shown in popup</param>
        /// <param name="blockRaycasts">Set if block Raycasts outside popup</param>
        /// <param name="time">Time after will popup automatically close</param>
        public void Use(string text, Sprite image, bool blockRaycasts, float time = defaultTime)
        {
            SetupPopup(text, image, blockRaycasts);
            closeCoroutine = StartCoroutine(CloseAfter(time));
        }

        #endregion

        private IEnumerator CloseAfter(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            Close();
        }

        public override void Close()
        {
            if (closeCoroutine != null)
            {
                StopCoroutine(closeCoroutine);
                closeCoroutine = null;
            }
            base.Close();
        }
    }
}
