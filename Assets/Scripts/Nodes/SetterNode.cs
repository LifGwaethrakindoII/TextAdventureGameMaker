using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public class SetterNode : TextAdventureNode
{
    public SetterGroup setters;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Setter; }

    public SetterNode(TextAdventureNode node) : base(node) { }

    /// <summary>Excecutes Node.</summary>
    public override NodeResult Excecute()
    {
        setters.Set();
        base.Excecute();
        return NodeResult.Success;
    }
}
}