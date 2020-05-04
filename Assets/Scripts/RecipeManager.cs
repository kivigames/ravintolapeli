using System.Collections.Generic;
using GameToolkit.Localization;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    private List<Recipe> allRecipes;

    [SerializeField]
    private SelectedRecipeStorage selectedRecipeStorage = null;

    [Header("Recipe grading display")]
    [SerializeField]
    private RectTransform gradeModal = null;

    [SerializeField]
    private LocalizedTextBehaviour gradeText = null;

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

    public void ShowGrade(List<Ingredient> plated)
    {
        var correct = 0;
        foreach (var ing in currentRecipe.Ingredients)
            if (plated.Contains(ing))
                correct += 1;

        gradeText.FormatArgs = new[] {correct.ToString(), currentRecipe.Ingredients.Count.ToString()};
        gradeModal.gameObject.SetActive(true);
    }
}