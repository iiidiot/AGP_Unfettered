using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter()
    {
        Camera.main.GetComponent<CameraFollowSmooth>().enabled = false;
    }

    void OnTriggerExit()
    {
        Camera.main.GetComponent<CameraFollowSmooth>().enabled = true;
    }
}
