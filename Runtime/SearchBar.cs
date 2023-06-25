using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Popup
{
    public class SearchBar : MonoBehaviour
    {
        private readonly SearchBarTree<Action> searchTree = new SearchBarTree<Action>();
        private readonly List<PopupButton> searchList = new List<PopupButton>();

        [SerializeField] private Transform resultsTab;
        [SerializeField] private GameObject scrollView;
        [SerializeField] private PopupButton resultPrefab;
        [SerializeField] private TMPro.TMP_InputField searchField;

        private void Start()
        {
            searchField.onSubmit.AddListener(Use);
            Close();
        }

        public void Add(string key, Action onSelected)
        {
            searchTree.Add(key, onSelected);
        }

        public void Use(string key)
        {
            Reset();
            scrollView.SetActive(true);
            foreach (var item in searchTree.Find(key))
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

        protected void Reset()
        {
            foreach (var item in searchList)
            {
                Destroy(item.gameObject);
            }

            searchList.Clear();
        }

        public void Close()
        {
            scrollView.SetActive(false);
        }
    }
}
