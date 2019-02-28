using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TiggerIntoCG : MonoBehaviour {
	public PlayableDirector pd;
	public GameObject hintUI;
	public bool canPressF = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(canPressF && Input.GetKeyDown(KeyCode.F))
		{
			pd.Play();
		}		
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
        	hintUI.SetActive(true);
            canPressF = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
        	hintUI.SetActive(false);
            canPressF = false;
        }
    }
}
