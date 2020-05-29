using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputSystemUIInputModule))]
public class BackButtonExit : MonoBehaviour
{
    private void Start()
    {
        GetComponent<InputSystemUIInputModule>().cancel.action.performed += Exit;
    }

    private void OnDestroy()
    {
        GetComponent<InputSystemUIInputModule>().cancel.action.performed -= Exit;
    }

    private void Exit(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel pressed, quitting...");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Main Menu
            Application.Quit();
        }
        else
        {
            // Else return to main menu
            SceneManager.LoadScene(0);
        }
    }
}