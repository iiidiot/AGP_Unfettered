using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			other.GetComponent<Rigidbody>().useGravity = false;
			other.GetComponent<Rigidbody>().velocity = new Vector2(0,0);
			PlayerTestController.Instance.OnLadder = true;
			PlayerTestController.Instance.facingRight = false;
			PlayerTestController.Instance.Flip(1);

		}

	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			//other.gameObject.GetComponent<Rigidbody>().useGravity = true; 
			PlayerTestController.Instance.OnLadder = false;
		}
	}
}
