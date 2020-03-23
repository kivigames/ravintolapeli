using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private string recipeName = null;

    public string RecipeName => recipeName;

    [SerializeField]
    private List<Ingredient> ingredients = new List<Ingredient>();

    public List<Ingredient> Ingredients => ingredients;
}