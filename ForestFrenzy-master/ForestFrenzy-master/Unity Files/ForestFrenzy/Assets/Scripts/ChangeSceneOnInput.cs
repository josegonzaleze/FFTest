﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnInput : MonoBehaviour {

    public string NewLevel = "Menu 3D";

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NewLevel);
        }
    }
}
