using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNode;

/*===========================================================================
**
** Class:  ConnectionNodeX
**
** Purpose: X-Node representing a ConnectionNode.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
    [Serializable]
    public class ConnectionNodeX : TextAdventureNodeX
    {
        public string validCommand;

        /// <returns>Node Type.</returns>
        public override NodeType GetNodeType() { return NodeType.Connection; }

        public bool MatchesCommand(ParsedCommand cmd)
        {
            // Split required pattern (e.g. "take [the] [shiny] sword")
            string[] patternParts = validCommand.Split(' ');
            
            // Check verb
            if (cmd.canonicalVerb != patternParts[0]) 
                return false;
            
            // Check noun (last part of pattern)
            if (patternParts.Length > 1 && 
                cmd.canonicalNoun != patternParts[patternParts.Length - 1])
                return false;
            
            return true;
        }

        /// <returns>Node's Content.</returns>
        public override string GetContent() { return validCommand; }
    }
}