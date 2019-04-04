using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileStone : MonoBehaviour {


    public GameObject fragments;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Invoke("DestoryStone", 0);
        }

    }



    public void DestoryStone()
    {
        Instantiate(fragments, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
