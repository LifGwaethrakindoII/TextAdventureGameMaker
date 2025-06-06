using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNode;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public abstract class TextAdventureCompositeNodeX : TextAdventureNodeX
{
    [Output] public TextAdventureNodeX[] children;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Undefined; }
}
}