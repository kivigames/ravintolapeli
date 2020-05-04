using System;
using System.Collections.Generic;
using GameToolkit.Localization;
using UnityEngine;
using UnityEngine.UI;

public class CounterScreen : GameScreen
{
    [SerializeField]
    private IngredientList plateIngredientList = null;

    [Header("Grading button and display")]
    [SerializeField]
    private Button gradeButton = null;

    [Tooltip("Panel to enable when showing grade.")]
    [SerializeField]
    private RectTransform gradeModal = null;

    [Tooltip("Text element to change according to grade.")]
    [SerializeField]
    private LocalizedTextBehaviour gradeText = null;

    [Tooltip("Localized text for normal dish grading.")]
    [SerializeField]
    private LocalizedText gradeTextNormal = null;

    [Tooltip("Localized text for completed dish grading.")]
    [SerializeField]
    private LocalizedText gradeTextWin = null;

    [Header("Free play recipe list")]
    [SerializeField]
    private Button recipesButton = null;

    [SerializeField]
    private PossibleRecipeList possibleRecipesList = null;

    [SerializeField]
    private GameObject freePlayHelpPanel = null;

    [Header("Recipe display")]
    [SerializeField]
    private RecipeDisplay recipeDisplay = null;

    private RecipeManager recipeManager;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();

        recipeDisplay.Recipe = recipeManager.CurrentRecipe;

        gradeButton.gameObject.SetActive(recipeDisplay.Recipe != null);
        recipesButton.gameObject.SetActive(recipeDisplay.Recipe == null);
        if (freePlayHelpPanel)
            freePlayHelpPanel.SetActive(recipeDisplay.Recipe == null);
    }

    public void ShowGrade()
    {
        var currentRecipe = recipeManager.CurrentRecipe;

        // Add plated ingredients to a list
        var plated = new List<Ingredient>();
        foreach (RectTransform item in plateIngredientList.ListGroup.transform)
            plated.Add(item.GetComponent<IngredientItem>().Ingredient);

        // Try to find recipe's ingredients from plated list
        var correct = 0;
        foreach (var ing in currentRecipe.Ingredients)
            if (plated.Contains(ing))
                correct += 1;

        var outOf = currentRecipe.Ingredients.Count;

        if (correct == outOf)
        {
            gradeText.LocalizedAsset = gradeTextWin;
        }
        else
        {
            gradeText.LocalizedAsset = gradeTextNormal;
            gradeText.FormatArgs = new[] {correct.ToString(), currentRecipe.Ingredients.Count.ToString()};
        }

        gradeModal.gameObject.SetActive(true);
    }

    private void AddIngredientsToSet(ISet<Ingredient> set, Ingredient ingredient)
    {
        if (ingredient == null) return;
        set.Add(ingredient);
        if (ingredient.BoiledVersion)
            AddIngredientsToSet(set, ingredient.BoiledVersion);
        if (ingredient.ChoppedVersion)
            AddIngredientsToSet(set, ingredient.ChoppedVersion);
        if (ingredient.FriedVersion)
            AddIngredientsToSet(set, ingredient.FriedVersion);
    }

    public void ShowPossibleRecipes()
    {
        // Add plated ingredients to a set, to make sure there's only max. one of each
        // Also recursively add all versions of each ingredient
        var platedIngredients = new HashSet<Ingredient>();
        foreach (RectTransform item in plateIngredientList.ListGroup.transform)
            AddIngredientsToSet(platedIngredients, item.GetComponent<IngredientItem>().Ingredient);

        possibleRecipesList.Clear();

        // Find matching ingredient counts on all recipes
        var possibleRecipes = new List<Tuple<Recipe, int>>();
        foreach (var recipe in recipeManager.AllRecipes)
        {
            var ingCount = recipe.Ingredients.Count;
            var matching = new HashSet<Ingredient>(platedIngredients);
            matching.IntersectWith(recipe.Ingredients);

            Debug.Log($"Possible: {recipe.RecipeName.Value}: {matching.Count}/{ingCount}");

            // Only add if any ingredients match
            if (matching.Count > 0)
                possibleRecipes.Add(new Tuple<Recipe, int>(recipe, matching.Count));
        }

        // Sort recipes by missing ingredient count (missing = total ingredient count - matching ingredient count)
        possibleRecipes.Sort((t1, t2) =>
        {
            return t1.Item1.Ingredients.Count - t1.Item2 - (t2.Item1.Ingredients.Count - t2.Item2);
        });

        // Add matched recipes in order to the list
        foreach (var (recipe, matching) in possibleRecipes) possibleRecipesList.AddRecipe(recipe, matching);

        possibleRecipesList.gameObject.SetActive(true);
    }
}