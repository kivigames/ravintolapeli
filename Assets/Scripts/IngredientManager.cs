using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

public class IngredientManager : MonoBehaviour
{
    private readonly List<Ingredient> dryIngredients = new List<Ingredient>();
    private readonly List<Ingredient> fridgeIngredients = new List<Ingredient>();

    private readonly ObservableCollection<Ingredient> selectedIngredients = new ObservableCollection<Ingredient>();
    private List<Ingredient> allIngredients;

    private Ingredient activeIngredient;

    [SerializeField]
    private ActiveIngredientChangeEvent onActiveIngredientChanged;

    public ActiveIngredientChangeEvent OnActiveIngredientChanged => onActiveIngredientChanged;

    public Ingredient ActiveIngredient
    {
        get => activeIngredient;
        set
        {
            activeIngredient = value;
            Debug.Log("IngredientManager: Setting active ingredient to " +
                      (activeIngredient ? activeIngredient.IngredientName : null));
            onActiveIngredientChanged.Invoke(activeIngredient);
        }
    }

    public List<Ingredient> AllIngredients => allIngredients;

    public List<Ingredient> DryIngredients => dryIngredients;

    public List<Ingredient> FridgeIngredients => fridgeIngredients;

    public ObservableCollection<Ingredient> SelectedIngredients => selectedIngredients;

    private void Awake()
    {
        Debug.Log("IngredientManager: Awake");
        if (onActiveIngredientChanged == null)
            onActiveIngredientChanged = new ActiveIngredientChangeEvent();

        // Load all ingredients into a list.
        allIngredients = new List<Ingredient>(Resources.LoadAll<Ingredient>("Ingredients"));

        // Additionally, store ingredients to separate lists by storage type.
        foreach (var ingredient in allIngredients)
        {
            Debug.Log("Loaded ingredient " + ingredient.IngredientName);

            if (ingredient.StorageType == StorageType.Dry)
                dryIngredients.Add(ingredient);
            else if (ingredient.StorageType == StorageType.Fridge)
                fridgeIngredients.Add(ingredient);
        }
    }

    public void SetActiveIngredient(IngredientItem ingredientItem)
    {
        ActiveIngredient = ingredientItem.Ingredient;
    }

    public bool IsIngredientSelected(Ingredient ing)
    {
        return SelectedIngredients.Contains(ing);
    }

    public void ReplaceIngredient(Ingredient ing, Ingredient newIngredient)
    {
        if (!SelectedIngredients.Contains(ing)) return;
        if (SelectedIngredients.Contains(newIngredient)) return;

        var index = SelectedIngredients.IndexOf(ing);
        SelectedIngredients[index] = newIngredient;

        if (ActiveIngredient == ing)
            ActiveIngredient = null;
    }

    public void SelectIngredient(Ingredient ing)
    {
        if (SelectedIngredients.Contains(ing)) return;

        Debug.Log("Selecting ingredient " + ing.IngredientName);

        SelectedIngredients.Add(ing);
    }

    public void SelectIngredient(IngredientItem ingItem)
    {
        SelectIngredient(ingItem.Ingredient);
    }

    public void DeselectIngredient(Ingredient ing)
    {
        if (!SelectedIngredients.Contains(ing)) return;

        Debug.Log("Deselecting ingredient " + ing.name);

        SelectedIngredients.Remove(ing);

        if (ActiveIngredient == ing)
            ActiveIngredient = null;
    }

    public void DeselectIngredient(IngredientItem ingItem)
    {
        DeselectIngredient(ingItem.Ingredient);
    }

    public class ActiveIngredientChangeEvent : UnityEvent<Ingredient>
    {
    }
}