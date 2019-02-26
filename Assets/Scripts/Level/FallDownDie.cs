using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position = GameRunTimeStatus.RebornPlace;
        }
    }

}
