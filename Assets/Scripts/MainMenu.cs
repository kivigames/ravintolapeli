using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SelectedRecipeStorage selectedRecipeStorage = null;

    public void StartFreePlay()
    {
        selectedRecipeStorage.SelectedRecipe = null;
        SceneManager.LoadScene(1);
    }
}