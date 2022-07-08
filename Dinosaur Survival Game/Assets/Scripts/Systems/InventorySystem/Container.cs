using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [Header("References")]
    // all the slots this container contains (include the hotbar slots)
    [SerializeField] List<Slot> Slots;
    public delegate void OnContainerChangedState();
    // invokes when the container is closing
    public static event OnContainerChangedState OnContainerClosed;
    // invokes when the container is opening
    public static event OnContainerChangedState OnContainerOpened;
    // reference for the sound manager
    private SoundManager soundManager;

    [Header("Settings")]
    // the sounds that will be played when the container opens 
    [SerializeField] SoundScriptableObject openContainerSound;
    // the sounds that will be played when the container closes
    [SerializeField] SoundScriptableObject closeContainerSound;
    // is the container currently open
    private bool isOpen;
    public bool IsOpen() { return isOpen; }


    private void Start()
    {
        // getting the sound manager
        soundManager = SoundManager.instance;
        // by default the container should be closed
        gameObject.SetActive(false);
    }


    /// <summary>
    /// method for openning the container and activating the gameobject
    /// </summary>
    public void OpenContainer()
    {
        // invokes the OnContainerOpened event
        OnContainerOpened?.Invoke();
        // plays the openContainerSound
        soundManager.PlaySound(openContainerSound);
        // Set the container opened state as true
        SetContainerOpened(true);
    }

    /// <summary>
    /// method for closing the container and disabling the gameobject
    /// </summary>
    public void CloseContainer()
    {
        // invokes the OnContainerClosed event
        OnContainerClosed?.Invoke();
        // plays the closeContainerSound
        soundManager.PlaySound(closeContainerSound);
        // Set the container opened state as false
        SetContainerOpened(false);
    }

    /// <summary>
    /// method for setting the container as opened or as closed 
    /// </summary>
    private void SetContainerOpened(bool Opened)
    {
        // setting the isOpen bool
        isOpen = Opened;
        // setting gameobject activation
        gameObject.SetActive(Opened);
    }


    /// <summary>
    /// method for checking if this container containes an item, can recieve a neededItemAmount which
    /// is the min amount we need to have from this item for this method to return true
    /// </summary>
    public bool ContaineItem(ItemScriptableObject item, int neededItemAmount = 1)
    {
        // how much currently we have from this item (default is 0)
        int currentItemAmount = 0;

        // looping all the slots in the container
        for (int i = 0; i < Slots.Count; i++)
        {
            Item slotItem = Slots[i].GetItem();
            // if the slot's item is the same as our item we add the slot amount to the currentItemAmount
            if (slotItem == null) { continue; }
            if (slotItem.itemScriptableObject == item) currentItemAmount += slotItem.itemStackAmount;
        }

        // in the end returning if we have the neededItemAmount from this item in the container
        return currentItemAmount >= neededItemAmount;
    }

    /// <summary>
    /// method for checking if our container is full(if all the slots inside it have items in them)
    /// </summary>
    public bool IsFull()
    {
        // if we can't find any empty slots we are full else we are not full
        return GetEmptySlot() == null;
    }

    /// <summary>
    /// method for removing item from the container 
    /// (NOTE: this doesnt drop the item from the container this destroyes it)
    /// </summary>
    public void RemoveItem(Item item)
    {
        // loop all the slots
        for (int i = 0; i < Slots.Count; i++)
        {
            Slot slot = Slots[i];
            // if the slot containes the item we want to remove delete the item
            if (slot.GetItem() == item) slot.DeleteItem();
        }
    }


    /// <summary>
    /// method for setting an item amount inside of the container to the itemAmount the method receives
    /// </summary>
    public void SetItemAmount(ItemScriptableObject item, int itemAmount)
    {
        // looping all the slots
        for (int i = 0; i < Slots.Count; i++)
        {
            Item slotItem = Slots[i].GetItem();
            // if the slot item is the same as our item then set his amount to the itemAmount
            if (slotItem?.itemScriptableObject == item) slotItem.itemStackAmount = itemAmount;
        }
    }

    /// <summary>
    /// method for getting an item amount inside of the container
    /// </summary>
    public int GetItemAmount(ItemScriptableObject item)
    {
        // looping all the slots
        for (int i = 0; i < Slots.Count; i++)
        {
            Item slotItem = Slots[i].GetItem();
            if (slotItem == null) { continue; }
            // if the slot item is the same as our item then return his amount to the itemAmount
            if (slotItem.itemScriptableObject == item) return slotItem.itemStackAmount;
        }

        // if we dodn't found the item return 0 we have 0 amount from that item
        return 0;
    }
    

    /// <summary>
    /// method for adding an item to the container
    /// </summary>
    public void AddItem(Item itemToAdd)
    {
        // the empty slot is a reference for the first empty slot we will find looping the slots
        Slot emptySlot = GetEmptySlot();
        // matching slot is a reference for the first slot with the item type the same as the itemToAdd
        Slot matchingSlot = GetStackingSlot(itemToAdd);


        // if the matching slot was found and the item is stackable or the empty slot was found add the item
        // to the inventory
        if ((matchingSlot && itemToAdd.itemScriptableObject.IsItemStackable()) || emptySlot)
        {
            // based on the item can be stacked or not assign the item to the correct slot
            if (itemToAdd.itemScriptableObject.IsItemStackable() && matchingSlot)
                StackItem(itemToAdd, matchingSlot.GetItem());
            else
                emptySlot.AddItem(itemToAdd);

            // inform the item that we add it to the inventory
            itemToAdd.AddToInventory();
        }
        else
            //Inventory Full
            Debug.Log("Full Container");
    }

    /// <summary>
    /// method for getting an empty slot from the container
    /// </summary>
    private Slot GetEmptySlot()
    {
        // loop all the slots
        for (int i = 0; i < Slots.Count; i++)
        {
            Slot Slot = Slots[i];
            // if the slot is empty return it
            if (Slot.GetItem() == null) return Slot;
        }

        // if we didn't find any empty slots return null
        return null;
    }

    /// <summary>
    /// method for getting a slot in the container that has the same item type as itemToStack
    /// </summary>
    private Slot GetStackingSlot(Item itemToStack)
    {
        // looping all the slots in the container
        for (int i = 0; i < Slots.Count; i++)
        {
            Item slotItem = Slots[i].GetItem();
            // if the slot doesnt have an item continue
            if (slotItem == null) { continue; }
            // if the slot item type is the same as the itemToStack item type we found the stacking slot
            if (slotItem.itemScriptableObject == itemToStack.itemScriptableObject) return Slots[i];
        }

        // if we didn't find the stacking slot return null
        return null;
    }

    /// <summary>
    /// method for stacking two items on top of each other
    /// </summary>
    private static void StackItem(Item itemToAdd, Item currentSlotItem)
    {
        // add the itemToAdd amount to the currentSlotItem
        currentSlotItem.itemStackAmount += itemToAdd.itemStackAmount;
        // destroyes the itemToAdd as it is not needed anymore
        Destroy(itemToAdd.gameObject);
    }
}
