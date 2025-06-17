using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/*===========================================================================
**
** Class:  TextConsoleWindow
**
** Purpose: This window class pretends to act as a simple text console.
** It was made with the intention of testing Text-Adventure Maker's project,
** though it may be used for other intended text interactions with a console.
**
** Author: Lîf Gwaethrakindo
**
===========================================================================*/
namespace Voidless
{
/// <summary>Event invoked when an input is being submitted.</summary>
/// <param name="input">Input submitted.</param>
public delegate void OnTextInputEvent(string input);

public class TextConsoleWindow : EditorWindow
{
	public const string PREFIX_OUTPUT = "Out: ";
	public const string PREFIX_INPUT = "In: ";

	public static TextConsoleWindow textConsoleWindow;
	private static StringBuilder outputBuilder;
    private static Queue<string> typingQueue;
	private static Action<string> OnInputSubmitted;
    private static string currentInput = "";
    private static Vector2 scrollPosition;
    private static bool typing;
    private static float typingCooldown = 0.05f;
    private static float lastUpdateTime;
	private static float deltaTime;
	private static float typingTime;
    private static string typingText;
    private static int typingIndex;
	private static bool enabled;
	private static OnTextInputEvent onTextInputSubmitted;
	private static OnTextInputEvent onTextInputOutputted;

	/// <summary>Creates a new TextConsoleWindow window.</summary>
	/// <param name="_welcomeMessage">Create a welcome message? false by default.</param>
	/// <returns>Created TextConsoleWindow window.</summary>
	[MenuItem("Voidless / Editor / Text Console")]
	public static TextConsoleWindow CreateTextConsoleWindow()
	{
		textConsoleWindow = GetWindow<TextConsoleWindow>("Text Console");
		if(outputBuilder == null) outputBuilder = new StringBuilder();
		else outputBuilder.Clear();
		typingQueue = new Queue<string>();

        Log("Hello World. This is a simple Text Console Window.");
		Log("Please consult the implementation (TextConsoleWindow.cs) in order to create your own text interactions.");
		Log("Happy coding.");

        return textConsoleWindow;
	}

#region Private:
	/// <summary>Callback invoked when TextConsoleWindow's instance is enabled.</summary>
	private void OnEnable()
	{
		lastUpdateTime = (float)EditorApplication.timeSinceStartup;
		EditorApplication.update += OnUpdate;
		onTextInputSubmitted = null;
		onTextInputOutputted = null;
		enabled = true;
	}

	/// <summary>Callback invoked when TextConsoleWindow's instance is disabled.</summary>
	private void OnDisable()
	{
		EditorApplication.update -= OnUpdate;
		onTextInputSubmitted = null;
		onTextInputOutputted = null;
		enabled = false;
	}

	/// <summary>Use OnGUI to draw all the controls of your window.</summary>
	private void OnGUI()
	{
		GUILayout.BeginVertical();
        
        // Main console view
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(position.height - 30));
        GUILayout.Label(outputBuilder.ToString());
        GUILayout.EndScrollView();

        // Input region
        GUILayout.BeginHorizontal();
        currentInput = GUILayout.TextField(currentInput, GUILayout.ExpandWidth(true));
        if(GUILayout.Button("Submit", GUILayout.Width(80))) SubmitInput();
        if(GUILayout.Button("Clear", GUILayout.Width(80))) Clear();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
	}

	/// <summary>Callback invoked at each Editor's update.</summary>
	private void OnUpdate()
	{
		float currentTime = (float)EditorApplication.timeSinceStartup;
		deltaTime = currentTime - lastUpdateTime;
		lastUpdateTime = currentTime;

		switch(typing)
		{
			case true:
				if(string.IsNullOrEmpty(typingText) && typingQueue.Count > 0)
				{
					typingText = typingQueue.Dequeue();
					typingIndex = 0;
					typingTime = 0.0f;
				}

				if(typingTime >= typingCooldown)
				{
					outputBuilder.Append(typingText[typingIndex]);
					typingIndex++;
					typingTime = 0.0f;

					if(typingIndex >= typingText.Length)
					{
						outputBuilder.AppendLine();
						typingText = null;

                        if(typingQueue.Count == 0) typing = false;
                    }
				}
				else typingTime += deltaTime;
			break;

			case false:
			break;
		}

		GUI.enabled = !typing;
		Repaint();
	}

	/// <summary>Submits Input into the console.</summary>
    private void SubmitInput()
    {
		if(typing || string.IsNullOrEmpty(currentInput)) return;

        outputBuilder.Append("\n");
        outputBuilder.Append(PREFIX_INPUT);
        outputBuilder.AppendLine(currentInput);
        outputBuilder.Append("\n");

		if(OnInputSubmitted != null) OnInputSubmitted(currentInput);
		if(onTextInputSubmitted != null) onTextInputSubmitted(currentInput);

        currentInput = string.Empty;
    }
#endregion

#region Public:
    /// <summary>Sets value for typing bool.</summary>
    /// <param name="_enable">Enable typing? true by default.</param>
    public static void EnableTyping(bool _enable = true)
    {
    	typing = _enable;
    }

    /// <summary>Logs text into console.</summary>
    /// <param name="text">Text to log, it will be enqueued.</param>
    public static void Log(string text, Action<string> onInputSubmitted = null)
    {
    	if(!enabled) CreateTextConsoleWindow();

    	text = PREFIX_OUTPUT + text;

    	typingQueue.Enqueue(text);
    	typing = true;
		OnInputSubmitted = onInputSubmitted;
    }

    /// <summary>Clears console and other cache data.</summary>
    public static void Clear()
    {
    	outputBuilder.Clear();
    	typing = false;
    	typingQueue.Clear();
    	currentInput = string.Empty;
    }

    /// <summary>Add submission event listener.</summary>
    /// <param name="_onTextSubmitted">Submission event to add.</param>
    public static void AddSubmissionListener(OnTextInputEvent _onTextInputSubmitted)
    {
    	onTextInputSubmitted += _onTextInputSubmitted;
    }

    /// <summary>Removes submission event listener.</summary>
    /// <param name="_onTextSubmitted">Submission event to remove.</param>
    public static void RemoveSubmissionListener(OnTextInputEvent _onTextInputSubmitted)
    {
    	onTextInputSubmitted -= _onTextInputSubmitted;
    }

    /// <summary>Add output event listener.</summary>
    /// <param name="_onTextSubmitted">Output event to add.</param>
    public static void AddOutputListener(OnTextInputEvent _onTextInputOutputted)
    {
    	onTextInputOutputted += _onTextInputOutputted;
    }

    /// <summary>Removes output event listener.</summary>
    /// <param name="_onTextOutputted">Output event to remove.</param>
    public static void RemoveOutputListener(OnTextInputEvent _onTextInputOutputted)
    {
    	onTextInputOutputted -= _onTextInputOutputted;
    }
#endregion
}
}