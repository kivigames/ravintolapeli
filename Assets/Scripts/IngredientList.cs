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

    public enum IngredientListType
    {
        All,
        Selected,
        Manual,

        Dry,
        Fridge
    }

    private IngredientManager ingredientManager;

    [SerializeField]
    private IngredientListType listType = IngredientListType.All;

    [SerializeField]
    private bool reverseOrder = false;

    [SerializeField]
    private bool allowDuplicates = false;

    [SerializeField]
    private LayoutGroup listGroup = null;

    [SerializeField]
    private IngredientItem ingredientItemPrefab = null;

    [SerializeField]
    private ListClickMode clickMode = ListClickMode.Select;

    [SerializeField]
    private ItemClickEvent clickAction = new ItemClickEvent();

    private Color originalItemColor;

    public IngredientListType ListType => listType;

    public bool ReverseOrder => reverseOrder;

    public bool AllowDuplicates => allowDuplicates;

    public LayoutGroup ListGroup => listGroup;

    public IngredientItem IngredientItemPrefab => ingredientItemPrefab;

    public ListClickMode ClickMode => clickMode;

    public ItemClickEvent ClickAction => clickAction;

    private void Awake()
    {
        ingredientManager = FindObjectOfType<IngredientManager>();

        originalItemColor = ingredientItemPrefab.GetComponent<Image>().color;
    }

    private void OnEnable()
    {
        if (listType == IngredientListType.Manual) return;

        foreach (var ing in GetIngredientList()) AddIngredient(ing);

        if (listType == IngredientListType.Selected)
        {
            var obs = ingredientManager.SelectedIngredients;
            obs.CollectionChanged += OnIngredientSelectionChanged;
        }

        ingredientManager.OnActiveIngredientChanged.AddListener(SetActiveIngredient);

        SetActiveIngredient(ingredientManager.ActiveIngredient);
    }

    private void OnDisable()
    {
        if (listType == IngredientListType.Manual) return;

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

        ingredientManager.OnActiveIngredientChanged.RemoveListener(SetActiveIngredient);
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

    public void CopyIngredient(IngredientItem ingredientItem)
    {
        AddIngredient(ingredientItem.Ingredient);
    }

    public IngredientItem AddIngredient(Ingredient ing)
    {
        if (!allowDuplicates)
            foreach (Transform child in listGroup.transform)
                if (child.GetComponent<IngredientItem>().Ingredient == ing)
                    return null;

        var ingObject = Instantiate(ingredientItemPrefab, listGroup.transform);
        ingObject.Ingredient = ing;

        if (reverseOrder)
            ingObject.transform.SetAsFirstSibling();

        var btn = ingObject.GetComponent<Button>();

        // Set item's button listener according to current clickMode
        if (clickMode == ListClickMode.Action)
            btn.onClick.AddListener(() => clickAction?.Invoke(ingObject));
        else
            btn.onClick.AddListener(() =>
            {
                if (ingredientManager.ActiveIngredient != ingObject.Ingredient)
                    ingredientManager.ActiveIngredient = ingObject.Ingredient;
                else
                    ingredientManager.ActiveIngredient = null;
            });

        return ingObject;
    }

    public void RemoveIngredient(IngredientItem ingItem)
    {
        if (ingItem)
            Destroy(ingItem.gameObject);
    }

    public void RemoveIngredient(Ingredient ing)
    {
        var item = GetItemByIngredient(ing);
        if (item)
            Destroy(item.gameObject);
    }

    // Used to update ingredient list to match selected ingredients.
    // See: IngredientManager.SelectedIngredients and ObservableCollection.
    private void OnIngredientSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (Ingredient ing in e.NewItems)
                AddIngredient(ing);
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (Ingredient ing in e.OldItems) RemoveIngredient(ing);
        }
        else if (e.Action == NotifyCollectionChangedAction.Replace)
        {
            var old = (Ingredient) e.OldItems[0];
            var newIngredient = (Ingredient) e.NewItems[0];

            var oldItem = GetItemByIngredient(old);
            var oldIndex = oldItem.transform.GetSiblingIndex();

            Destroy(oldItem.gameObject);
            AddIngredient(newIngredient)?.transform.SetSiblingIndex(oldIndex);
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
            case IngredientListType.Manual:
                return null;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    [Serializable]
    public class ItemClickEvent : UnityEvent<IngredientItem>
    {
    }
}