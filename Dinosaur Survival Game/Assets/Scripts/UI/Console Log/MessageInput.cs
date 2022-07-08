using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageInput : MonoBehaviour
{
    [SerializeField] GameConsoleManager consoleManager;
    private TMP_InputField inputField;

    public TMP_InputField GetInputField() { return inputField; }

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void SendConsoleMessage()
    {
        consoleManager.AddToConsoleMessegas(inputField.text);
        inputField.text = string.Empty;
    }
}
