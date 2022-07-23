using System;
using UnityEngine;

namespace Pospec.Popup
{
    /// <summary>
    /// Interface of one Popup option
    /// </summary>
    public interface IPopupOption
    {
        /// <summary>
        /// Text displayed at popup button
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Color of text of popup button
        /// </summary>
        Color? Col { get; set; }
        /// <summary>
        /// Is this PopupOption Generic
        /// </summary>
        bool IsGeneric { get; }
    }

    /// <summary>
    /// Non generic version of PopupAction
    /// </summary>
    public class PopupOption : IPopupOption
    {
        public string Name { get; set; }
        public Color? Col { get; set; }
        public bool IsGeneric => false;

        public Action action;

        public PopupOption(string _name, Action _action)
        {
            Name = _name;
            action = _action;
            Col = null;
        }

        public PopupOption(string _name, Action _action, Color _color)
        {
            Name = _name;
            action = _action;
            Col = _color;
        }
    }

    /// <summary>
    /// Generic version of PopupOption, passing Value into the onClick Action
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PopupOption<T> : IPopupOption
    {
        public string Name { get; set; }
        public Color? Col { get; set; }
        public bool IsGeneric => true;

        public Action<T> action;
        public T value;

        public PopupOption(string _name, Action<T> _action, T _value)
        {
            Name = _name;
            action = _action;
            value = _value;
            Col = null;
        }

        public PopupOption(string _name, Action<T> _action, T _value, Color _color)
        {
            Name = _name;
            action = _action;
            value = _value;
            Col = _color;
        }
    }
}
