using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont_Destroy : MonoBehaviour
{
    public AudioSource bg;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Application.loadedLevel == 1 || Application.loadedLevel == 2)
            Destroy(gameObject);
    }
}