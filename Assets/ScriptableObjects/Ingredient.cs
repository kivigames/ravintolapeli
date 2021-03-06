using GameToolkit.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredient", order = 0)]
public class Ingredient : ScriptableObject
{
    [SerializeField]
    private LocalizedText ingredientName = null;

    [SerializeField]
    private Sprite sprite = null;

    [SerializeField]
    private StorageType storageType = StorageType.None;

    [SerializeField]
    private Ingredient choppedVersion = null;

    [SerializeField]
    private Ingredient boiledVersion = null;

    [SerializeField]
    private Ingredient friedVersion = null;

    public LocalizedText IngredientName => ingredientName;

    public Sprite Sprite => sprite;

    public StorageType StorageType => storageType;

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