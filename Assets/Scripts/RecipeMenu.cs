using System.Collections.Generic;
using GameToolkit.Localization;
using TMPro;
using UnityEngine;

public class RecipeMenu : MonoBehaviour
{
    private List<Recipe> allRecipes;

    private int currentRecipe;

    [SerializeField]
    private LocalizedTextBehaviour titleText = null;

    [SerializeField]
    private TextMeshProUGUI pageText = null;

    [SerializeField]
    private LocalizedTextBehaviour ingredientText = null;

    [SerializeField]
    private LocalizedTextBehaviour recipeText = null;

    private void Start()
    {
        allRecipes = new List<Recipe>(Resources.LoadAll<Recipe>("Recipes"));

        SetRecipe(0);
    }

    public void PreviousRecipe()
    {
        if (currentRecipe > 0) SetRecipe(currentRecipe - 1);
    }

    public void NextRecipe()
    {
        if (currentRecipe < allRecipes.Count - 1) SetRecipe(currentRecipe + 1);
    }

    private void SetRecipe(int index)
    {
        currentRecipe = index;
        var recipe = allRecipes[index];

        if (titleText)
            titleText.LocalizedAsset = recipe.RecipeName;

        if (pageText)
            pageText.text = index + 1 + "/" + allRecipes.Count;

        if (ingredientText)
            ingredientText.LocalizedAsset = recipe.IngredientText;

        if (recipeText)
            recipeText.LocalizedAsset = recipe.RecipeText;
    }
}