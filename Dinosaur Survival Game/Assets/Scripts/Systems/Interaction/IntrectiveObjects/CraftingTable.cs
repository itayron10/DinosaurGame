using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
    [Header("References")]
    [Tooltip("This canvas will enable when player interacts with this crafting table")]
    [SerializeField] Canvas craftingCanvas;
    private InputManager inputManager; // reference for the input manager

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        // set the input manager
        inputManager = InputManager.instance;
        inputManager.playerInputActions.UI.Cancel.performed += Cancel_performed;
        // disable the canvas on awake
        SetCraftingCanvas(false);
    }

    private void Cancel_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // disable the canvas when the player press on the cancel button
        SetCraftingCanvas(false);
    }

    public override void Interacte()
    {
        base.Interacte();
        SetCraftingCanvas(craftingCanvas ? !craftingCanvas.enabled : false);
    }

    private void SetCraftingCanvas(bool active)
    {
        if (!craftingCanvas) { return; }
        // turn off/on the craftingCanvas
        craftingCanvas.enabled = active;
        // set the input based on the craftingCanvas
        SetCraftingMenuInput(active);
    }

    /// <summary>
    /// this method sets the input actions based on the craftin canvas enabled
    /// </summary>
    private void SetCraftingMenuInput(bool isCraftingCanvasEnabled)
    {
        // sets the camera action map enabled/disabled based on if the crafting canvas is enabled/disabled
        inputManager.SetInputActionMap(inputManager.playerInputActions.Camera, !isCraftingCanvasEnabled);
        // sets the movement action map enabled/disabled based on if the crafting canvas is enabled/disabled
        inputManager.SetInputActionMap(inputManager.playerInputActions.Movement, !isCraftingCanvasEnabled);
        // sets the ui action map enabled/disabled based on if the crafting canvas is enabled/disabled
        inputManager.SetInputActionMap(inputManager.playerInputActions.UI, !isCraftingCanvasEnabled);
        // sets the combat action map enabled/disabled based on if the crafting canvas is enabled/disabled
        inputManager.SetInputActionMap(inputManager.playerInputActions.Combat, !isCraftingCanvasEnabled);
    }
}
