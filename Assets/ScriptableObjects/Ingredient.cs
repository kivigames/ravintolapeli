using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredient", order = 0)]
public class Ingredient : ScriptableObject
{
    [SerializeField]
    private string ingredientName = null;

    [SerializeField]
    private Sprite sprite = null;

    [SerializeField]
    private StorageType storageType = StorageType.None;

    private bool selected = false;

    [SerializeField]
    private Ingredient choppedVersion = null;

    [SerializeField]
    private Ingredient boiledVersion = null;

    [SerializeField]
    private Ingredient friedVersion = null;

    public string IngredientName => ingredientName;

    public Sprite Sprite => sprite;

    public StorageType StorageType => storageType;

    public bool Selected
    {
        get => selected;
        set => selected = value;
    }

    public Ingredient ChoppedVersion => choppedVersion;

    public Ingredient BoiledVersion => boiledVersion;

    public Ingredient FriedVersion => friedVersion;
}

public enum StorageType
{
    None,
    Dry,
    Fridge
}