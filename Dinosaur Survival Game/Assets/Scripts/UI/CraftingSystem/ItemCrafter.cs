using UnityEngine;

public class ItemCrafter : MonoBehaviour
{
    [Header("References")]
    // a reference for the player container
    private Container playerContainer;

    [Header("Settings")]
    // this recipe is used to determine the ingredients and the item result of the craft function
    [SerializeField] RecipeScriptableObject recipe;
    [Tooltip("This Where Item Will Spawn Once the Inventory is Full")]
    // the items spawn position when they crafted
    [SerializeField] Transform spawnPointWhenContainerFullTranform;

    public Container GetContainer() { return playerContainer; }
    public RecipeScriptableObject GetRecipe() { return recipe; }

    private void Start()
    {
        // finds the player container
        playerContainer = PlayerInventoryManager.instance.GetMainContainer();
    }

    /// <summary>
    /// craft method can be called from any script and is used to craft this itemCrafter recipe 
    /// </summary>
    public void Craft()
    {
        CraftRecipe(recipe, playerContainer, spawnPointWhenContainerFullTranform.position);
    }

    /// <summary>
    /// this static method crafts a recipe to the given container with a given recipe
    /// </summary>
    private static void CraftRecipe(RecipeScriptableObject recipe, Container container, Vector3 spawnPos)
    {
        if (HaveIngredients(recipe, container))
        {
            RemoveRecipeingredientsFromInventory(recipe, container);

            Item item = Instantiate(recipe.outcomeIngredientPrefab, spawnPos, Quaternion.identity).GetComponent<Item>();
            container.AddItem(item);
        }
        else
            Debug.Log($"you can not make this: {recipe.outcomeIngredientPrefab.name}");
    }


    /// <summary>
    /// this static method removes the given recipe ingredients from the given container
    /// </summary>
    private static void RemoveRecipeingredientsFromInventory(RecipeScriptableObject recipe, Container container)
    {
        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            Ingredient ingredient = recipe.ingredients[i];
            ItemScriptableObject ingredientItem = ingredient.item;
            container.SetItemAmount
                (ingredientItem, container.GetItemAmount(ingredientItem) - ingredient.itemAmount);
        }
    }

    /// <summary>
    /// this static method returns if the given recipe ingredients are available in the given container
    /// </summary>
    public static bool HaveIngredients(RecipeScriptableObject recipe, Container container)
    {
        if (!container) { return false; }

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            Ingredient ingredient = recipe.ingredients[i];
            if (!container.ContaineItem(ingredient.item, ingredient.itemAmount)) return false;
        }
        
        return true;
    }
}
