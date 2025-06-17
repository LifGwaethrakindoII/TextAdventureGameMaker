using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/*===========================================================================
**
** Class:  ConditionNodeX
**
** Purpose: X-Node acting as a ConditionNode.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
[Serializable]
public class ConditionNodeX : TextAdventureNodeX
{
    public ComparerGroup[] comparers;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Condition; }
}
}