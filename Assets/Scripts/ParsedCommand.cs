using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
    [Serializable]
    public class ParsedCommand : MonoBehaviour
    {
        private const string EMPTY = "EMPTY";

        public string rawInput;
        public string canonicalVerb;
        public string canonicalNoun;
        public List<string> adjectives;
        public List<string> articles;
        public List<WordCategory> categories;

        public ParsedCommand()
        {
            adjectives = new List<string>();
            articles = new List<string>();
            categories = new List<WordCategory>();
        }

        public ParsedCommand(string _rawInput, string _canonicalVerb, string _canonicalNoun) : this()
        {
            rawInput = _rawInput;
            canonicalVerb = _canonicalVerb;
            canonicalNoun = _canonicalNoun;
        }

        public static ParsedCommand Default()
        {
            return new ParsedCommand(EMPTY, EMPTY, EMPTY);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("{ Raw Input: ");
            builder.Append(rawInput);
            builder.Append(", Canonical Verb: ");
            builder.Append(canonicalVerb);
            builder.Append(", Canonical Noun: ");
            builder.Append(canonicalNoun);
            builder.Append(", Adjectives: ");
            builder.Append(adjectives.CollectionToString());
            builder.Append(", Articles: ");
            builder.Append(articles.CollectionToString());
            builder.Append(", Word Categories: ");
            builder.Append(categories.CollectionToString());

            return builder.ToString();
        }
    }
}