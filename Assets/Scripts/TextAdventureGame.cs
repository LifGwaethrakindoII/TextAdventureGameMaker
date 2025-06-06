using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Voidless.TextAdventureMaker
{
public enum GameState
{
    None,
    AIWriting,
    PlayerWriting
}

public class TextAdventureGame : Singleton<TextAdventureGame>, IFiniteStateMachine<GameState>
{
    [Space(5f)]
    [Header("Text-Adventure Game's Attributes:")]
    [SerializeField] private TextAdventureGameData _gameData;
    [SerializeField] private TextAdventureGameUI _gameUI;
    [SerializeField] private GameObjectPool<PoolTextMeshProUGUI> _textMeshPool;
    [SerializeField] private bool _debug;
    private GameState _state;
    private GameState _previousState;
    private StringStringDictionary _stringDictionary;
    private StringFloatDictionary _floatDictionary;
    private StringBoolDictionary _boolDictionary;
    private StringIntDictionary _intDictionary;

    /// <summary>Gets and Sets gameData property.</summary>
    public TextAdventureGameData gameData
    {
        get { return _gameData; }
        set { _gameData = value; }
    }

    /// <summary>Gets and Sets gameUI property.</summary>
    public TextAdventureGameUI gameUI
    {
        get { return _gameUI; }
        set { _gameUI = value; }
    }

    /// <summary>Gets and Sets textMeshPool property.</summary>
    public GameObjectPool<PoolTextMeshProUGUI> textMeshPool
    {
        get { return _textMeshPool; }
        set { _textMeshPool = value; }
    }

    /// <summary>Gets and Sets debug property.</summary>
    public bool debug
    {
        get { return _debug; }
        set { _debug = value; }
    }

    /// <summary>Gets and Sets state property.</summary>
    public GameState state
    {
        get { return _state; }
        set { _state = value; }
    }

    /// <summary>Gets and Sets previousState property.</summary>
    public GameState previousState
    {
        get { return _previousState; }
        set { _previousState = value; }
    }

    /// <summary>Gets and Sets stringDictionary property.</summary>
    public static StringStringDictionary stringDictionary
    {
        get { return Instance._stringDictionary; }
        private set { Instance._stringDictionary = value; }
    }

    /// <summary>Gets and Sets floatDictionary property.</summary>
    public static StringFloatDictionary floatDictionary
    {
        get { return Instance._floatDictionary; }
        private set { Instance._floatDictionary = value; }
    }

    /// <summary>Gets and Sets boolDictionary property.</summary>
    public static StringBoolDictionary boolDictionary
    {
        get { return Instance._boolDictionary; }
        private set { Instance._boolDictionary = value; }
    }

    /// <summary>Gets and Sets intDictionary property.</summary>
    public static StringIntDictionary intDictionary
    {
        get { return Instance._intDictionary; }
        private set { Instance._intDictionary = value; }
    }

    /// <summary>Called internally after Awake.</summary>
    protected override void OnAwake()
    {
        base.OnAwake();
        if(gameData != null)
        {
            gameData.Initialize();

            stringDictionary = new StringStringDictionary();
            floatDictionary = new StringFloatDictionary();
            boolDictionary = new StringBoolDictionary();
            intDictionary = new StringIntDictionary();
            stringDictionary.CopyFrom(gameData.stringDictionary);
            floatDictionary.CopyFrom(gameData.floatDictionary);
            boolDictionary.CopyFrom(gameData.boolDictionary);
            intDictionary.CopyFrom(gameData.intDictionary);
        }
        textMeshPool.Initialize();
    }

    /// <summary>Callback invoked after the scene loads.</summary>
    private void Start()
    {
        if (Instance.gameUI != null) Instance.gameUI.commandLineInterface.onMessageSent += OnMessageSent;
    }

    /// <summary>Interprets player's input into a command.</summary>
    /// <param name="_command">Player's input.</param>
    public static void ParseCommand(string _command)
    {
        if(Instance.gameData == null)
        {
            DebugMessage("[TextAdventureGame] Game Data not assigned.", LogType.Error);
            return;
        }

        CreateTextMesh(_command);

        string[] words = _command.ToLower().Split(' '); /// Separate the input sentence into separate words.
        StringWordCategoryDictionary wordCategoryMap = Instance.gameData.parserData.wordCategoryMap;
        WordCategoryStringListDictionary dataWords = Instance.gameData.parserData.wordsMap;
        WordCategoryStringListDictionary categoryWords = new WordCategoryStringListDictionary();

        DebugMessage("[TextAdventureGame] Words have been split into: " + words.CollectionToString(false));

        /// Match words with their respective categories:
        foreach(string word in words)
        {
            if(wordCategoryMap.ContainsKey(word))
            {
                WordCategory category = wordCategoryMap[word];
                if(!categoryWords.ContainsKey(category)) categoryWords.Add(category, new List<string>());
                categoryWords[category].Add(word);
            }
        }

        /// Match categories to GameObjects:
        /*if (categorizedWords.ContainsKey(WordCategory.Verb) && categorizedWords.ContainsKey(WordCategory.Noun))
        {
            List<string> verbs = categorizedWords[WordCategory.Verb];
            List<string> nouns = categorizedWords[WordCategory.Noun];
            foreach (var verb in verbs)
            {
                foreach (var noun in nouns)
                {
                    if (objectNames.ContainsKey(noun))
                    {
                        GameObject gameObject = objectNames[noun];
                        switch (gameObject)
                        {
                            case GameObject.Door:
                                OpenDoor();
                                break;
                            // Add cases for other game objects
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid command.");
        }*/
    }

    /// <summary>Creates Pool-TextMesh with given message.</summary>
    /// <param name="_message">Message to assign to recycled Pool-TextMesh</param>
    /// <param name="_color">Optional Pool-TextMesh's color [UnambiguousColor.White by default]</param>
    /// <returns>Recycled Pool-TextMesh, already introduced into UI's Layout.</returns>
    public static PoolTextMeshProUGUI CreateTextMesh(string _message, UnambiguousColor _color = UnambiguousColor.White)
    {
        PoolTextMeshProUGUI poolTextMesh = Instance.textMeshPool.Recycle(Vector3.zero, Quaternion.identity);

        if (poolTextMesh == null) return null;

        Color color = VColor.GetUnambiguousColor(_color);

        poolTextMesh.textMesh.color = color;
        poolTextMesh.textMesh.text = _message;

        if(Instance.gameUI != null) Instance.gameUI.AddTextMesh(poolTextMesh);

        return poolTextMesh;
    }

    /// <summary>Debugs message into the game console.</summary>
    /// <param name="_message">Message to debug.</param>
    /// <param name="_logType">Log Type [LogType.Log by default].</param>
    public static void DebugMessage(string _message, LogType _logType = LogType.Log)
    {
        if(!Application.isPlaying)
        {
            VDebug.Log(_logType, _message);
            return;
        }
        if(!Instance.debug) return;

        UnambiguousColor color = UnambiguousColor.Transparent;

        switch(_logType)
        {
            case LogType.Assert:
            case LogType.Log:
                color = UnambiguousColor.Grey;
            break;

            case LogType.Warning:
                color = UnambiguousColor.Yellow;
            break;

            case LogType.Error:
            case LogType.Exception:
                color = UnambiguousColor.Red;
            break;
        }

        CreateTextMesh(_message, color);
    }

    /// <summary>Callback invoked when entering state.</summary>
    /// <param name="_state">State entered.</param>
    public void OnEnterState(GameState _state)
    {
        switch (_state)
        {
            case GameState.AIWriting:
                Instance.gameUI.commandLineInterface.Activate(false);
            break;

            case GameState.PlayerWriting:
                Instance.gameUI.commandLineInterface.Activate(true);
            break;
        }

        DebugMessage("Entered State: " + _state.ToString());
    }

    /// <summary>Callback invoked when exiting state.</summary>
    /// <param name="_state">State exited.</param>
    public void OnExitState(GameState _state) { /*...*/ }

    /// <summary>callback invoked then the Command Line Interface has sent a message.</summary>
    /// <param name="_message">Message sent by the Command Line Interface.</param>
    private void OnMessageSent(string _message)
    {
        ParseCommand(_message);
    }

    [Button("TEST Player's Input")]
    /// <summary>Tests Player's Input.</summary>
    /// <param name="_playerInput">Player's Input.</param>
    private void TEST_PlayerInput(string _playerInput)
    {
        ParseCommand(_playerInput);
    }

    [Button("Activate/Deactivate CLI")]
    /// <summary>Activates/Deactivates CLI.</summary>
    /// <param name="_activate">Activate? True by default.</param>
    private void TEST_ActivateCommandLineInterface(bool _activate = true)
    {
        this.ChangeState(_activate ? GameState.PlayerWriting : GameState.AIWriting);
    }

    /// <summary>Typing's Coroutine.</summary>
    /// <param name="_poolTextMesh">TextMesh where the typing will happen.</param>
    /// <param name="_message">Message that is gonna be typed.</param>
    /// <param name="onTypingEnds">Optional callback invoked when the typing routine comes to an end [null by default].</param>
    public static IEnumerator TypingRoutine(PoolTextMeshProUGUI _poolTextMesh, string _message, Action onTypingEnds = null)
    {
        StringBuilder builder = new StringBuilder();
        float d = Instance.gameData.typingDuration;
        float t = 0.0f;

        foreach(char character in _message)
        {
            builder.Append(character);
            _poolTextMesh.textMesh.text = builder.ToString();

            while(t < d)
            {
                t += Time.deltaTime;
                yield return null;
            }

            t = 0.0f;
        }

        if(onTypingEnds != null) onTypingEnds();
    }
}
}