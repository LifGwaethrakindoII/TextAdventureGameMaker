using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public class TextAdventureGameUI : MonoBehaviour
{
    [SerializeField] private RectTransform _scrollContent;
    [SerializeField] private CommandLineInterface _commandLineInterface;

    /// <summary>Gets scrollContent property.</summary>
    public RectTransform scrollContent { get { return _scrollContent; } }

    /// <summary>Gets commandLineInterface property.</summary>
    public CommandLineInterface commandLineInterface { get { return _commandLineInterface; } }

    /// <summary>Adds TextMesh to UI.</summary>
    /// <param name="_textMesh">TextMesh to add to UI</param>
    public void AddTextMesh(PoolTextMeshProUGUI _poolTextMesh)
    {
        RectTransform textRectTransform = _poolTextMesh.textMesh.rectTransform;

        textRectTransform.ParentWith(scrollContent);
        textRectTransform.SetSiblingIndex(commandLineInterface.transform.GetSiblingIndex());
    }
}
}