using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Voidless.TextAdventureMaker
{
[CreateAssetMenu]
public class TextAdventureXNodeGraph : NodeGraph
{
    public DialogueNodeX GetRootNode()
    {
        foreach (Node node in nodes)
        {
            DialogueNodeX dialogueNode = node as DialogueNodeX;
            if(dialogueNode != null && dialogueNode.ID == 0) return dialogueNode;
        }

        return null;
    }

    public DialogueNode GenerateDialogueNodeGraph()
    {
        return ConvertToDialogueNode(GetRootNode());
    }

    public static DialogueNode ConvertToDialogueNode(TextAdventureNodeX nodeX)
    {
        DialogueNode dialogueNode = new DialogueNode(null);
        /*dialogueNode.ID = nodeX.ID;
        dialogueNode.dialogue = nodeX.dialogue;*/
        dialogueNode.connections = new List<ConnectionNode>();

        Debug.Log("Root: " + nodeX.ToString());

        foreach(TextAdventureNodeX node in nodeX.GetTAMConnections())
        {
            switch (node.GetNodeType())
            {
                default:
                case NodeType.Undefined: break;

                case NodeType.Dialogue:
                    /*DialogueNodeX dialogueNodeX = node as DialogueNodeX;
                    DialogueNode dialogueNode = new DialogueNode();*/
                    /*dialogueNode.ID = nodeX.ID;
                    dialogueNode.dialogue = nodeX.dialogue;*/
                    dialogueNode.connections = new List<ConnectionNode>();
                    break;

                case NodeType.Connection:

                break;

                case NodeType.Condition:

                break;
            }

            ConnectionNodeX connectionNodeX = node as ConnectionNodeX;
            if(connectionNodeX == null) continue;

            Debug.Log("Here's a Connection: " + connectionNodeX.ToString());
            ConnectionNode nodeConnection = new ConnectionNode(null);
            DialogueNodeX parentNode = connectionNodeX.GetInputNode<DialogueNodeX>("parentNode");
            DialogueNodeX targetNode = connectionNodeX.GetOutputNode<DialogueNodeX>("targetNode");
            Debug.Log("Port Info: " + node.GetInputNodePortInfo("parentNode"));
            Debug.Log("Do we have parent node? " + parentNode.EvaluateForNullReference());
            Debug.Log("Port Info: " + node.GetInputNodePortInfo("targetNode"));
            Debug.Log("Do we have target node? " + targetNode.EvaluateForNullReference());

            /*nodeConnection.targetNode = ConvertToDialogueNode(targetNode);
            nodeConnection.validVerbs = connectionNodeX.validVerbs;
            dialogueNode.connections.Add(nodeConnection);*/
        }

        /*foreach (NodePort outputPort in nodeX.Outputs)
        {
            foreach (NodePort inputPort in outputPort.GetConnections())
            {
                if (inputPort.node != null)
                {
                    ConnectionNodeX connectionNodeX = inputPort.node as ConnectionNodeX;
                    if (connectionNodeX != null)
                    {
                        Debug.Log("Here's a Connection: " + connectionNodeX.ToString());
                        ConnectionNode nodeConnection = new ConnectionNode();
                        nodeConnection.targetNode = ConvertToDialogueNode(inputPort.node as DialogueNodeX);
                        nodeConnection.validVerbs = connectionNodeX.validVerbs;
                        dialogueNode.connections.Add(nodeConnection);
                    }
                }
            }
        }*/

        return dialogueNode;
    }

    public static void TEST_PROPERTYSTUFF()
    {
        
    }

}
}