using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractiveController : MonoBehaviour {

    public GameObject panel;

	// Use this for initialization
	void Start () {
        panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        // todo if tag is player
        panel.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        panel.SetActive(false);
    }
}
