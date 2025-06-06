using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public class TextAdventureSequencerNode : TextAdventureCompositeNode
{
    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Sequencer; }  

    public TextAdventureSequencerNode(TextAdventureNode node) : base(node)
    {}

    /// <summary>Excecutes Node.</summary>
    public override NodeResult Excecute()
    {
        if(children == null || children.Length == 0) return NodeResult.Error;
    
        foreach(TextAdventureNode child in children)
        {
            NodeResult result = child.Excecute();

            switch(result)
            {
                case NodeResult.Failure:
                case NodeResult.Error:      return NodeResult.Failure;
            }
        }

        return NodeResult.Success;
    }
}
}