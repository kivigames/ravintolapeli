using System.Collections.Generic;
using GameToolkit.Localization;
using TMPro;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    private SelectedRecipeStorage selectedRecipeStorage = null;

    [Header("Recipe display")]
    [SerializeField]
    private LocalizedTextBehaviour recipeTitle = null;

    [SerializeField]
    private LocalizedTextBehaviour recipeText = null;

    [Header("Recipe grading display")]
    [SerializeField]
    private RectTransform gradeModal = null;

    [SerializeField]
    private TextMeshProUGUI gradeText = null;

    private Recipe currentRecipe = null;
    private string originalGradeText;

    public Recipe CurrentRecipe
    {
        get => currentRecipe;
        set => SetRecipe(currentRecipe = value);
    }

    private void Awake()
    {
        originalGradeText = gradeText.text;
    }

    private void Start()
    {
        if (selectedRecipeStorage) CurrentRecipe = selectedRecipeStorage.SelectedRecipe;
    }

    private void SetRecipe(Recipe recipe)
    {
        if (recipeText && recipe)
        {
            recipeTitle.LocalizedAsset = recipe.RecipeName;
            recipeText.LocalizedAsset = recipe.IngredientText;
        }
        else if (recipeText)
        {
            recipeTitle.LocalizedAsset = null;
            recipeText.LocalizedAsset = null;
        }
    }

    public void ShowGrade(List<Ingredient> plated)
    {
        var correct = 0;
        foreach (var ing in currentRecipe.Ingredients)
            if (plated.Contains(ing))
                correct += 1;

        gradeText.text = string.Format(originalGradeText, correct, currentRecipe.Ingredients.Count);
        gradeModal.gameObject.SetActive(true);
    }
}