using UnityEngine;
using UnityEngine.UI;

public class PossibleRecipeList : MonoBehaviour
{
    [SerializeField]
    private LayoutGroup recipeList = null;

    [SerializeField]
    private PossibleRecipeItem recipeItem = null;

    public LayoutGroup RecipeList => recipeList;

    public PossibleRecipeItem RecipeItem => recipeItem;

    public void AddRecipe(Recipe recipe, int matchedIngredients)
    {
        var obj = Instantiate(recipeItem, recipeList.transform);
        obj.Recipe = recipe;
        obj.SetIngredientCount(matchedIngredients, recipe.Ingredients.Count);
    }

    public void Clear()
    {
        foreach (Transform child in recipeList.transform)
        {
            Destroy(child.gameObject);
        }
    }
}