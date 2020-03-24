using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IngredientList : MonoBehaviour
{
    public enum ListClickMode
    {
        Select,
        Action
    }

    private IngredientManager ingredientManager;

    public IngredientListType listType;

    public VerticalLayoutGroup listGroup;

    public IngredientItem ingredientItemPrefab;

    public ListClickMode clickMode = ListClickMode.Select;

    public ItemClickEvent clickAction;

    private Color originalItemColor;

    private void Awake()
    {
        ingredientManager = FindObjectOfType<IngredientManager>();

        if (clickAction == null)
            clickAction = new ItemClickEvent();

        originalItemColor = ingredientItemPrefab.GetComponent<Image>().color;
    }

    private void OnEnable()
    {
        foreach (var ing in GetIngredientList()) AddIngredient(ing);

        if (listType == IngredientListType.Selected)
        {
            var obs = ingredientManager.SelectedIngredients;
            obs.CollectionChanged += OnIngredientSelectionChanged;
        }

        ingredientManager.onActiveIngredientChanged.AddListener(SetActiveIngredient);

        SetActiveIngredient(ingredientManager.ActiveIngredient);
    }

    private void OnDisable()
    {
        if (listType == IngredientListType.Selected)
        {
            var obs = ingredientManager.SelectedIngredients;
            obs.CollectionChanged -= OnIngredientSelectionChanged;
        }

        for (var i = 0; i < listGroup.transform.childCount; i++)
        {
            var ch = listGroup.transform.GetChild(i);
            Destroy(ch.gameObject);
        }

        ingredientManager.onActiveIngredientChanged.RemoveListener(SetActiveIngredient);
    }

    private void SetActiveIngredient(Ingredient ingredient)
    {
        if (clickMode != ListClickMode.Select) return;

        for (var i = 0; i < listGroup.transform.childCount; i++)
        {
            var ch = listGroup.transform.GetChild(i);
            if (ch.GetComponent<IngredientItem>().Ingredient == ingredient)
                ch.GetComponent<Image>().color = Color.cyan;
            else
                ch.GetComponent<Image>().color = originalItemColor;
        }
    }

    private IngredientItem AddIngredient(Ingredient ing)
    {
        var ingObject = Instantiate(ingredientItemPrefab, listGroup.transform);
        ingObject.Ingredient = ing;
        var btn = ingObject.GetComponent<Button>();

        if (clickMode == ListClickMode.Action)
            btn.onClick.AddListener(() => clickAction?.Invoke(ingObject));
        else
            btn.onClick.AddListener(() => ingredientManager.ActiveIngredient = ingObject.Ingredient);

        return ingObject;
    }

    private void OnIngredientSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (Ingredient ing in e.NewItems)
                AddIngredient(ing);
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (Ingredient ing in e.OldItems)
            {
                var item = GetItemByIngredient(ing);
                if (item)
                    Destroy(item.gameObject);
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Replace)
        {
            var old = (Ingredient) e.OldItems[0];
            var newIngredient = (Ingredient) e.NewItems[0];

            var oldItem = GetItemByIngredient(old);
            var oldIndex = oldItem.transform.GetSiblingIndex();

            Destroy(oldItem.gameObject);
            AddIngredient(newIngredient).transform.SetSiblingIndex(oldIndex);
        }
    }

    private IngredientItem GetItemByIngredient(Ingredient ingredient)
    {
        for (var i = 0; i < listGroup.transform.childCount; i++)
        {
            var ch = listGroup.transform.GetChild(i);
            if (ch.GetComponent<IngredientItem>().Ingredient == ingredient)
                return ch.GetComponent<IngredientItem>();
        }

        return null;
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

    [Serializable]
    public class ItemClickEvent : UnityEvent<IngredientItem>
    {
    }
}