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
        /// Is this PopupOption Generic
        /// </summary>
        bool IsGeneric { get; }
    }

    public interface IPopupButtonOption : IPopupOption
    {
        /// <summary>
        /// Text displayed at popup button
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Color of text of popup button
        /// </summary>
        Color? Col { get; set; }
    }

    /// <summary>
    /// Non generic version of PopupAction
    /// </summary>
    public class PopupOption : IPopupOption
    {
        public bool IsGeneric => false;

        public Action action;

        public PopupOption(Action _action)
        {
            action = _action;
        }
    }

    /// <summary>
    /// Generic version of PopupOption, passing Value into the onClick Action
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PopupOption<T> : IPopupOption
    {
        public bool IsGeneric => true;

        public Action<T> action;
        public T value;

        public PopupOption(Action<T> _action, T _value)
        {
            action = _action;
            value = _value;
        }
    }

    public class PopupButtonOption : PopupOption, IPopupButtonOption
    {
        public string Name { get; set; }
        public Color? Col { get; set; } = null;


        public PopupButtonOption(string _name, Action _action) : base(_action)
        {
            Name = _name;
            Col = null;
        }

        public PopupButtonOption(string _name, Action _action, Color _color) : base(_action)
        {
            Name = _name;
            Col = _color;
        }
    }

    public class PopupButtonOption<T> : PopupOption<T>, IPopupButtonOption
    {
        public string Name { get; set; }
        public Color? Col { get; set; } = null;

        public PopupButtonOption(string _name, Action<T> _action, T _value) : base(_action, _value)
        {
            Name = _name;
            Col = null;
        }

        public PopupButtonOption(string _name, Action<T> _action, T _value, Color _color) : base(_action, _value)
        {
            Name = _name;
            Col = _color;
        }
    }
}
