using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public class TextAdventureSequencerNodeX : TextAdventureNodeX
{
    /// <returns>Node Type.</returns>
    public override NodeType GetNodeType() { return NodeType.Sequencer; }
}
}