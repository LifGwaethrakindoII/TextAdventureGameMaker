using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===========================================================================
**
** Class:  TextAdventureSequencerNode
**
** Purpose: X-Node that acts as a Sequencer (follows the flow until a child
** fails).
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
public class TextAdventureSequencerNodeX : TextAdventureCompositeNodeX
{
    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Sequencer; }
}
}