using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/*===========================================================================
**
** Class:  DialogueNode
**
** Purpose: X-Node representing a DialogueNode.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
[Serializable]
public class DialogueNodeX : TextAdventureCompositeNodeX
{
    [TextArea] public string dialogue;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Dialogue; }
}
}