using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNode;
using UnityEditor.Build.Pipeline;
using Voidless.XNode;

using IO = XNode.NodePort.IO;

/*===========================================================================
**
** Class:  TextAdventureNodeX
**
** Purpose: X-Node representing a Text-Adventure Node.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
public enum NodeType { Undefined, Dialogue, Condition, Connection, Setter, Selector, Sequencer, Jumper }

public enum NodeResult { Undefined, Success, Running, Failure, Error }

[Serializable]
public abstract class TextAdventureNodeX : Node
{
    [Input] public TextAdventureNodeX parent;
    [Output] public TextAdventureNodeX next;
    public string _name;

    /// <summary>Gets and Sets name property.</summary>
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    /// <returns>Node Type.</returns>
    public virtual NodeType GetNodeType() { return NodeType.Undefined; }

    /// <summary>GetValue should be overridden to return a value for any specified output port.</summary>
    public override object GetValue(NodePort port)
    {
        switch(port.fieldName)
        {
            case "parent":  return parent;
            case "next":    return next;
            default:        return default(object);
        }
    }

    public TextAdventureNodeX GetParent()
    {
        if(parent == null) parent = this.GetInputNode<TextAdventureNodeX>("parent");

        return parent;
    }

    public TextAdventureNodeX GetNext()
    {
        if(next == null) next = this.GetOutputNode<TextAdventureNodeX>("next");

        return next;
    }

    /// <summary>Iterates through connections of the type TextAdventureNodeX.</summary>
    public virtual IEnumerable<TextAdventureNodeX> GetTAMConnections()
    {
        foreach (NodePort outputPort in Outputs)
        {
            foreach (NodePort inputPort in outputPort.GetConnections())
            {
                TextAdventureNodeX node = inputPort.node as TextAdventureNodeX;
                if(node != null) yield return node;
            }
        }
    }

    /// <summary>Iterates through children connections [or single child].</summary>
    public virtual IEnumerable<TextAdventureNodeX> IterateThroughChildren()
    {
        yield return GetNext();
    }

    /// <summary>Resets Node's port references.</summary>
    public virtual void Reset()
    {
        parent = null;
        next = null;
    }

    /// <returns>String representing this Node's info.</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("{ Node Type =  ");
        builder.Append(GetNodeType().ToString());
        builder.Append(", Has parent: ");
        builder.Append(GetParent() != null);
        builder.Append(", Has children: ");
        builder.Append(GetNext() != null);
        builder.Append(", Name: ");
        builder.Append(name != string.Empty ? name : "NONE");
        builder.Append(" }");

        return builder.ToString();
    }
}
}