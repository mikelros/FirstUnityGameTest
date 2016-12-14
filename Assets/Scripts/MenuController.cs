using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene("main");

    }

    public void Exit()
    {
        Application.Quit();
    }
}
