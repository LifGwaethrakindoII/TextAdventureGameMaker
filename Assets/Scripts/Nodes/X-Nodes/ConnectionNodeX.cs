using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNode;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class ConnectionNodeX : TextAdventureNodeX
{
    [Input] public DialogueNodeX parentNode;
    [Output] public DialogueNodeX targetNode;
    public List<string> validVerbs;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Connection; }

    public override object GetValue(NodePort port)
    {
        switch (port.fieldName)
        {
            case "parentNode": Debug.Log("Give me the parent"); return parentNode;
            case "targetNode": return targetNode;
            default: return base.GetValue(port);
        }
    }

    public DialogueNodeX GeParentNode() => parentNode;



    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(base.ToString());
        builder.Append(", Parent Node: ");
        builder.Append(parentNode.EvaluateForNullReference());
        builder.Append(", Target Node: ");
        builder.Append(targetNode.EvaluateForNullReference());
        builder.Append(", Valid Verbs: ");
        builder.Append(validVerbs.CollectionToString(false));
        builder.Append(" }");

        return builder.ToString();
    }
}
}