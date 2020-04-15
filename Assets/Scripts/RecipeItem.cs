using TMPro;
using UnityEngine;

public class RecipeItem : MonoBehaviour
{
    private Recipe recipe;

    [SerializeField]
    private TextMeshProUGUI titleText = null;

    public Recipe Recipe
    {
        get => recipe;
        set => SetRecipe(recipe = value);
    }

    private void SetRecipe(Recipe newRecipe)
    {
        titleText.text = newRecipe.RecipeName;
    }
}