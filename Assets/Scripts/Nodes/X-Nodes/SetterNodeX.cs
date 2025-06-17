using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===========================================================================
**
** Class:  SetterNodeX
**
** Purpose: X-Node representing a SetterNode.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
public class SetterNodeX : TextAdventureNodeX
{
    public SetterGroup setters;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Setter; }
}
}