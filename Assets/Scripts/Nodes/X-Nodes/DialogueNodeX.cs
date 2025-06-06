using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class DialogueNodeX : TextAdventureNodeX
{
    [Input] public ConnectionNodeX parentConnection;
    [Output] public ConnectionNodeX[] connections;
    public int ID;
    [TextArea] public string dialogue;

    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Dialogue; }

    /// <returns>This X-Node into a TextAdventure Node.</returns>
    public override TextAdventureNode ToTextAdventureNode()
    {
        DialogueNode dialogueNode = new DialogueNode(null);
        dialogueNode.ID = ID;
        dialogueNode.dialogue = dialogue;
        return dialogueNode;
    }

    public IEnumerable<ConnectionNodeX> GetConnections()
    {
        foreach (NodePort port in Outputs)
        {
            ConnectionNodeX[] connections = GetValue(port) as ConnectionNodeX[];
            if (connections != null)
            {
                foreach (ConnectionNodeX connection in connections)
                {
                    Debug.Log("Got me a ConnectionNodeX.");
                }
                return connections;
            }
        }

        return null;
    }

    public override object GetValue(NodePort port)
    {
        Debug.Log("Port Name: " + port.fieldName);

        switch (port.fieldName)
        {
            case "parentConnection":    return parentConnection;
            case "connections":         return connections;
            default:                    return base.GetValue(port);
        }
    }

    public override string ToString()
    {
        foreach(NodePort port in Outputs)
        {
            Debug.Log("Got an Output Port...");
        }

        foreach (ConnectionNodeX connection in GetConnections())
        {
            Debug.Log("Got a Connection!");
        }

        foreach (ConnectionNodeX connection in connections)
        {
            Debug.Log("Got a Connection!");
        }

        return connections != null ? connections.CollectionToString() : "EMPTY_ARRAY";
    }
}
}