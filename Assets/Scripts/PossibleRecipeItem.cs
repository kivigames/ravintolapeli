using GameToolkit.Localization;
using UnityEngine;

public class PossibleRecipeItem : RecipeItem
{
    [SerializeField]
    private LocalizedTextBehaviour ingredientCountText = null;

    public void SetIngredientCount(int count, int max)
    {
        ingredientCountText.FormatArgs = new[] {count.ToString(), max.ToString()};
    }
}