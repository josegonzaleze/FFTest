using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {


	public void LoadByIndex(string sceneIndex)
	{
		//SceneManager.LoadScene (sceneIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

    }
}
