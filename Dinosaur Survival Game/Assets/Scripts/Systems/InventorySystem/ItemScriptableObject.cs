using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    // item name currently is not used maybe in the future he would be used
    [SerializeField] string itemName;

    // the item id is very important value by this we identify the item instance as this item, each item id must
    // be unique to that item
    [SerializeField] int itemId;
    public int GetItemId() => itemId;

    // the item icon is a sprite that displayed as the item instance when it is inside of a container
    [SerializeField] Sprite itemIcon;
    public Sprite GetItemIcon() => itemIcon;

    // if this is true the item will stack on top of other items when added to container that contains the item instance
    // if not then the item will take another slot in the container even if the container contains this item
    [SerializeField] bool isStackable;
    public bool IsItemStackable() => isStackable;
}
