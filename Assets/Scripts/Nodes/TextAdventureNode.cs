using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public enum NodeType { Undefined, Dialogue, Condition, Connection, Setter, Selector, Sequencer }

public enum NodeResult { Undefined, Success, Running, Failure, Error }

[Serializable]
public abstract class TextAdventureNode
{
    public ConnectionNode connectionNode;
    public DialogueNode dialogueNode;
    public ConditionNode conditionNode;
    public SetterNode setterNode;
    public TextAdventureNode next;

    /// <returns>Node Type.</returns>
    public abstract NodeType GetNodeType();

    /// <summary>Sets Next Node.</summary>
    /// <param name="node">Next Node</param>
    public void SetNextNode(TextAdventureNode node)
    {
        if (node == null) return;

        next = node;
        connectionNode = null;
        dialogueNode = null;
        conditionNode = null;
        setterNode = null;

        switch (node.GetNodeType())
        {
            case NodeType.Dialogue:
                dialogueNode = node as DialogueNode;
            break;

            case NodeType.Condition:
                conditionNode = node as ConditionNode;
            break;

            case NodeType.Connection:
                connectionNode = node as ConnectionNode;
            break;

            case NodeType.Setter:
                setterNode = node as SetterNode;
            break;

            case NodeType.Undefined:
            default:
                Debug.Log("Node with uncompatible node type " + node.GetNodeType().ToString() + " was provided.");
            break;
        }
    }

    public TextAdventureNode(TextAdventureNode node)
    {
        SetNextNode(node);
    }

    /// <summary>Excecutes Node.</summary>
    public virtual NodeResult Excecute()
    {
        switch (next.GetNodeType())
        {
            case NodeType.Dialogue:     return dialogueNode.Excecute();
            case NodeType.Condition:    return conditionNode.Excecute();
            case NodeType.Connection:   return connectionNode.Excecute();
            case NodeType.Undefined:
            default:                    return NodeResult.Error;
        }
    }
}
}