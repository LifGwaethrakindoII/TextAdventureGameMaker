using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Voidless.TextAdventureMaker
{
[CustomEditor(typeof(TextAdventureGame))]
public class TextAdventureGameInspector : Editor
{
	private TextAdventureGame textAdventureGame; 	/// <summary>Inspector's Target.</summary>

	/// <summary>Sets target property.</summary>
	private void OnEnable()
	{
		textAdventureGame = target as TextAdventureGame;
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

		DialogueNodeX node = textAdventureGame.gameData.nodeGraph.GetRootNode() as DialogueNodeX;

		if(node != null) TextConsoleWindow.Log(node.dialogue);
		TextConsoleWindow.AddSubmissionListener(OnTextSubmission);
	}

	private void OnTextSubmission(string _input)
	{
		Debug.Log("[TextAdventureGameInspector] Input Submitted: " + _input);
	}
}
}