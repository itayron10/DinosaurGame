using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class RecipeScriptableObject : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("The list of all the ingredients for crafting this recipe")]
    public Ingredient[] ingredients;
    [Tooltip("The prefab that will be added to the inventory when crafting this recipe")]
    public GameObject outcomeIngredientPrefab;
}

[System.Serializable]
public class Ingredient
{
    [Tooltip("What item this ingredient need")]
    public ItemScriptableObject item; // the ingredient item
    [Tooltip("how much from this ingredient's item is needed")]
    public int itemAmount; // this will be used to check how much from the ingredient's item is needed
}
