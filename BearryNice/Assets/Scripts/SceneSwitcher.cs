using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	
	public void ChangeScene(string sceneName) {

        Debug.Log("Changing scene to " + sceneName);
        SceneManager.LoadScene(sceneName);
	}
}
