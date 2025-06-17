using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

/// <summary>Event invoked when this Input Field enters a message.</summary>
/// <param name="_message">Message sent.</param>
public delegate void OnMessageSent(string _message);

namespace Voidless.TextAdventureMaker
{
[RequireComponent(typeof(TMP_InputField))]
public class CommandLineInterface : MonoBehaviour
{
    public const float INTERVAL_BLINK = 0.5f;

    public event OnMessageSent onMessageSent;

    [SerializeField] private InputActionReference _enterAction;
    private TMP_InputField _inputField;
    private bool _receiveInput;
    private Coroutine blinkingCoroutine;

    /// <summary>Gets enterAction property.</summary>
    public InputActionReference enterAction { get { return _enterAction; } }

    /// <summary>Gets inputField property./// </summary>
    public TMP_InputField inputField
    {
        get
        {
            if (_inputField == null) _inputField = GetComponent<TMP_InputField>();
            return _inputField;
        }
    }

    /// <summary>Get and Sets receiveInput property.</summary>
    public bool receiveInput
    {
        get { return _receiveInput; }
        set { _receiveInput = value; }
    }

    /// <summary>Callback invoked when this is instanciated.</summary>
    private void Awake()
    {
        Activate(false);
        if(enterAction != null) enterAction.action.performed += OnEnterActionPerformed;
    }

    /// <summary>Activates/Deactivates CommandLineInterface.</summary>
    /// <param name="_activate">Activate? true by default.</param>
    public void Activate(bool _activate = true)
    {
        /*/// Leave if there is no flag change:
        if(_activate == receiveInput) return;*/

        receiveInput = _activate;

        switch (_activate)
        {
            case true:
                inputField.interactable = true;
                inputField.Select();
                inputField.ActivateInputField();
                //this.StartCoroutine(BlinkingCoroutine(), ref blinkingCoroutine);
            break;

            case false:
                inputField.interactable = false;
                //this.DispatchCoroutine(ref blinkingCoroutine);
            break;
        }
    }

    /// <summary>Sends Message and invokes callback.</summary>
    /// <param name="_empty">Empty the InputField? true by default</param>
    public void SendMessage(bool _empty = true)
    {
        if(onMessageSent != null) onMessageSent(inputField.text);
        if(_empty) inputField.text = string.Empty;
    }

    /// <summary>Callback invoked when the Enter-Action is performed.</summary>
    /// <param name="_contect">Callback's Context.</param>
    private void OnEnterActionPerformed(CallbackContext _context)
    {
        Debug.Log("[CommandLineInterface] Enter-Action received.");
        SendMessage();
    }

    /// <summary>Cursor Blinking's Coroutine.</summary>
    private IEnumerator BlinkingCoroutine()
    {
        float t = 0.0f;

        while(receiveInput)
        {
            inputField.text += "|";

            while(t < INTERVAL_BLINK)
            {
                t += Time.deltaTime;
                yield return null;
            }

            t = 0.0f;
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);

            while(t < INTERVAL_BLINK)
            {
                t += Time.deltaTime;
                yield return null;
            }
            {
                t = 0.0f;
            }

            t = 0.0f;
        }
    }
}
}