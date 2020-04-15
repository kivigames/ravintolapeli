using UnityEngine;

[CreateAssetMenu(fileName = "Selected Recipe Store", menuName = "Selected Recipe Store", order = 0)]
public class SelectedRecipeStorage : ScriptableObject
{
    [SerializeField]
    private Recipe selectedRecipe = null;

    public Recipe SelectedRecipe
    {
        get => selectedRecipe;
        set => selectedRecipe = value;
    }
}