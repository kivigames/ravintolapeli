using UnityEngine;
using UnityEngine.UI;

public class PrepScreen : GameScreen
{
    public Button chopButton;
    public Button boilButton;
    public Button fryButton;

    private IngredientManager ingredientManager;

    private void Awake()
    {
        Debug.Log("Prep: Awake");
        ingredientManager = FindObjectOfType<IngredientManager>();
    }

    private void OnEnable()
    {
        Debug.Log("Prep: OnEnable");
        SetActiveIngredient(ingredientManager.ActiveIngredient);
        ingredientManager.onActiveIngredientChanged.AddListener(SetActiveIngredient);
    }

    private void OnDisable()
    {
        SetActiveIngredient(null);
        ingredientManager.onActiveIngredientChanged.RemoveListener(SetActiveIngredient);
    }

    private void SetActiveIngredient(Ingredient ingredient)
    {
        if (ingredient == null)
        {
            EnablePrep(boilButton, true);
            EnablePrep(chopButton, true);
            EnablePrep(fryButton, true);
            return;
        }

        EnablePrep(boilButton, CanBoil(ingredient));
        EnablePrep(chopButton, CanChop(ingredient));
        EnablePrep(fryButton, CanFry(ingredient));
    }

    private static void EnablePrep(Button prepBtn, bool enabled)
    {
        prepBtn.gameObject.SetActive(enabled);
    }

    public bool CanBoil(Ingredient ing)
    {
        return ing.BoiledVersion != null && !ingredientManager.IsIngredientSelected(ing.BoiledVersion);
    }

    public bool CanChop(Ingredient ing)
    {
        return ing.ChoppedVersion != null && !ingredientManager.IsIngredientSelected(ing.ChoppedVersion);
    }

    public bool CanFry(Ingredient ing)
    {
        return ing.FriedVersion != null && !ingredientManager.IsIngredientSelected(ing.FriedVersion);
    }

    public void BoilIngredient()
    {
        var active = ingredientManager.ActiveIngredient;
        if (!active) return;
        if (!CanBoil(active)) return;

        ingredientManager.ReplaceIngredient(active, active.BoiledVersion);
    }

    public void ChopIngredient()
    {
        var active = ingredientManager.ActiveIngredient;
        if (!active) return;
        if (!CanChop(active)) return;

        ingredientManager.ReplaceIngredient(active, active.ChoppedVersion);
    }

    public void FryIngredient()
    {
        var active = ingredientManager.ActiveIngredient;
        if (!active) return;
        if (!CanFry(active)) return;

        ingredientManager.ReplaceIngredient(active, active.FriedVersion);
    }
}