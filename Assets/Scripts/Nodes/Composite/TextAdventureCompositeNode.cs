using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public class TextAdventureCompositeNode : TextAdventureNode
{
    public TextAdventureNode[] children;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Undefined; }

    public TextAdventureCompositeNode(TextAdventureNode node) : base(node)
    {}
}
}