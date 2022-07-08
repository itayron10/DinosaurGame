using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangerItem : ActionItem
{
    [Header("References")]
    [SerializeField] string statName; // the name of the stat this item will change (the stats defined in the player stats)
    [SerializeField] string onUseAnimationTriggerName; // the name of the animator trigger that will be triggered when we use the item
    private Stat Stat; // private reference for the stat this item will change
    private ItemAnimator itemAnimator; // private reference for the item animator that will help us change animator state
    private bool canBeUsed = true; // when true the player can use the item by pressing the input(help with spamming delay)
    private float delayTimer; // the timer that counts after the player use this item to help with spamming

    [Header("Settings")]
    [SerializeField] float statIncreaseAmount; // how much teh stat will be increased after using this item once
    [SerializeField] float usingDelay = 1f; // the delay between uses, the player can't use this item for usingDelay after using it


    private void Update() => HandleItemDelay();


    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        Stat = PlayerStats.instance.GetStat(statName);
        itemAnimator = GetComponent<ItemAnimator>();
    }

    private void HandleItemDelay()
    {

        // when we cant use the item start counting delay
        if (canBeUsed) { return; }

        // counting the delay
        delayTimer += Time.deltaTime;
        // when the delay reaching the using delay the item can be used again
        if (delayTimer >= usingDelay)
        {
            canBeUsed = true;
            delayTimer = 0f;
        }
    }

    public override void Action()
    {
        // only using the item when we can use it
        if (!canBeUsed) { return; }
        StartCoroutine(ChangeStat());
    }

    private IEnumerator ChangeStat()
    {
        // set the animation trigger
        itemAnimator?.SetTrigger(onUseAnimationTriggerName);
        yield return new WaitForSeconds(itemAnimator.GetAnimator().GetCurrentAnimatorClipInfo(0).Length);
        // set the stat amount
        Stat.SetStatAmount(Stat.GetStatAmount() + statIncreaseAmount);
        // remove a single item from the inventory
        GetItem().itemStackAmount--;
        // reset the delay, we can't use the item right after using it we have a bit of delay
        canBeUsed = false;
    }

}
