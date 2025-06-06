using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
[CreateAssetMenu(menuName = "Text-AdventureMaker / Game Data Asset")]
public class TextAdventureGameData : ScriptableObject
{
    [SerializeField] private TextAdventureXNodeGraph _nodeGraph;
    [SerializeField] private TextParserData _parserData;
    [Space(5f)]
    [Header("Game Settings:")]
    [SerializeField] private float _typingDuration;
    [Space(5f)]
    [Header("Data:")]
    [SerializeField] private StringStringDictionary _stringDictionary;
    [SerializeField] private StringFloatDictionary _floatDictionary;
    [SerializeField] private StringBoolDictionary _boolDictionary;
    [SerializeField] private StringIntDictionary _intDictionary;
    private DialogueNode _rootNode;

    /// <summary>Gets nodeGraph property.</summary>
    public TextAdventureXNodeGraph nodeGraph { get { return _nodeGraph; } }

    /// <summary>Gets parserData property.</summary>
    public TextParserData parserData { get { return _parserData; } }

    /// <summary>Gets typingDuration property.</summary>
    public float typingDuration { get { return _typingDuration; } }

    /// <summary>Gets and Sets rootNode property.</summary>
    public DialogueNode rootNode
    {
        get { return _rootNode; }
        protected set { _rootNode = value; }
    }

    /// <summary>Gets stringDictionary property.</summary>
    public StringStringDictionary stringDictionary { get { return _stringDictionary; } }

    /// <summary>Gets floatDictionary property.</summary>
    public StringFloatDictionary floatDictionary { get { return _floatDictionary; } }

    /// <summary>Gets boolDictionary property.</summary>
    public StringBoolDictionary boolDictionary { get { return _boolDictionary; } }

    /// <summary>Gets intDictionary property.</summary>
    public StringIntDictionary intDictionary { get { return _intDictionary; } }

    /// <summary>Initializes Data.</summary>
    public void Initialize()
    {
        parserData.Initialize();
    }

    [Button("Generate JSON")]
    public void GenerateGraphJSON()
    {
        if(nodeGraph == null) return;
    
        rootNode = nodeGraph.GenerateDialogueNodeGraph();
        string JSON = VJSONSerializer.SerializeToJSON<DialogueNode>(rootNode, VString.GetProjectPath() + " / Resources / Dialogue Graphs  / X", true);

        Debug.Log("Generated JSON: " + JSON);
    }

    [Button("Test Stuff")]
    private void TEST_STUFF()
    {
        
    }
}
}