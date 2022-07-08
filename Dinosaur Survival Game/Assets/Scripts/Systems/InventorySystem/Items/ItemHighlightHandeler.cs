using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

[RequireComponent(typeof(Item))]
[RequireComponent(typeof(HighlightEffect))]
public class ItemHighlightHandeler : MonoBehaviour
{
    [Header("Reference")]
    private Item item; // the item of this ItemHighlightHandeler object
    private HighlightEffect highlightEffect; // the HighlightEffect of this ItemHighlightHandeler object

    private void Awake()
    {
        FindPrivateObjects();
        SubscribeToItemEvents();
    }

    private void OnDestroy()
    {
        // stop listening to the item events when destroyed
        item.OnAddedToInventory -= UpdateHighlited;
        item.OnRemovedFromInventory -= UpdateHighlited;
    }

    private void SubscribeToItemEvents()
    {
        // update the highlight whenever the item is equiped or unequipped
        item.OnAddedToInventory += UpdateHighlited;
        item.OnRemovedFromInventory += UpdateHighlited;
    }

    private void FindPrivateObjects()
    {
        item = GetComponent<Item>();
        highlightEffect = GetComponent<HighlightEffect>();
    }

    private void UpdateHighlited()
    {
        // update the highlight effect to when the item is added/removed from inventory
        highlightEffect.SetHighlighted(!highlightEffect.highlighted);
    }
}
