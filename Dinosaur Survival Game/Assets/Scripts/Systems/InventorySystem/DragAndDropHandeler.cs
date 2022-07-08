using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragAndDropHandeler : MonoBehaviour
{
    [Header("References")]
    // a reference for the image that will follow the mouse screen position to emulate icon cursor
    // when we drag items
    [SerializeField] Image followMouseImage;
    [SerializeField] HotBar hotBar;
    // private reference for the input manager used for mouse dragging input
    private InputManager inputManager;
    
    [Header("Dragging References")]
    // private reference for the current item that the player is dragging
    private Item heldItem = null;
    // private reference for the current slot that the player has selected when he started dragging
    private Slot heldItemSlot = null;

    private void Start()
    {
        // when a container in the scene closes we stop dragging so that we won't be dragging when the player closes
        // a container while dragging
        Container.OnContainerClosed += StopDragging;
        SubbscribeToInput();
        // set the image that emulates dragging item icons to false on default
        SetDragFollowImage(false);
    }

    // stop listening to the onContainerClosed when destroyed
    private void OnDestroy() => Container.OnContainerClosed -= StopDragging;

    // updates the followMouseImage to follow the mouse
    private void Update() => followMouseImage.transform.position = Input.mousePosition;

    
    /// <summary>
    /// this method is called on start is used to subbcribe to the select item input 
    /// </summary>
    private void SubbscribeToInput()
    {
        // get the input manager
        inputManager = InputManager.instance;
        // sbbscribe OnMouseButtonPressed to the start action of the select item (when the button started to be pressed)
        inputManager.playerInputActions.UI.SelectItem.started += OnMouseButtonPressed;
        // sbbscribe OnMouseButtonReleased to the end action of the select item (when the button realeased)
        inputManager.playerInputActions.UI.SelectItem.canceled += OnMouseButtonReleased;
    }

    /// <summary>
    /// called when the mouse button is pressed, start dragging when it happens
    /// </summary>
    private void OnMouseButtonPressed(InputAction.CallbackContext contex) => StartDragging();
    /// <summary>
    /// called when the mouse button is released, stop the dragging when it happens
    /// </summary>
    private void OnMouseButtonReleased(InputAction.CallbackContext contex) => StopDragging();

    /// <summary>
    /// this method is called when we stop dragging the item (if the dragg is canceled or it ends)
    /// this method is the one who moves the items between slots when we stop dragging
    /// </summary>
    private void StopDragging()
    {
        // setting the drag follow image to false so that the image is only on when we are dragging 
        SetDragFollowImage(false);
        // get the slot under the mouse
        Slot slotUnderMouse = HudManager.GetSlotUnderMouse();
        // if there is no slot under the mouse reset the dragging settings
        if (slotUnderMouse == null) { SetDraggingReferencesOnDrag(null, false); return; }

        // if there is no item in the slot under the mouse we move the held item to that slot
        if (slotUnderMouse.GetItem() == null)
        {
            // if we are not holding an item we return
            if (heldItem == null) { return; }
            // if we are holding item we add the item to the slot under the mouse
            slotUnderMouse.AddItem(heldItem);
            
            if (hotBar.IsActiveSlot(heldItemSlot.GetComponent<HotBarSlot>()) &&
                !hotBar.IsActiveSlot(slotUnderMouse.GetComponent<HotBarSlot>()))
                heldItem.Unequip();            
            
            // setting the previous slot (before dragging and changing slots) of the item to be empty
            heldItemSlot.SetItem(null);
        }

        // in the end we reset the dragging settings 
        SetDraggingReferencesOnDrag(null, false);
    }

    /// <summary>
    /// this method is called when we press the mouse button in the inventory in order to start dragging an item
    /// </summary>
    private void StartDragging()
    {
        // get the slot under the mouse
        Slot slotUnderMouse = HudManager.GetSlotUnderMouse();
        // if there is not slot under the mouse just return
        if (slotUnderMouse == null) { return; }
        // if there is not item in the slot under the mouse just return
        if (slotUnderMouse.GetItem() == null) { return; }
        // set the dragging references based on the slot under the mouse
        SetDraggingReferencesOnDrag(slotUnderMouse, true);
    }

    /// <summary>
    /// this method is called when we start dragging\stop dragging and it sets the 
    /// dragging variables according to the slot we give it
    /// </summary>
    private void SetDraggingReferencesOnDrag(Slot heldItemSlot, bool draggingImageEnable)
    {
        // set the heldItemSlot ss the slot
        this.heldItemSlot = heldItemSlot;
        // set the heldItem ss the slot's item
        this.heldItem = heldItemSlot?.GetItem();
        // set the follow image to be enabled and assigns the image sprite to be the held item icon (enulating dragging)
        SetDragFollowImage(draggingImageEnable, heldItem?.itemScriptableObject.GetItemIcon());
    }

    /// <summary>
    /// this method is called when we start/stop dragging for enabling/disabling the drag follow image
    /// it can also get a sprite to be assigned
    /// </summary>
    private void SetDragFollowImage(bool active, Sprite sprite = null)
    {
        // set the image gameobject to be active based on the active bool
        followMouseImage.gameObject.SetActive(active);
        // set the image sprite based on the sprite
        followMouseImage.sprite = sprite;
    }

}
