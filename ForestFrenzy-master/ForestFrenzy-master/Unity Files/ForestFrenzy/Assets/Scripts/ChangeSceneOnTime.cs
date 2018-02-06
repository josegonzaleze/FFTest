using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnTime : MonoBehaviour {

    public float delay = 5;
    public string NewLevel = "Menu 3D";

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Application.LoadLevel(NewLevelString);
        UnityEngine.SceneManagement.SceneManager.LoadScene(NewLevel);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
