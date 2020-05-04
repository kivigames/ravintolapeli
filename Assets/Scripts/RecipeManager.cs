using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private List<Recipe> allRecipes;

    [SerializeField]
    private SelectedRecipeStorage selectedRecipeStorage = null;

    private Recipe currentRecipe = null;

    public Recipe CurrentRecipe
    {
        get => currentRecipe;
        set => currentRecipe = value;
    }

    public List<Recipe> AllRecipes => allRecipes;

    private void Start()
    {
        allRecipes = new List<Recipe>(Resources.LoadAll<Recipe>("Recipes"));
        if (selectedRecipeStorage) CurrentRecipe = selectedRecipeStorage.SelectedRecipe;
    }
}