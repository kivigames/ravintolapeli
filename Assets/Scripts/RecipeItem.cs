using GameToolkit.Localization;
using UnityEngine;

public class RecipeItem : MonoBehaviour
{
    private Recipe recipe;

    [SerializeField]
    private LocalizedTextBehaviour titleText = null;

    public Recipe Recipe
    {
        get => recipe;
        set => SetRecipe(recipe = value);
    }

    protected virtual void SetRecipe(Recipe newRecipe)
    {
        titleText.LocalizedAsset = newRecipe.RecipeName;
    }
}