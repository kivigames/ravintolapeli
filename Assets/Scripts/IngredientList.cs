using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class IngredientList : MonoBehaviour
{
    private IngredientManager ingredientManager;

    public IngredientListType listType;

    public VerticalLayoutGroup listGroup;

    public IngredientItem ingredientItemPrefab;

    private void Awake()
    {
        ingredientManager = FindObjectOfType<IngredientManager>();
    }

    private void Start()
    {
        foreach (var ing in GetIngredientList())
        {
            var ingObject = Instantiate(ingredientItemPrefab, listGroup.transform);
            ingObject.Ingredient = ing;
        }

        if (listType == IngredientListType.Selected)
        {
            var obs = ingredientManager.SelectedIngredients;
            obs.CollectionChanged += OnIngredientSelectionChanged;
        }
    }

    private void OnIngredientSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
            foreach (Ingredient ing in e.NewItems)
            {
                var ingObject = Instantiate(ingredientItemPrefab, listGroup.transform);
                ingObject.Ingredient = ing;
            }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
            foreach (Ingredient ing in e.OldItems)
                for (var i = 0; i < listGroup.transform.childCount; i++)
                {
                    var ch = listGroup.transform.GetChild(i);
                    if (ch.GetComponent<IngredientItem>().Ingredient == ing)
                        Destroy(ch.gameObject);
                }
    }

    private IEnumerable<Ingredient> GetIngredientList()
    {
        switch (listType)
        {
            case IngredientListType.All:
                return ingredientManager.AllIngredients;
            case IngredientListType.Selected:
                return ingredientManager.SelectedIngredients;
            case IngredientListType.Dry:
                return ingredientManager.DryIngredients;
            case IngredientListType.Fridge:
                return ingredientManager.FridgeIngredients;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}