using System;
using UnityEngine;

namespace Pospec.Popup
{
    public class PopupButtonOption
    {
        /// <summary>
        /// Text displayed at popup button
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Action to be triggered
        /// </summary>
        public Action Callback { get; set; }

        /// <summary>
        /// Color of text of popup button
        /// </summary>
        public Color? Col { get; set; } = null;

        public PopupButtonOption(string name, Action callback) : this(name, callback, null) { }

        public PopupButtonOption(string name, Action callback, Color? color)
        {
            Name = name;
            Callback = callback;
            Col = color;
        }
    }
}
