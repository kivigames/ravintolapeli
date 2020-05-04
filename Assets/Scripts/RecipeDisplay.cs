using GameToolkit.Localization;
using UnityEngine;

public class RecipeDisplay : MonoBehaviour
{
    [SerializeField]
    private LocalizedTextBehaviour recipeTitle = null;

    [SerializeField]
    private LocalizedTextBehaviour recipeText = null;

    private Recipe recipe = null;

    public Recipe Recipe
    {
        get => recipe;
        set => UpdateRecipe(recipe = value);
    }

    private void UpdateRecipe(Recipe newRecipe)
    {
        if (recipeText && newRecipe)
        {
            gameObject.SetActive(true);
            recipeTitle.LocalizedAsset = newRecipe.RecipeName;
            recipeText.LocalizedAsset = newRecipe.SimpleIngredientText;
        }
        else
        {
            recipeTitle.LocalizedAsset = null;
            recipeText.LocalizedAsset = null;
            gameObject.SetActive(false);
        }
    }
}