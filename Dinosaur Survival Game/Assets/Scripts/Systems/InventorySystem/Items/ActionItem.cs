using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ActionItem : MonoBehaviour
{
    [Header("References")]
    private Item item;
    public Item GetItem() { return item; }

    private void Awake()
    {
        item = GetComponent<Item>();
        // listen to actions when the item is equipped
        item.OnItemEquipped += StartListenToAction;
        // stop listen to actions when the item is not equipped
        item.OnItemUnequipped += StopListenToAction;
    }

    private void Start() => FindPrivateObjects();

    private void OnDestroy()
    {
        DestroyActionItem();
    }

    public virtual void DestroyActionItem()
    {
        // stop listen to when destroyed
        item.OnItemEquipped -= StartListenToAction;
        item.OnItemUnequipped -= StopListenToAction;
        StopListenToAction();
    }

    private void StartListenToAction() => PlayerAttackingManager.OnPlayerAction += Action;
    private void StopListenToAction() => PlayerAttackingManager.OnPlayerAction -= Action;

    /// <summary>
    /// this method is used to find privte objects on start
    /// </summary>
    public virtual void FindPrivateObjects()
    {

    }

    public virtual void Action()
    {
        // this method is used to do stuff when the player acts with an item
    }

}
