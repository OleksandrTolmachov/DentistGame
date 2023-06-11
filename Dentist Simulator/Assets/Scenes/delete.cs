using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
