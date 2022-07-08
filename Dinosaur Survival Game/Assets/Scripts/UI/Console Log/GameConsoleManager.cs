using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class GameConsoleManager : MonoBehaviour
{
    [Header("Commands")]
    private static Dictionary<string, Command> commands = new Dictionary<string, Command>();
    [SerializeField] CommandHelper commandHelper;

    [Header("Console References")]
    [SerializeField] GameObject messagePrefab;
    [SerializeField] Transform messagesParant;
    [SerializeField] MessageInput consoleInput;

    [Header("Console Parameters")]
    [SerializeField] int maxMessegasSize;
    private List<string> consoleMessages = new List<string>();

    [Header("References")]
    private InputManager inputManager;
    public delegate void OnCommandCalled(string commandEventInput);


    public struct Command
    {
        public string commandCallInput;
        public OnCommandCalled commandEventCall;

        public Command(string _commandCallInput, OnCommandCalled eventMethod)
        {
            this.commandCallInput = _commandCallInput;
            commandEventCall = eventMethod;
        }
    }
    
    private void Awake()
    {
        SubscribeToPlayerInput();
        AddCommands();
    }

    private void OnToggleConsole(InputAction.CallbackContext contex)
    {
        // set the console to on/off based on it's current state
        SetConsole(!consoleInput.GetInputField().IsInteractable());
    }

    private void SetConsole(bool active)
    {
        // update the console text box 
        consoleInput.GetInputField().interactable = active;
        // auto selects the text box when activating the console
        if (active) SelectTheConsoleInputField();
        // updates the input
        SetInputMap(active);
    }

    private void SetInputMap(bool isConsoleInteractable)
    {
        inputManager.SetInputActionMap(inputManager.playerInputActions.Camera, !isConsoleInteractable);
        inputManager.SetInputActionMap(inputManager.playerInputActions.Movement, !isConsoleInteractable);
        inputManager.SetInputActionMap(inputManager.playerInputActions.Combat, !isConsoleInteractable);
    }

    private void SelectTheConsoleInputField()
    {
        // auto select the console input field to start typing easily
        if (consoleInput.GetInputField() == null) { return; }
        EventSystem.current.SetSelectedGameObject(consoleInput.GetInputField().gameObject);
        consoleInput.GetInputField().ActivateInputField();
    }

    private void SubscribeToPlayerInput()
    {
        inputManager = InputManager.instance;
        inputManager.playerInputActions.UI.ToggleConsole.performed += OnToggleConsole;
        inputManager.playerInputActions.UI.Cancel.performed += CloseConsole;
    }

    private void CloseConsole(InputAction.CallbackContext obj)
    {
        SetConsole(false);
    }

    private void AddCommands()
    {
        AddCommand("SetTime", commandHelper.SetTime);
        AddCommand("SetHudVisible", commandHelper.SetHudVisible);
    }
    
    public void AddToConsoleMessegas(string stringToAdd)
    {
        if (stringToAdd == string.Empty) { return; }
        CreateNewMessage(stringToAdd);
        SelectTheConsoleInputField();
        DetectCommand(stringToAdd);
        HandleMessagesMaxSize();
    }
    
    private void DetectCommand(string consoleString)
    {
        string[] words = consoleString.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (words.Length < 1) { return; }
        string firstWord = words[0];


        foreach (var item in commands)
        {
            if (item.Key.ToLower() == firstWord.ToLower())
            {
                if (words.Length > 1)
                {
                    string secondWord = words[1];
                    Debug.Log($"Command {item.Key} has found");
                    item.Value.commandEventCall.Invoke(secondWord);
                }
                else AddToConsoleMessegas("Not A Valid Command");
            }
        }
    }

    private void HandleMessagesMaxSize()
    {
        if (consoleMessages.Count < maxMessegasSize) { return; }

        // removes the first messege to free up visual space for the new one
        while (consoleMessages.Count >= maxMessegasSize)
        {
            DestroyImmediate(messagesParant.transform.GetChild(0).gameObject);
            consoleMessages.RemoveAt(0);
        }
    }
    
    private void CreateNewMessage(string stringToAdd)
    {
        // instanciate a new gmaeobject for the messege we want to add
        GameObject messegeInstance = Instantiate(messagePrefab, Vector3.zero, Quaternion.identity, messagesParant);
        messegeInstance.name = messegeInstance.GetComponent<TextMeshProUGUI>().text = stringToAdd;
        consoleMessages.Add(stringToAdd);
    }
    
    private void AddCommand(string commandInput, OnCommandCalled commandEventMethod)
    {
        Command newCommand = new Command(commandInput, commandEventMethod);
        if (!commands.ContainsKey(commandInput))
            commands.Add(commandInput, newCommand);
    }
}
