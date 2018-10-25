using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        effect.SetActive(false);
        this.GetComponent<Button>().onClick.AddListener(LoadNextScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject effect;
    //public Transform p;

    private void LoadNextScene()
    {
        //GameObject go = Instantiate(effect, p.position, p.rotation);

        effect.SetActive(true);
        effect.GetComponent<ParticleSystem>().Play();

        Invoke("RealLoad", 0.5f);
    }

    private void RealLoad()
    {
        SceneManager.LoadScene("loading");
    }
}
