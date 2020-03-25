using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterScreen : GameScreen
{
    public RectTransform gradeModal;

    public TextMeshProUGUI gradeText;

    public IngredientList plateIngredientList;

    public Recipe gradedRecipe;

    private string originalText;

    private void Awake()
    {
        originalText = gradeText.text;
    }

    public void ShowGrade()
    {
        var platedIngredients = new List<Ingredient>();
        foreach (RectTransform item in plateIngredientList.listGroup.transform)
            platedIngredients.Add(item.GetComponent<IngredientItem>().Ingredient);

        var correct = 0;
        foreach (var ing in gradedRecipe.Ingredients)
            if (platedIngredients.Contains(ing))
                correct += 1;

        gradeText.text = string.Format(originalText, correct, gradedRecipe.Ingredients.Count);
        gradeModal.gameObject.SetActive(true);
    }
}