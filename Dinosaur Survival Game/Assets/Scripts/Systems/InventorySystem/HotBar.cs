using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotBar : MonoBehaviour
{
    [Header("References")]
    // the tranform that holds the selected item in the hotbar
    [SerializeField] Transform itemHandHolder;
    // all the slots of the hotbar
    [SerializeField] List<HotBarSlot> Slots;

    [Header("Parameters")]
    // the current slot the player selected in the hotbar
    private HotBarSlot activeSlot;
    // the index of the active slot in the Slots list
    private int activeSlotIndex;


    private void Update()
    {
        SetActiveSlotIndex();
        SetSlotsState();
    }

    /// <summary>
    /// this method setting the slots to be the active / not active slot based on the index
    /// </summary>
    private void SetSlotsState()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            HotBarSlot slot = Slots[i];
            HandleActiveSlotItem(slot);
        }
    }


    /// <summary>
    /// method for handeling the active hotbar slot and mainly handle
    /// the slot's item, set the equip state of the slot's item if the item is not null 
    /// </summary>
    private void HandleActiveSlotItem(HotBarSlot slot)
    {
        // Get the item and the reference for if the slot is the active slot
        Item item = slot.GetItem();
        bool isActiveSlot = IsActiveSlot(slot);

        // Set Slot Ui highlight image
        slot.GetHighlightImage().enabled = isActiveSlot;
        
        // if there is no item just return
        if (item == null) { return; }
        // if the slot is the active slot then the parant is the item holder if not then the parant is the slot
        if (isActiveSlot)
            item.transform.SetParent(itemHandHolder.transform, false);
        else
            item.transform.SetParent(slot.transform, false);

        SetItemBasedOnActiveSlot(item, isActiveSlot);
    }

    public bool IsActiveSlot(HotBarSlot slot)
    {
        return slot == activeSlot;
    }

    /// <summary>
    /// method for setting an item equiped settings based on if the item's slot is the active slot
    /// </summary>
    public static void SetItemBasedOnActiveSlot(Item item, bool isActiveSlot)
    {
        // only if the item equiped is not set we need to set it
        if (item.Equiped != isActiveSlot)
        {
            // set the item equiped state
            item.Equiped = isActiveSlot;
            if (!item.Equiped) item.Unequip();
            else item.Equip();
        }

        // enable/disable the gameobject
        item.gameObject.SetActive(isActiveSlot);
        // reset the position
        item.transform.localPosition = Vector3.zero;
        // reset the rotation
        item.transform.localRotation = Quaternion.identity;
    }



    private void SetActiveSlotIndex()
    {
        // get the input scroll amount
        float currentScrollAmount = InputManager.GetScrollAmount();

        // if the scroll amount is positive then increase the index until it reach the end then loop back
        if (currentScrollAmount > 0)
        {
            activeSlotIndex ++;
            if (activeSlotIndex >= Slots.Count) activeSlotIndex = 0;
        }
        // if the scroll amount is negetive then decrease the index until it reach the end then loop back
        else if (currentScrollAmount < 0)
        {
            if (activeSlotIndex <= 0) activeSlotIndex = Slots.Count;
            activeSlotIndex--;
        }

        // set the active slot by the active slot index
        activeSlot = Slots[activeSlotIndex];
    }

}
