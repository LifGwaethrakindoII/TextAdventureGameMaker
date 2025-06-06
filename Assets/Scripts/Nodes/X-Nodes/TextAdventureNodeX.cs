using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNode;
using UnityEditor.Build.Pipeline;

using IO = XNode.NodePort.IO;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public abstract class TextAdventureNodeX : Node
{
    [Input] public TextAdventureNodeX parent;
    [Output] public TextAdventureNodeX next;
    public NodeType childrenNodeType;

    /// <returns>Node Type.</returns>
    public virtual NodeType GetNodeType() { return NodeType.Undefined; }

    /// <summary>Gets Connection Nodes.</summary>
    /// <param name="_portName">Name of the NodePort containing the Nodes.</param>
    /// <param name="_direction">Direction of the Nodes [Input by default].</param>
    /// <param name="_default">Default value to return if no Nodes could be retreived [null by default].</param>
    /// <returns>Connection Nodes, default otherwise.</returns>
    public T[] GetNodes<T>(string _portName, IO _direction = IO.Input, T[] _default = default) where T : Node
    {
        NodePort nodePort =  _direction == IO.Input ? GetInputPort(_portName) : GetOutputPort(_portName);

        if(nodePort == null) return _default;

        int length = nodePort.ConnectionCount;

        if(length == 0) return _default;

        List<T> nodes = new List<T>();

        for(int i = 0; i < length; i++)
        {
            NodePort port = nodePort.GetConnection(i);
            
            if(port == null) continue;

            T node = port.node as T;

            if(node != null) continue;

            nodes.Add(node);
        }

        return nodes.Count > 0 ? nodes.ToArray() : _default;
    }

    /// <summary>Gets Connection Node.</summary>
    /// <param name="_portName">Name of the NodePort containing the Node.</param>
    /// <param name="_direction">Direction of the Node [Input by default].</param>
    /// <param name="_default">Default value to return if no Node could be retreived [null by default].</param>
    /// <returns>Connection Node, default otherwise.</returns>
    public T GetNode<T>(string _portName, IO _direction = IO.Input, T _default = default) where T : Node
    {
        NodePort nodePort =  _direction == IO.Input ? GetInputPort(_portName) : GetOutputPort(_portName);

        if(nodePort == null) return _default;

        T result = nodePort.Connection.node as T;
        bool success = result != null;

        return success ? result : _default;
    }

    /// <summary>Gets Input Connection Node.</summary>
    /// <param name="_portName">Name of the NodePort containing the Node.</param>
    /// <param name="_default">Default value to return if no Node could be retreived [null by default].</param>
    /// <returns>Input Connection Node, default otherwise.</returns>
    public T GetInputNode<T>(string _portName, T _default = default) where T : Node
    {
        return GetNode<T>(_portName, IO.Input, _default);
    }

    /// <summary>Gets Output Connection Node.</summary>
    /// <param name="_portName">Name of the NodePort containing the Node.</param>
    /// <param name="_default">Default value to return if no Node could be retreived [null by default].</param>
    /// <returns>Output Connection Node, default otherwise.</returns>
    public T GetOutputNode<T>(string _portName, T _default = default) where T : Node
    {
        return GetNode<T>(_portName, IO.Output, _default);
    }

    /// <summary>Gets information of NodePort.</summary>
    /// <param name="_portName">Name of the NodePort.</param>
    /// <returns>NodePort's info.</returns>
    public string GetInputNodePortInfo(string _portName)
    {
        NodePort port = GetInputPort(_portName);

        if (port == null) return string.Empty;

        StringBuilder builder = new StringBuilder();

        builder.Append("{ Field Name: ");
        builder.Append(port.fieldName);
        builder.Append(", Type: ");
        builder.Append(port.GetType().Name);
        builder.Append(", Direction: ");
        builder.Append(port.direction);
        builder.Append(", Connection Type: ");
        builder.Append(port.connectionType.ToString());
        builder.Append(", Type Constraint: ");
        builder.Append(port.typeConstraint.ToString());
        builder.Append(", Dynamic: ");
        builder.Append(port.IsDynamic.ToString());
        builder.Append(" }");

        return builder.ToString();
    }

    /// <returns>This X-Node into a TextAdventure Node.</returns>
    public virtual TextAdventureNode ToTextAdventureNode() => null;

    /// <returns>String representing this Node's info.</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("{ Node Type =  ");
        builder.Append(GetNodeType().ToString());

        return builder.ToString();
    }

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
}
}