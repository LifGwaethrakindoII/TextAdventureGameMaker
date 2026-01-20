using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Voidless.TextAdventureMaker
{
    public enum WordCategory { Undefined, Verb, Noun, Adjective, Article }

    [Serializable] public class StringWordCategoryDictionary : StringKeyDictionary<WordCategory> { /*...*/ }
    [Serializable] public class WordCategoryStringListDictionary : SerializableDictionary<WordCategory, List<string>> { /*...*/ }

    [Serializable]
    public class TextParserData
    {
        [SerializeField] private StringStringListDictionary _verbSynonyms;
        [SerializeField] private StringStringListDictionary _nounSynonyms;
        [SerializeField] private StringStringListDictionary _adjectiveSynonyms;
        [SerializeField] private StringStringListDictionary _articleSynonyms;
        private StringStringDictionary _verbSynonymsToCanonical;
        private StringStringDictionary _nounSynonymsToCanonical;
        private StringStringDictionary _adjectiveSynonymsToCanonical;
        private StringStringDictionary _articleSynonymsToCanonical;
        private StringWordCategoryDictionary _wordCategoryMap;

#region Getters/Setters:
        /// <summary>Gets verbSynonyms property.</summary>
        public StringStringListDictionary verbSynonyms
        {
            get { return _verbSynonyms; }
            private set { _verbSynonyms = value; }
        }

        /// <summary>Gets nounSynonyms property.</summary>
        public StringStringListDictionary nounSynonyms
        {
            get { return _nounSynonyms; }
            private set { _nounSynonyms = value; }
        }

        /// <summary>Gets adjectiveSynonyms property.</summary>
        public StringStringListDictionary adjectiveSynonyms
        {
            get { return _adjectiveSynonyms; }
            private set { _adjectiveSynonyms = value; }
        }

        /// <summary>Gets articleSynonyms property.</summary>
        public StringStringListDictionary articleSynonyms
        {
            get { return _articleSynonyms; }
            private set { _articleSynonyms = value; }
        }

        /// <summary>Gets and Sets verbSynonymsToCanonical property.</summary>
        public StringStringDictionary verbSynonymsToCanonical
        {
            get { return _verbSynonymsToCanonical; }
            set { _verbSynonymsToCanonical = value; }
        }

        /// <summary>Gets and Sets nounSynonymsToCanonical property.</summary>
        public StringStringDictionary nounSynonymsToCanonical
        {
            get { return _nounSynonymsToCanonical; }
            set { _nounSynonymsToCanonical = value; }
        }

        /// <summary>Gets and Sets adjectiveSynonymsToCanonical property.</summary>
        public StringStringDictionary adjectiveSynonymsToCanonical
        {
            get { return _adjectiveSynonymsToCanonical; }
            set { _adjectiveSynonymsToCanonical = value; }
        }

        /// <summary>Gets and Sets articleSynonymsToCanonical property.</summary>
        public StringStringDictionary articleSynonymsToCanonical
        {
            get { return _articleSynonymsToCanonical; }
            set { _articleSynonymsToCanonical = value; }
        }

        /// <summary>Gets wordCategoryMap property.</summary>
        public StringWordCategoryDictionary wordCategoryMap
        {
            get { return _wordCategoryMap; }
            private set { _wordCategoryMap = value; }
        }
#endregion

        /// <summary>Initializes all dictionaries.</summary>
        public void Initialize()
        {
            InitializeSynonymDictionaries();
            InitializeWordCategoryMaps();

            Debug.Log("[TextParserData] Initialized, with the following data: " + ToString());
        }

        /// <summary>Initializes Synonym Dictionaries.</summary>
        private void InitializeSynonymDictionaries()
        {
            verbSynonymsToCanonical = BuildReverseDictionary(verbSynonyms);
            nounSynonymsToCanonical = BuildReverseDictionary(nounSynonyms);
            adjectiveSynonymsToCanonical = BuildReverseDictionary(adjectiveSynonyms);
            articleSynonymsToCanonical = BuildReverseDictionary(articleSynonyms);
        }

        /// <summary>Initializes Word Category Maps.</summary>
        private void InitializeWordCategoryMaps()
        {
            wordCategoryMap = new StringWordCategoryDictionary();

            PopulateWordCategoryMap(WordCategory.Verb, verbSynonyms);
            PopulateWordCategoryMap(WordCategory.Noun, nounSynonyms);
            PopulateWordCategoryMap(WordCategory.Adjective, adjectiveSynonyms);
            PopulateWordCategoryMap(WordCategory.Article, articleSynonyms);
        }

        /// <summary>Populates Word Category Map.</summary>
        private void PopulateWordCategoryMap(WordCategory category, StringStringListDictionary synonyms)
        {
            if(synonyms == null || synonyms.Count == 0) return;

            foreach(var pair in synonyms)
            {
                // Add the canonical word
                AddToWordCategoryMap(pair.Key, category);
                
                // Add all synonyms
                foreach(string synonym in pair.Value)
                {
                    AddToWordCategoryMap(synonym, category);
                }
            }
        }

        /// <summary>Adds word into Word Category Map.</summary>
        /// <param name="word">Word to add.</param>
        /// <param name="category">Word Category.</param>
        private void AddToWordCategoryMap(string word, WordCategory category)
        {
            if(!wordCategoryMap.ContainsKey(word)) wordCategoryMap.Add(word, category);
        }

        /// <summary>Reverse builds a dictionary of synonyms associated with a canonical word.</summary>
        /// <param name="source">Source Synonym Dictionary.</param>
        /// <returns>Reverse-built Dictionary.</returns>
        private StringStringDictionary BuildReverseDictionary(StringStringListDictionary source)
        {
            var reverseDict = new StringStringDictionary();
            if(source == null) return reverseDict;

            foreach(var pair in source)
            {
                string canonical = pair.Key;
                foreach(string synonym in pair.Value)
                {
                    if(!reverseDict.ContainsKey(synonym))
                    {
                        reverseDict.Add(synonym, canonical);
                    }
                }
            }
            return reverseDict;
        }

        /// <summary>Gets word category.</summary>
        /// <param name="word">Word.</param>
        /// <returns>Word's category.</returns>
        public WordCategory GetWordCategory(string word)
        {
            if(wordCategoryMap == null || !wordCategoryMap.ContainsKey(word)) return WordCategory.Undefined;

            else return wordCategoryMap[word];
        }

        /// <summary>Gets word category, using Fuzzy-Matching.</summary>
        /// <param name="word">Word.</param>
        /// <returns>Word's category.</returns>
        public WordCategory GetWordCategoryWithFuzzyMatching(string word)
        {
            // 1. Try exact match first
            if (wordCategoryMap.TryGetValue(word, out var category))
                return category;

            // 2. Fuzzy match against all known words
            string closestMatch = FuzzyMatcher.FindClosestMatch(word, 2, wordCategoryMap.Keys.ToArray());

            return closestMatch != null ? wordCategoryMap[closestMatch] : WordCategory.Noun;
        }

        /// <summary>Gets canonical word from synonym.</summary>
        /// <param name="word">Synonim word.</param>
        /// <param name="category">Word's category.</param>
        /// <returns>Canonical word from synonym.</returns>
        public string GetCanonicalForm(string word, WordCategory category)
        {
            switch(category)
            {
                case WordCategory.Verb:
                    return verbSynonymsToCanonical.TryGetValue(word, out string verbCanonical) ? verbCanonical : word;
                case WordCategory.Noun:
                    return nounSynonymsToCanonical.TryGetValue(word, out string nounCanonical) ? nounCanonical : word;
                case WordCategory.Adjective:
                    return adjectiveSynonymsToCanonical.TryGetValue(word, out string adjCanonical) ? adjCanonical : word;
                case WordCategory.Article:
                    return articleSynonymsToCanonical.TryGetValue(word, out string artCanonical) ? artCanonical : word;
                default:
                    return word;
            }
        }

        /// <summary>Parses input and returns it as a ParsedCommand.</summary>
        /// <param name="input">Text to parse.</param>
        /// <returns>Parsed input into ParsedCommand.</returns>
        public ParsedCommand ParseInput(string input)
        {
            if(input == null || input.Length == 0) return ParsedCommand.Default();

            string[] words = input.Split(' ');

            ParsedCommand command = new ParsedCommand();
            command.rawInput = input;
            command.categories = new List<WordCategory>(words.Length);
            
            foreach(string word in words)
            {
                WordCategory category = GetWordCategoryWithFuzzyMatching(word); // Uses wordCategoryMap
                command.categories.Add(category);

                Debug.Log("[TextParserData] Analizing word " + word + " that falls into category " + category.ToString());

                switch(category) {
                    case WordCategory.Verb:
                        if(command.canonicalVerb == null) // Only keep first verb
                            command.canonicalVerb = GetCanonicalForm(word, WordCategory.Verb);
                    break;
                        
                    case WordCategory.Noun:
                        if(command.canonicalNoun == null) // Only keep first noun
                            command.canonicalNoun = GetCanonicalForm(word, WordCategory.Noun);
                    break;
                        
                    case WordCategory.Adjective:
                        command.adjectives.Add(word); // Keep all adjectives
                    break;
                        
                    case WordCategory.Article:
                        command.articles.Add(word); // Keep all articles
                    break;
                }
            }
            
            return command;
        }

        /// <returns>String representing this TextParserData.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Verbs: ");
            builder.AppendLine(verbSynonyms.DictionaryToString());
            builder.Append("Nounds: ");
            builder.AppendLine(nounSynonyms.DictionaryToString());
            builder.Append("Adjectives: ");
            builder.AppendLine(adjectiveSynonyms.DictionaryToString());
            builder.Append("Articles: ");
            builder.AppendLine(articleSynonyms.DictionaryToString());
            builder.Append("Canonical Verbs: ");
            builder.AppendLine(verbSynonymsToCanonical.ToString());
            builder.Append("Canonical Nounds: ");
            builder.AppendLine(nounSynonymsToCanonical.ToString());
            builder.Append("Canonical Adjectives: ");
            builder.AppendLine(adjectiveSynonymsToCanonical.ToString());
            builder.Append("Canonical Articles: ");
            builder.AppendLine(articleSynonymsToCanonical.ToString());
            builder.Append("Word Category Map: ");
            builder.AppendLine(wordCategoryMap.DictionaryToString());

            return builder.ToString();
        }
    }
}