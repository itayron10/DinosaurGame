using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("References")]
    // the item that this item instance represents
    public ItemScriptableObject itemScriptableObject;
    [Tooltip("How much from that item is in this gameobject item")]
    // the amount of this item's scriptable object that added to the inventory when we add this item
    public int itemStackAmount; 
    // item events for handling different looks and components when in and out of the inventory
    public delegate void ItemEventHandeler();
    // invokes when the item is added to the inentory
    public event ItemEventHandeler OnAddedToInventory;
    // invokes when the item is removed from the inventory
    public event ItemEventHandeler OnRemovedFromInventory;
    // invokes when the item is equiped to the player's hand
    public event ItemEventHandeler OnItemEquipped;
    // invokes when the item is unequiped to the player's hand
    public event ItemEventHandeler OnItemUnequipped;

    [Header("Item Equiping Settings")]
    // private reference for the layer name of the item when he is equiped for using item camera render
    [SerializeField] string equipedLayerName = "Item";
    // private reference for the layer name of the item when he is not in inventory for using the main camera render
    [SerializeField] string defaultLayerName = "Default";

    // is the item equipe in the player's hand (set in the hotbar)
    public bool Equiped;
    [HideInInspector] public bool inInventory;

    // can the item be added to the inventory?
    private bool canBeAddedToInventory = true;
    // a private reference
    private float pickedUpTimer;
    // the duration since the item has been removed from the inventory until it can be picked up again
    private float pickedUpDelay = 1f;

    private void Start() => GameObjectExtentions.SetLayerRecursively(transform, LayerMask.NameToLayer(defaultLayerName));

    private void Update() => HandleEqupingDelay();

    /// <summary>
    /// method called in the slot drop item function and this method sets the equiped canBeAddedToInventory and 
    /// inInventory bools to false also it changes our layer to the default layer 
    /// and invokes the OnRemovedFronInventory event and unequiping the item
    /// </summary>
    public void RemoveFromInventory()
    {
        inInventory = Equiped = canBeAddedToInventory = false;
        GameObjectExtentions.SetLayerRecursively(transform, LayerMask.NameToLayer(defaultLayerName));
        OnRemovedFromInventory?.Invoke();
        Unequip();
    }

    /// <summary>
    /// method callad in the conatiner we add ourselfs to that invokes the OnAddedToInventory event
    /// and also set our layer to the equiped layer so that we will be rendered in the item's camera
    /// </summary>
    public void AddToInventory()
    {
        GameObjectExtentions.SetLayerRecursively(transform, LayerMask.NameToLayer(equipedLayerName));
        OnAddedToInventory?.Invoke();
        inInventory = true;
    }


    /// <summary>
    /// equips the item by invoking the OnItemEquipped (called in the hotbar)
    /// </summary>
    public void Equip()
    {
        Equiped = true;
        OnItemEquipped?.Invoke();
    }
    
    /// <summary>
    /// unequips the item by invoking the OnItemUnequipped (called in the hotbar and when removed from inventory)
    /// </summary>
    public void Unequip()
    {
        Equiped = false;
        OnItemUnequipped?.Invoke();
    }

    /// <summary>
    /// when the item is not inside the inventory we don't want to immidialy add it back on content we want to wait 
    /// a short amount of time before we can add it to the inventory again
    /// </summary>
    private void HandleEqupingDelay()
    {
        if (canBeAddedToInventory) { return; }
        pickedUpTimer += Time.deltaTime;
        if (pickedUpTimer >= pickedUpDelay)
        {
            canBeAddedToInventory = true;
            pickedUpTimer = 0f;
        }
    }

    /// <summary>
    /// when our trigger enters to the inventory manager(the player) we add ourselfs to the inventory's container
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInventoryManager>(out PlayerInventoryManager inventory) && canBeAddedToInventory)
            inventory.GetMainContainer().AddItem(this);
    }
}
