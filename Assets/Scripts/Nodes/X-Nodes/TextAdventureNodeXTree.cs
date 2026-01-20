using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

/*===========================================================================
**
** Class:  TextAdventureNodeXTree
**
** Purpose: Tree structure that will handle the flow taking a root reference.
**
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless.TextAdventureMaker
{
    public delegate void OnOutput(string _output);

    public class TextAdventureNodeXTree
    {
        public event OnOutput onOutput;

        private TextAdventureNodeX _root;
        private TextAdventureNodeX _current;
        private TextAdventureNodeX _previous;

        /// <summary>Gets and Sets root property.</summary>
        public TextAdventureNodeX root
        {
            get { return _root; }
            private set { _root = value; }
        }

        /// <summary>Gets and Sets current property.</summary>
        public TextAdventureNodeX current
        {
            get { return _current; }
            private set { _current = value; }
        }

        /// <summary>Gets and Sets previous property.</summary>
        public TextAdventureNodeX previous
        {
            get { return _previous; }
            private set { _previous = value; }
        }

        /// <summary>TextAdventureNodeXTree's Constructor.</summary>
        /// <param name="_root">Tree's Root.</param>
        public TextAdventureNodeXTree(TextAdventureNodeX _root)
        {
            root = _root;
            current = root;
            previous = null;
        }

        /// <summary>Sends input into the tree to be processed.</summary>
        /// <param name="_input">Input to process.</param>
        public void SendInput(string _input)
        {

        }

        private void SendOutputEvent(string _output)
        {
            if(onOutput != null) onOutput(_output);
        }

        public string GetContent(TextAdventureNodeX _node)
        {
            if(_node == null) return string.Empty;

            switch(_node.GetNodeType())
            {
                case NodeType.Dialogue:

                break;

                default: return string.Empty;
            }

            return string.Empty;
        }

        /// <returns>Current Node's Content.</returns>
        public string GetCurrentContent()
        {
            return current != null ? current.GetContent() : "EMPTY";
        }

        /// <returns>String representing this Tree.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Root: ");
            builder.AppendLine(root.ToStringEvenIfNull());
            builder.Append("Current: ");
            builder.AppendLine(current.ToStringEvenIfNull());
            builder.Append("Previous: ");
            builder.AppendLine(previous.ToStringEvenIfNull());

            return builder.ToString();
        }
    }
}