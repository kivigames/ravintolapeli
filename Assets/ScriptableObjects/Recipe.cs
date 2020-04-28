using System.Collections.Generic;
using GameToolkit.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private LocalizedText recipeName = null;

    [SerializeField]
    private List<Ingredient> ingredients = new List<Ingredient>();

    [SerializeField]
    private LocalizedText ingredientsText = null;

    [SerializeField]
    private LocalizedText simpleIngredientsText = null;

    [SerializeField]
    private LocalizedText recipeText = null;

    public LocalizedText RecipeName => recipeName;

    public List<Ingredient> Ingredients => ingredients;

    public LocalizedText IngredientText => ingredientsText;

    public LocalizedText SimpleIngredientText => simpleIngredientsText;

    public LocalizedText RecipeText => recipeText;
}