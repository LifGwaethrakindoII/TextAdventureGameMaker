using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*============================================================
**
** Class:  TextAdventureGameConsoleTester
**
** Purpose: Editor-Only class that, extended with an Inspector,
** tests the dialogue flow in Editor mode.
**
** Author: Lîf Gwaethrakindo
**
==============================================================*/
namespace Voidless.TextAdventureMaker
{
    public class TextAdventureGameConsoleTester : MonoBehaviour
    {
        [SerializeField] private TextAdventureGame _game;

        /// <summary>Gets game property.</summary>
        public TextAdventureGame game { get { return _game; } }
    }
}