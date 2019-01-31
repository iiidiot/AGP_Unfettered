using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _debugtemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
