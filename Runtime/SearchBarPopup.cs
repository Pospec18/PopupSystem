using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Popup
{
    public class SearchBarPopup : BasePopup
    {
        private readonly SearchBar<Action> searchTree = new SearchBar<Action>();
        private readonly List<PopupButton> searchList = new List<PopupButton>();

        [SerializeField] private Transform resultsTab;
        [SerializeField] private PopupButton resultPrefab;
        [SerializeField] private TMPro.TMP_InputField searchField;

        private void Start()
        {
            searchField.onSubmit.AddListener(Use);
        }

        public void Add(string key, Action onSelected)
        {
            searchTree.Add(key, onSelected);
        }

        public void Use(string key)
        {
            foreach (var item in searchTree.Find(""))
            {
                PopupButton button = Instantiate(resultPrefab, resultsTab);
                button.textField.text = item.Key;
                button.button.onClick.AddListener(() =>
                {
                    item.Value();
                    Close();
                });
                searchList.Add(button);
            }
        }

        protected override void Reset()
        {
            base.Reset();
            foreach (var item in searchList)
            {
                Destroy(item);
            }

            searchList.Clear();
        }
    }
}
