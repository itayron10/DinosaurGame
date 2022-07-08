using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [Header("Referneces")]
    // private reference for the current item in the slot
    private Item item;
    public Item GetItem() { return item; }


    [Header("Settings")]
    // reference for the image that will display the item icon when he is in this slot
    [SerializeField] Image itemIconImage;
    // reference for the text that will display the item amount when he is in this slot
    [SerializeField] TextMeshProUGUI itemAmountText;


    private void Update()
    {
        UpdateUi();
        UpdateItem();
    }

    /// <summary>
    /// handles the item stack amount getting lower than 0 if it is lower than 0 then the item is destroyed
    /// this is for easier workflow working with stackable items
    /// </summary>
    private void UpdateItem()
    {
        if (GetItem() == null) { return; }
        if (GetItem().itemStackAmount < 1) DeleteItem();
    }

    /// <summary>
    /// method for droping the item from the inventory the drop point is the transform which from it the
    /// item will be dropped and the throw streangth is how much force to throw the item from the dropPoint
    /// </summary>
    public void DropItem(Transform dropPoint, float throwStreangth)
    {
        // if there is no item we can't drop it so return
        if (item == null) { return; }
        // enables the item's gameobject
        item.gameObject.SetActive(true);
        // set the item's parant to null
        item.transform.SetParent(null, false);
        // set the item's gameobject's position to the drop point
        item.gameObject.transform.position = dropPoint.position;
        // set the item's gameobject's rotation to the drop point
        item.gameObject.transform.rotation = dropPoint.rotation;
        // add to the item a rigidbody and lanches the item forward using the throwStreacngth
        item.gameObject.AddComponent<Rigidbody>().velocity = dropPoint.forward * throwStreangth;
        // updates the item by telling him we removed the item from the inventory
        item.RemoveFromInventory();
        // removes the item from the slot's item
        item = null;
    }

    /// <summary>
    /// method for destroying the item in this slot
    /// </summary>
    public void DeleteItem() => Destroy(item.gameObject);

    /// <summary>
    /// method for adding an item to the slot if this slot is empty
    /// </summary>
    public void AddItem(Item itemToAdd)
    {
        if (item != null || itemToAdd == null) { return; }
        // make the slot's item this item
        item = itemToAdd;
        // disable the item's gameobject
        itemToAdd.gameObject.SetActive(false);
        // setting the item's position to the transform position
        itemToAdd.transform.SetParent(this.transform, false);
        // destroying the item's rigidbody if it has a rigidbody
        if (itemToAdd.TryGetComponent<Rigidbody>(out Rigidbody rb)) Destroy(rb);
    }

    /// <summary>
    /// sets the current item to the item it recieves
    /// </summary>
    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    /// <summary>
    /// updates the slot's ui display (itemIconImage and itemAmountText) based on if the item is null or not
    /// if it is we just update it to display nothing if not we display the item's icon and stack amount
    /// </summary>
    public void UpdateUi()
    {
        if (item == null)
            SetUi(null, string.Empty);
        else
            SetUi(item.itemScriptableObject.GetItemIcon(), item.itemStackAmount.ToString("n0"));
    }


    /// <summary>
    /// sets the ui display of this slot (itemIconImage and itemAmountText) according to the icon and amount text
    /// </summary>
    private void SetUi(Sprite icon, string amountText)
    {
        itemIconImage.sprite = icon;
        itemAmountText.text = amountText;
    }
}
