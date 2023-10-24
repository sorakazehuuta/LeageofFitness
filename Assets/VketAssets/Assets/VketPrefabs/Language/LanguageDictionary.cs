using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vket.VketPrefabs.Language
{
    [CreateAssetMenu(menuName = "VketPrefabs/LanguageDictionary", fileName = "LanguageDictionary")]
    public class LanguageDictionary : ScriptableObject, ILanguage, ISerializationCallbackReceiver
    {
        [System.Serializable]
        public struct Pair
        {
            public string key;
            public string value;
        }

        [SerializeField]
        private Pair[] language = Array.Empty<Pair>();
        private Dictionary<string, string> dictionary;

        public string this[string key] => dictionary[key];

        internal Pair[] GetTranslations()
        {
            return language;
        }

        internal void SetTranslations(Pair[] pairs)
        {
            language = pairs;
            OnAfterDeserialize();
        }

        public bool TryGetValue(string key, out string value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            dictionary = new Dictionary<string, string>();
            foreach (var text in language)
            {
                dictionary.Add(text.key, text.value);
            }
        }
    }
}