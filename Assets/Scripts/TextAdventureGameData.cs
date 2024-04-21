using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
    [CreateAssetMenu(menuName = "Text-AdventureMaker / Game Data Asset")]
    public class TextAdventureGameData : ScriptableObject
    {
        [SerializeField] private TextParserData _parserData;
        [Space(5f)]
        [Header("Game Settings:")]
        [SerializeField] private float _typingDuration;

        /// <summary>Gets parserData property.</summary>
        public TextParserData parserData { get { return _parserData; } }

        /// <summary>Gets typingDuration property.</summary>
        public float typingDuration { get { return _typingDuration; } }

        /// <summary>Initializes Data.</summary>
        public void Initialize()
        {
            parserData.Initialize();
        }
    }
}