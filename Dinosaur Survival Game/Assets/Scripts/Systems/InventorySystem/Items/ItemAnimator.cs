using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimator : AnimationHandler
{
    [Header("References")]
    // the item of this ItemAnimator object
    private Item item; 

    public override void FindPrivateObjects()
    {
        item = GetComponent<Item>();
        // when the item is unequiped we reset the animator
        item.OnRemovedFromInventory += ResetAnimator;
        item.OnItemUnequipped += ResetAnimator;
    }

    private void OnDestroy()
    {
        // stop listening to the unequiped and removed events when destroyed
        item.OnRemovedFromInventory -= ResetAnimator;
        item.OnItemUnequipped -= ResetAnimator;
    }

    // if the item is not equiped then the animator is disable if it is then the animator is enabled
    private void Update() => GetAnimator().enabled = item.Equiped;


}

