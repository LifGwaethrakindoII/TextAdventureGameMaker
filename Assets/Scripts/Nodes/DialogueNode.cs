using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class DialogueNode : TextAdventureNode
{
    public int ID;
    [TextArea] public string dialogue;
    public List<ConnectionNode> connections;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Dialogue; }

    public DialogueNode(TextAdventureNode node) : base(node) { }
}
}