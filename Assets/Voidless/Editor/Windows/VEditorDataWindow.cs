using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Voidless
{
public class VEditorDataWindow : EditorWindow
{
	public static VEditorDataWindow voidlessEditorDataWindow;
	private static SerializedProperty editorDictionary;

	/// <summary>Creates a new VEditorDataWindow window.</summary>
	/// <returns>Created VEditorDataWindow window.</summary>
	[MenuItem("Voidless/Editor's Data")]
	public static VEditorDataWindow CreateVEditorDataWindow()
	{
		voidlessEditorDataWindow = GetWindow<VEditorDataWindow>("Editor's Data");
		return voidlessEditorDataWindow;
	}

	/// <summary>Use OnGUI to draw all the controls of your window.</summary>
	private void OnGUI()
	{
		VEditorData.ShowDictionary();
	}
}
}