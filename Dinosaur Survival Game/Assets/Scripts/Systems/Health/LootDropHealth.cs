using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropHealth : BasicHealth
{
    [SerializeField] LootDrop[] lootDrops;
    [SerializeField] Vector3 lootDropSpawnPositionOffset;

    public override void Die()
    {
        foreach (LootDrop lootDrop in lootDrops)
        {
            if (Random.value <= lootDrop.GetSpawnChance)
            {
                // instatiate the dropped item
                if (!lootDrop.GetLootItemPrefab) { continue; }
                Item droppedItem = Instantiate(lootDrop.GetLootItemPrefab, 
                    transform.position + lootDropSpawnPositionOffset,
                   Quaternion.identity);
                Debug.Log(droppedItem);
                // set the dropped item stack amount based on the DroppedItemStackAmount variable of the lootDrop
                droppedItem.itemStackAmount = Random.Range(lootDrop.GetDroppedItemStackAmount.x,
                    lootDrop.GetDroppedItemStackAmount.y);
            }
        }   
        base.Die();
    }
}

[System.Serializable]
public class LootDrop
{
    // the preab of the item that will be dropped
    [SerializeField] Item lootItemPrefab;
    public Item GetLootItemPrefab => lootItemPrefab;

    // the chance that the item will be dropped
    [Range(0f, 1f)] [SerializeField] float spawnChance;
    public float GetSpawnChance => spawnChance;

    // the amount of the dropped item stack amount that will be dropped (x is min, y is max)
    [SerializeField] Vector2Int droppedItemStackAmount;
    public Vector2Int GetDroppedItemStackAmount => droppedItemStackAmount;

}
