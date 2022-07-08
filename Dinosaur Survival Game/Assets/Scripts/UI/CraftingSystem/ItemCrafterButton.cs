using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemCrafter))]
public class ItemCrafterButton : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The foreground image that covers the entire button and apear when the the recipe can't be crafted")]
    [SerializeField] Image itemCrafterEnableImage;
    private ItemCrafter itemCrafter;

    private void Awake()
    {
        itemCrafter = GetComponent<ItemCrafter>();
    }

    private void Update()
    {
        // enable the image to disable button presses and make the crafter button visuals darker when can't craft
        itemCrafterEnableImage.enabled = !ItemCrafter.HaveIngredients
            (itemCrafter.GetRecipe(), itemCrafter.GetContainer());
    }



}
