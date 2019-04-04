using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestory : MonoBehaviour {

    public GameObject fragments;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Instantiate(fragments, transform.position, transform.rotation);
            Destroy(gameObject);
        }
          
    }

}
