using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecipeSelection : MonoBehaviour
{
    private List<Recipe> allRecipes;

    [SerializeField]
    private RecipeItem itemPrefab = null;

    [SerializeField]
    private Color selectedColor = Color.cyan;

    [SerializeField]
    private SelectedRecipeStorage selectedRecipeStorage = null;

    [SerializeField]
    private LayoutGroup recipeList = null;

    [SerializeField]
    private Button playButton = null;

    private Color originalColor;
    private RecipeItem current;

    private void Start()
    {
        if (!itemPrefab) return;

        allRecipes = new List<Recipe>(Resources.LoadAll<Recipe>("Recipes"));

        originalColor = itemPrefab.GetComponent<Image>().color;

        foreach (var recipe in allRecipes)
        {
            var item = Instantiate(itemPrefab, recipeList.transform);
            item.Recipe = recipe;
            item.GetComponent<Button>().onClick.AddListener(() => OnClick(item));
        }

        playButton.onClick.AddListener(OnStartGame);
    }

    // Enable play button when a recipe is selected.
    private void OnClick(RecipeItem origin)
    {
        if (current)
            current.GetComponent<Image>().color = originalColor;

        if (current == origin)
        {
            current = null;
            selectedRecipeStorage.SelectedRecipe = null;
            playButton.interactable = false;
            return;
        }

        current = origin;
        selectedRecipeStorage.SelectedRecipe = current.Recipe;
        current.GetComponent<Image>().color = selectedColor;
        playButton.interactable = true;
    }

    private void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }
}