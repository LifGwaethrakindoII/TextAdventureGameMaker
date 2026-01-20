using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/*===========================================================================
**
** Class:  TextAdventureSelectorNode
**
** Purpose: X-Node that acts as a Selector (follows the flow of the first
** successful child).
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
    [Serializable]
    public class TextAdventureSelectorNodeX : TextAdventureCompositeNodeX
    {
        /// <returns>Node Type.</returns>
        public override NodeType GetNodeType() { return NodeType.Selector; }
    }
}