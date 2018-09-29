using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowChange : MonoBehaviour {
    public int flag = 0;
	// Use this for initialization
	void Start () {
        Camera.main.GetComponent<CameraFollowTight>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (flag == 0)
            {
                Camera.main.GetComponent<CameraFollowTight>().enabled = true;
                Camera.main.GetComponent<CameraFollowSmooth>().enabled = false;
            }
            else if(flag == 1)
            {
                Camera.main.GetComponent<CameraFollowTight>().enabled = false;
                Camera.main.GetComponent<CameraFollowSmooth>().enabled = true;
            }
        }
    }
}
