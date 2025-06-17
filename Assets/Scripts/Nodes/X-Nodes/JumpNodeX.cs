using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/*===========================================================================
**
** Class:  JumpNodeX
**
** Purpose: X-Node acting as a JumpNode.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
[Serializable]
public class JumpNodeX : TextAdventureNodeX
{
    public string nodeName;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Jumper; }
}
}