using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredient", order = 0)]
public class Ingredient : ScriptableObject
{
    [SerializeField]
    private string ingredientName = null;

    [SerializeField]
    private Sprite sprite = null;

    [SerializeField]
    private StorageType storageType = StorageType.Dry;

    private bool selected;

    public string IngredientName => ingredientName;

    public Sprite Sprite => sprite;

    public StorageType StorageType => storageType;

    public bool Selected
    {
        get => selected;
        set => selected = value;
    }
}

public enum StorageType
{
    Dry,
    Fridge
}