using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void Do()
    {
        //this.gameObject.SetActive(false);
        this.transform.Find("FireWallEffect").GetComponent<ParticleSystem>().Stop();
        this.GetComponent<BoxCollider>().enabled = false;
    }

}
