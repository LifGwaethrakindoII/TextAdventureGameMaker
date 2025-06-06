using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class ConnectionNode : TextAdventureNode
{
    public DialogueNode targetNode;
    public List<string> validVerbs;

    public ConnectionNode(TextAdventureNode node) : base(node)
    {
        targetNode = null;
        validVerbs = new List<string>();
    }

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Connection; }
}
}