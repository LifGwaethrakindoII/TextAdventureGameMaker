using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Voidless.TextAdventureMaker
{
    [CreateAssetMenu]
    public class TextAdventureXNodeGraph : NodeGraph
    {
        private TextAdventureNodeX _current;

        /// <summary>Gets and Sets current property.</summary>
        public TextAdventureNodeX current
        {
            get { return _current; }
            set { _current = value; }
        }

        public TextAdventureNodeX GetRootNode()
        {
            foreach (Node node in nodes)
            {
                TextAdventureNodeX TAMNode = node as TextAdventureNodeX;
                if(TAMNode != null && TAMNode.name == "Root") return TAMNode;
            }

            return null;
        }

        /*
        public XNode.Node GetNextNode(XNode.Node currentNode)
        {  
            // Single-child nodes (e.g., DialogueNode)  
            if (currentNode is ISingleChildNode singleChild) {  
                return singleChild.GetNextNode();  
            }  

            // Multi-child nodes (e.g., SelectorNode)  
            if (currentNode is IMultiChildNode multiChild) {  
                return multiChild.GetNextNode(blackboard); // Uses Blackboard for conditions  
            }  

            return null;  
        }
        */

        public void TEST()
        {
            TextAdventureNodeX node = GetRootNode();

            if(node == null)
            {
                Debug.Log("[TextAdventureXNodeGraph] Couldn't find a Root Node inside the Graph. Make sure you have a node named Root first.");
                return;
            }

            node.Reset();
            Debug.Log("[TextAdventureXNodeGraph]: " + node.ToString());
        }

    }
}