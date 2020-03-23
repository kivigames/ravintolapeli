using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text = null;

    [SerializeField]
    private Image image = null;

    [SerializeField]
    private Ingredient ingredient;

    public Ingredient Ingredient
    {
        get => ingredient;
        set => SetIngredient(value);
    }

    private void Awake()
    {
        // If item was given an ingredient through Unity, refresh the item so ingredient details are shown.
        if (ingredient)
            SetIngredient(ingredient);
    }

    private void SetIngredient(Ingredient ing)
    {
        ingredient = ing;

        if (text)
            text.text = ing.IngredientName;
        if (image)
            image.sprite = ing.Sprite;
    }

    public void AddToSelected()
    {
        var ingMan = FindObjectOfType<IngredientManager>();
        ingMan.SelectIngredient(ingredient);
    }

    public void RemoveFromSelected()
    {
        var ingMan = FindObjectOfType<IngredientManager>();
        ingMan.DeselectIngredient(ingredient);
    }
}