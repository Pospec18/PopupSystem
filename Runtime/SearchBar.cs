using System.Collections.Generic;
using System.Linq;

namespace Pospec.Popup
{
    public class SearchBar<T>
    {
        private readonly IDictionary<char, ISearchNode> items = new Dictionary<char, ISearchNode>();

        private interface ISearchNode
        {
            IEnumerable<T> GetValues(string key);
            void Add(string key, T value);
        }

        private class SearchNode : ISearchNode
        {
            private readonly IList<KeyValuePair<string, T>> lines = new List<KeyValuePair<string, T>>();
            private static readonly char[] escapeCharacters = { ':', '-' };

            public IEnumerable<T> GetValues(string key)
            {
                List<T> startOfLine = new List<T>();
                List<T> startOfWord = new List<T>();
                List<T> startOfSomething = new List<T>();
                List<T> inMiddle = new List<T>();
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

            public void Add(string key, T value)
            {
                lines.Add(KeyValuePair.Create(key, value));
            }
        }

        public void Add(string key, T value)
        {
            string keyLower = key.ToLower();
            for (int i = 0; i < keyLower.Length; i++)
            {
                items.TryAdd(keyLower[i], new SearchNode());
                items[keyLower[i]].Add(keyLower.Substring(0, i + 1), value);
            }
        }

        public IEnumerable<T> Find(string key)
        {
            string keyLower = key.ToLower();
            if (items.TryGetValue(keyLower[keyLower.Length - 1], out ISearchNode node))
                return node.GetValues(keyLower);
            return null;
        }
    }
}
