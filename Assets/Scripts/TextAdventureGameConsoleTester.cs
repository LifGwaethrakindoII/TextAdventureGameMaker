using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
    public class TextAdventureGameConsoleTester : MonoBehaviour
    {
        [SerializeField] private TextAdventureGame _game;

        /// <summary>Gets game property.</summary>
        public TextAdventureGame game { get { return _game; } }
    }
}