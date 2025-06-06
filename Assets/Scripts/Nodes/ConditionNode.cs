using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class ConditionNode : TextAdventureNode
{
    public ComparerGroup[] comparers;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Condition; }

    public ConditionNode(TextAdventureNode node) : base(node) { }
}
}