using System.Collections.Generic;
using System.Linq;

namespace Pospec.Popup
{
    public class SearchBar<T>
    {
        private readonly IDictionary<char, ISearchNode> items = new Dictionary<char, ISearchNode>();

        private interface ISearchNode
        {
            IEnumerable<SearchResult<T>> GetValues(string key);
            void Add(string key, SearchResult<T> value);
        }

        private class SearchNode : ISearchNode
        {

            private readonly IList<KeyValuePair<string, SearchResult<T>>> lines = new List<KeyValuePair<string, SearchResult<T>>>();
            private static readonly char[] escapeCharacters = { ':', '-' };

            public IEnumerable<SearchResult<T>> GetValues(string key)
            {
                List<SearchResult<T>> startOfLine = new List<SearchResult<T>>();
                List<SearchResult<T>> startOfWord = new List<SearchResult<T>>();
                List<SearchResult<T>> startOfSomething = new List<SearchResult<T>>();
                List<SearchResult<T>> inMiddle = new List<SearchResult<T>>();
                foreach (var line in lines)
                {
                    if (!line.Key.EndsWith(key))
                        continue;

                    int lineLength = line.Key.Length;
                    int keyLength = key.Length;
                    if (lineLength == keyLength)
                        startOfLine.Add(line.Value);
                    else if (line.Key[lineLength - keyLength - 1] == ' ')
                        startOfWord.Add(line.Value);
                    else if (escapeCharacters.Contains(line.Key[lineLength - keyLength - 1]))
                        startOfSomething.Add(line.Value);
                    else
                        inMiddle.Add(line.Value);
                }

                return startOfLine.Union(startOfWord).Union(startOfSomething).Union(inMiddle);
            }

            public void Add(string key, SearchResult<T> value)
            {
                lines.Add(KeyValuePair.Create(key, value));
            }
        }

        public void Add(string key, T value)
        {
            string keyLower = key.ToLower();
            SearchResult<T> searchValue = new SearchResult<T>(key, value);
            for (int i = 0; i < keyLower.Length; i++)
            {
                items.TryAdd(keyLower[i], new SearchNode());
                items[keyLower[i]].Add(keyLower.Substring(0, i + 1), searchValue);
            }
        }

        public IEnumerable<SearchResult<T>> Find(string key)
        {
            string keyLower = key.ToLower();
            if (items.TryGetValue(keyLower[keyLower.Length - 1], out ISearchNode node))
                return node.GetValues(keyLower);
            return null;
        }
    }

    public class SearchResult<T>
    {
        public string Key { get; private set; }
        public T Value { get; private set; }

        public SearchResult(string key, T value)
        {
            Key = key;
            Value = value;
        }
    }
}
