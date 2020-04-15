using System.Collections.Generic;
using UnityEngine;

public class CounterScreen : GameScreen
{
    [SerializeField]
    private IngredientList plateIngredientList = null;

    private RecipeManager recipeManager;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
    }

    public void ShowGrade()
    {
        var platedIngredients = new List<Ingredient>();
        foreach (RectTransform item in plateIngredientList.listGroup.transform)
            platedIngredients.Add(item.GetComponent<IngredientItem>().Ingredient);

        recipeManager.ShowGrade(platedIngredients);
    }
}