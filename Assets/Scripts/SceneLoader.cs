using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Helper method for button actions
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}