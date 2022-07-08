using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour
{
    [Header("References")]
    // singelton reference for the instance
    public static PlayerInventoryManager instance;
    // the container that the player inventory will use
    [SerializeField] Container mainContainer;
    // the point in which the items that droped from the contianer are droped from
    [SerializeField] Transform dropPointTransform;
    // reference for the input manager
    private InputManager inputManager;
    public Container GetMainContainer() { return mainContainer; }

    [Header("Parameters")]
    // the streangth we add to the rigidbody of the items that we drop from the containe
    [SerializeField] float throwStreangth = 10f;



    private void Awake()
    {
        SetSingelton();
        SubbscribeToInput();
    }

    private void SetSingelton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// this method subbscribes to the input of the inventory
    /// </summary>
    private void SubbscribeToInput()
    {
        inputManager = InputManager.instance;
        // subscribes the ToggleInventory to the toggle inventory input
        inputManager.playerInputActions.UI.ToggleInventory.performed += ToggleInventory;
        // subscribes the CloseInventory to the cancel input
        inputManager.playerInputActions.UI.Cancel.performed += CloseInventory;
        // subscribes the DropItem to the drop item input
        inputManager.playerInputActions.UI.DropItem.performed += HandleDropItem;
    }

    /// <summary>
    /// this method is called when we press the cancel input and it is used to always close the container
    /// when we press the cancel input (not like the toggle inventory that toggles the container on and off)
    /// </summary>
    private void CloseInventory(InputAction.CallbackContext obj)
    {
        // closes the container if the coneainer is open
        if (mainContainer.IsOpen()) mainContainer.CloseContainer();
        // set the input maps based on the container
        SetInputMaps();
    }

    /// <summary>
    /// this method is called when we press the toggle inventory input and it is used to turn the container
    /// on and off based on the container's open state (if it is on turn it off and vice versa)
    /// </summary>
    private void ToggleInventory(InputAction.CallbackContext contex)
    {
        // if the container is open close it
        if (mainContainer.IsOpen())
            mainContainer.CloseContainer();
        // if the container is not open open it
        else
            mainContainer.OpenContainer();
        
        // set the input maps based on the container's open state
        SetInputMaps();
    }

    /// <summary>
    /// this method sets the input action maps of the camera, combat and interaction to be disabled when
    /// the container is opened and enabled when the container is closed so that we can have easy controll when browsing 
    /// the inventory
    /// </summary>
    private void SetInputMaps()
    {
        // get reference for the open state of the main container
        bool isContainerOpen = mainContainer.IsOpen();
        // set the camera input based on teh container open state
        inputManager.SetInputActionMap(inputManager.playerInputActions.Camera, !isContainerOpen);
        // set the combat input based on teh container open state
        inputManager.SetInputActionMap(inputManager.playerInputActions.Combat, !isContainerOpen);
        // set the interaction input based on teh container open state
        inputManager.SetInputActionMap(inputManager.playerInputActions.Interaction, !isContainerOpen);
    }

    /// <summary>
    /// this method is called when we press the drop item input and get the slot that we pressed on
    /// </summary>
    private void HandleDropItem(InputAction.CallbackContext contex)
    {
        // if the container is not open we can't drop items from it
        if (!mainContainer.IsOpen()) { return; }
        // getting the slot under the mouse
        Slot slotUnderMouse = HudManager.GetSlotUnderMouse();
        // if there is a slot under the mouse drop the item from the slot
        if (slotUnderMouse == null) { return; }
        HudManager.GetSlotUnderMouse().DropItem(dropPointTransform, throwStreangth);
    }


}
