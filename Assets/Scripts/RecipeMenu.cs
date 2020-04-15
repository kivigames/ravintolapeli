using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeMenu : MonoBehaviour
{
    private List<Recipe> allRecipes;

    private int currentRecipe;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI pageText;

    [SerializeField]
    private TextMeshProUGUI ingredientText;

    [SerializeField]
    private TextMeshProUGUI recipeText;

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
            titleText.text = recipe.RecipeName;

        if (pageText)
            pageText.text = (index + 1) + "/" + allRecipes.Count;

        if (ingredientText)
            ingredientText.text = recipe.IngredientText;

        if (recipeText)
            recipeText.text = recipe.RecipeText;
    }
}