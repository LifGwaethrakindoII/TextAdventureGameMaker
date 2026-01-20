using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Voidless.TextAdventureMaker
{
	[CustomEditor(typeof(TextAdventureGameConsoleTester))]
	public class TextAdventureGameConsoleTesterInspector : Editor
	{
		private TextAdventureGameConsoleTester tester;
		private TextAdventureNodeXTree tree;

		/// <summary>Sets target property.</summary>
		private void OnEnable()
		{
			tester = target as TextAdventureGameConsoleTester;
		}

		/// <summary>OnInspectorGUI override.</summary>
		public override void OnInspectorGUI()
		{	
			DrawDefaultInspector();
			GUILayout.Space(20);
			if(GUILayout.Button("Test on Text Editor Window")) TestOnTextEditorWindow();		
		}

		private void TestOnTextEditorWindow()
		{
			TextConsoleWindow.CreateTextConsoleWindow();

			tree = new TextAdventureNodeXTree(tester.game.gameData.nodeGraph.GetRootNode() as DialogueNodeX);

			if(tree.current != null) TextConsoleWindow.Log(tree.current.GetContent());
			TextConsoleWindow.AddSubmissionListener(OnTextSubmission);
		}

		private void OnTextSubmission(string _input)
		{
			if(tree.current == null) return;

			TextConsoleWindow.Log("Showing again just to assert: " + _input);
			TextConsoleWindow.Log(tree.current.ToString());
		}
	}
}