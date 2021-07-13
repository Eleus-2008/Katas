using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public static class Anagrams
    {
        public static List<string> FindAnagrams(string word, List<string> words)
        {
            var wordDictionary = GetCharDictionary(word);
            var results = new List<string>();

            foreach (var w in words)
            {
                if (CheckAnagrams(wordDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), w))
                {
                    results.Add(w);
                }
            }

            return results;

            static bool CheckAnagrams(Dictionary<char, int> wordDictionary, string word)
            {
                foreach (var c in word)
                {
                    if (wordDictionary.ContainsKey(c))
                        wordDictionary[c]--;
                    else
                        return false;

                    if (wordDictionary[c] == 0)
                        wordDictionary.Remove(c);
                }

                return !wordDictionary.Any();
            }

            static Dictionary<char, int> GetCharDictionary(string word)
            {
                var dictionary = new Dictionary<char, int>();
                foreach (var c in word)
                {
                    if (!dictionary.ContainsKey(c))
                        dictionary.Add(c, 1);
                    else
                        dictionary[c]++;
                }

                return dictionary;
            }
        }
    }
}