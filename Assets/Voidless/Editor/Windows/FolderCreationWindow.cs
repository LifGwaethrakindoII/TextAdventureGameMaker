using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Voidless
{
public enum EssentialFolders
{
	Animations,
	Animator_Controllers,
	Audios,
	Editor,
	Editor_Default_Resources,
	Images,
	Gizmos,
	Materials,
	Models,
	Music,
	Plugins,
	Resources,
	Scenes,
	Scripts,
	Shaders,
	Sounds,
	StreamingAssets,
	Textures 
}

public class FolderCreationWindow : EditorWindow
{
	private const string ICON_FOLDER = "Folder Icon";
	private const float SPACE = 20.0f;
	private const float WIDTH_BUTTON = 50.0f;
	private const int MAX_STRING_SIZE = 24;

	public static FolderCreationWindow folderCreationWindow;
	private static List<string> unexistingFolders;
	private static List<string> existingFolders;
	private static Vector3 scrollPosition;
	private static string dataPath;

	/// <summary>Creates a new FolderCreationWindow window.</summary>
	/// <returns>Created FolderCreationWindow window.</summary>
	[MenuItem("Voidless/Folder Creation Menu")]
	public static FolderCreationWindow CreateFolderCreationWindow()
	{
		unexistingFolders = null;
		existingFolders = null;
		dataPath = Application.dataPath + "/";
		folderCreationWindow = GetWindow<FolderCreationWindow>("Folder Creation:");
		EvaluateDirectories();
		return folderCreationWindow;
	}

	private void OnGUI()
	{
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
		GUILayout.Space(SPACE);
		ShowExistingFolders();
		ShowUnexistingFolders();
		EditorGUILayout.EndScrollView();
	}

	/// <summary>Shows a List of the already-existing folders.</summary>
	private void ShowExistingFolders()
	{
		GUILayout.Label("Existing Folders");
		GUILayout.Space(SPACE);
		foreach(string folder in existingFolders)
		{
			GUILayout.Label(folder.SnakeCaseToSpacedText());
		}
		GUILayout.Space(SPACE);
	}

	/// <summary>Shows the essential folders that haven't yet been created.</summary>
	private void ShowUnexistingFolders()
	{
		GUILayout.Label("Non-Existing Folders");
		GUILayout.Space(SPACE);
		foreach(string folder in unexistingFolders)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(folder.SnakeCaseToSpacedText());
			if(GUILayout.Button("Create",  GUILayout.Width(WIDTH_BUTTON)))
			{
				CreateFolder(folder);
				break;
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.Space(SPACE);
	}

	/// <summary>Created a Folder.</summary>
	/// <param name="_folder">Folder's Name.</param>
	private static void CreateFolder(string _folder)
	{
		Directory.CreateDirectory(dataPath + _folder.SnakeCaseToSpacedText());
		unexistingFolders.Remove(_folder);
		existingFolders.Add(_folder);
		AssetDatabase.Refresh();
	}

	/// <summary>Evaluates all directories to update the existing and non-existing lists.</summary>
	private static void EvaluateDirectories()
	{
		int enumCount = Enum.GetNames(typeof(EssentialFolders)).Length;

		if(unexistingFolders == null || existingFolders == null)
		{
			unexistingFolders = new List<string>();
			existingFolders = new List<string>();

			for(int i = 0; i < enumCount; i++)
			{
				EssentialFolders folder = (EssentialFolders)(i);
				string currentFolder = folder.ToString();
				string spacedFolder = currentFolder.SnakeCaseToSpacedText();

				if(!Directory.Exists(dataPath + spacedFolder)) unexistingFolders.Add(spacedFolder);
				else existingFolders.Add(spacedFolder);
			}
		}
		else
		{
			foreach(string folder in unexistingFolders)
			{
				if(Directory.Exists(dataPath + folder))
				{
					unexistingFolders.Remove(folder);
					existingFolders.Add(folder);
					break;
				}
			}
		}
	}
}
}