using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : ActionItem
{
    [Header("References")]
    [SerializeField] Building buildingPrefab;
    private Building itemBuildingInstance;
    private BuildingManager buildingManager;

    public override void FindPrivateObjects()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        GetItem().OnItemUnequipped += DestroyBuildingPreview;
        GetItem().OnItemEquipped += CreateNewBuildingPreview;
    }

    public override void DestroyActionItem()
    {
        base.DestroyActionItem();
        GetItem().OnItemUnequipped -= DestroyBuildingPreview;
        GetItem().OnItemEquipped -= CreateNewBuildingPreview;
    }


    /// <summary>
    /// this method destroys the building preview when the item is not equiped
    /// </summary>
    private void DestroyBuildingPreview()
    {
        Debug.Log($"{buildingPrefab.name} was destroyed now");
        buildingManager.DestroyBuilding(itemBuildingInstance);
    }

    /// <summary>
    /// this method creats a building preview when the item is equiped
    /// </summary>
    private void CreateNewBuildingPreview()
    {
        itemBuildingInstance = Instantiate(buildingPrefab);
        buildingManager.SetBuildingInstance(itemBuildingInstance);
    }

    public override void Action()
    {
        // try to place the building and report if we can place it
        buildingManager.PlaceBuilding(out bool canPlaceBuilding);
        // if we can't place the building we return
        if (!canPlaceBuilding) { return; }
        // if we plcaed the building we remove one from the stack
        GetItem().itemStackAmount--;
        // if we have more buildings in the stack we create new building preview
        if (GetItem().itemStackAmount > 0) CreateNewBuildingPreview();
    }
}
