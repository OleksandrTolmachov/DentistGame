using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        var objects = Object.FindObjectsOfType<DontDestroyOnLoad>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != this && objects[i].name == gameObject.name)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
