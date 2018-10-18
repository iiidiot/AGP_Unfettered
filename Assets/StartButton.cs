using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.GetComponent<Button>().onClick.AddListener(LoadNextScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadNextScene()
    {
        SceneManager.LoadScene("loading");
    }
}
