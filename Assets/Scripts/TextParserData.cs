using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Voidless.TextAdventureMaker
{
public enum WordCategory { Verb, Noun, Adjective, Article }

[Serializable] public class StringWordCategoryDictionary : StringKeyDictionary<WordCategory> { /*...*/ }
[Serializable] public class WordCategoryStringListDictionary : SerializableDictionary<WordCategory, List<string>> { /*...*/ }

[Serializable]
public struct TextParserData
{
    //[SerializeField] private StringWordCategoryDictionary _wordCategoryMap;
    [SerializeField] private List<string> _verbs;
    [SerializeField] private List<string> _nouns;
    [SerializeField] private List<string> _adjectives;
    [SerializeField] private List<string> _articles;
    //[SerializeField] private StringHashSet _verbsMap; /// \TODO REPAIR HashSet Serialization/Deserialization with Odin Inspector
    private WordCategoryStringListDictionary _wordsMap;
    private StringWordCategoryDictionary _wordCategoryMap;

    /// <summary>Gets verbs property.</summary>
    public List<string> verbs { get { return _verbs; } }

    /// <summary>Gets nouns property.</summary>
    public List<string> nouns { get { return _nouns; } }

    /// <summary>Gets adjectives property.</summary>
    public List<string> adjectives { get { return _adjectives; } }

    /// <summary>Gets articles property.</summary>
    public List<string> articles { get { return _articles; } }

    /// <summary>Gets and Sets wordsMap property.</summary>
    public WordCategoryStringListDictionary wordsMap
    {
        get { return _wordsMap; }
        private set { _wordsMap = value; }
    }

    /// <summary>Gets and Sets wordCategoryMap property.</summary>
    public StringWordCategoryDictionary wordCategoryMap
    {
        get { return _wordCategoryMap; }
        private set { _wordCategoryMap = value; }
    }

    /// <summary>Initializes Data.</summary>
    public void Initialize()
    {
        wordsMap = new WordCategoryStringListDictionary();

        wordsMap.Add(WordCategory.Verb, verbs);
        wordsMap.Add(WordCategory.Noun, nouns);
        wordsMap.Add(WordCategory.Adjective, adjectives);
        wordsMap.Add(WordCategory.Article, articles);
    
        wordCategoryMap = new StringWordCategoryDictionary();

        foreach(string verb in verbs)
        {
            wordCategoryMap.Add(verb, WordCategory.Verb);
        }
        foreach(string noun in nouns)
        {
            wordCategoryMap.Add(noun, WordCategory.Noun);
        }
        foreach(string adjective in adjectives)
        {
            wordCategoryMap.Add(adjective, WordCategory.Adjective);
        }
        foreach(string article in articles)
        {
            wordCategoryMap.Add(article, WordCategory.Article);
        }
    }
}
}