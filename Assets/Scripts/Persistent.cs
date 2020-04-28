using UnityEngine;

public class Persistent : MonoBehaviour
{
    [SerializeField]
    private int id = 0;

    private void Start()
    {
        foreach (var o in FindObjectsOfType<Persistent>())
        {
            if (o != this && o.id == id)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(this);
    }
}