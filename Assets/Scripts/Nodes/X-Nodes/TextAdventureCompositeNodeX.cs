using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNode;
using Voidless.XNode;

/*===========================================================================
**
** Class: TextAdventureCompositeNodeX
**
** Purpose: X-Node acting as a CompositeNode.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
    [Serializable]
    public abstract class TextAdventureCompositeNodeX : TextAdventureNodeX
    {
        [Output] public TextAdventureNodeX[] children;

        /// <returns>Node Type.</returns>
        public override NodeType GetNodeType() { return NodeType.Undefined; }

        /// <summary>GetValue should be overridden to return a value for any specified output port.</summary>
        public override object GetValue(NodePort port)
        {
            switch(port.fieldName)
            {
                case "children":    return children;
                default:            return base.GetValue(port);
            }
        }

        /// <returns>Children connected to this X-Node.</returns>
        public TextAdventureNodeX[] GetChildren()
        {
            if(children == null || children.Length == 0) children = this.GetOutputNodes<TextAdventureNodeX>("children");

            return children;
        }

        /// <summary>Resets Node's port references.</summary>
        public override void Reset()
        {
            base.Reset();
            children = null;
        }

        /// <returns>String representing this X-Node.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(base.ToString());
            builder.Append("{ Has Children? ");
            builder.Append(GetChildren() != null);
            
            if(children != null)
            {
                builder.Append("Children Count: ");
                builder.Append(children.Length);
            }

            builder.Append(" }");

            return builder.ToString();
        }

        /// <returns>Children nodes.</returns>
        public override IEnumerable<TextAdventureNodeX> IterateThroughChildren()
        {
            if(next != null) yield return next;

            if(GetChildren() != null)
            {
                foreach(TextAdventureNodeX child in GetChildren())
                {
                    yield return child;
                }
            }
        }
    }
}