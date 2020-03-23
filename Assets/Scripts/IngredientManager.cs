using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum IngredientListType
{
    All,
    Selected,

    Dry,
    Fridge
}

public class IngredientManager : MonoBehaviour
{
    private readonly List<Ingredient> dryIngredients = new List<Ingredient>();
    private readonly List<Ingredient> fridgeIngredients = new List<Ingredient>();

    private readonly ObservableCollection<Ingredient> selectedIngredients = new ObservableCollection<Ingredient>();
    private List<Ingredient> allIngredients;

    public List<Ingredient> AllIngredients => allIngredients;

    public List<Ingredient> DryIngredients => dryIngredients;

    public List<Ingredient> FridgeIngredients => fridgeIngredients;

    public ObservableCollection<Ingredient> SelectedIngredients => selectedIngredients;

    private void Awake()
    {
        allIngredients = new List<Ingredient>(Resources.LoadAll<Ingredient>("Ingredients"));

        foreach (var ingredient in allIngredients)
        {
            Debug.Log("Loaded ingredient " + ingredient.IngredientName);

            if (ingredient.StorageType == StorageType.Dry)
                dryIngredients.Add(ingredient);
            else if (ingredient.StorageType == StorageType.Fridge)
                fridgeIngredients.Add(ingredient);
        }
    }

    public void SelectIngredient(Ingredient ing)
    {
        Debug.Log("Selecting ingredient " + ing.IngredientName);
        if (ing.Selected) return;

        SelectedIngredients.Add(ing);
        ing.Selected = true;
    }

    public void SelectIngredient(IngredientItem ingItem)
    {
        SelectIngredient(ingItem.Ingredient);
    }

    public void DeselectIngredient(Ingredient ing)
    {
        Debug.Log("Deselecting ingredient " + ing.name);
        if (!ing.Selected) return;

        SelectedIngredients.Remove(ing);
        ing.Selected = false;
    }

    public void DeselectIngredient(IngredientItem ingItem)
    {
        DeselectIngredient(ingItem.Ingredient);
    }
}