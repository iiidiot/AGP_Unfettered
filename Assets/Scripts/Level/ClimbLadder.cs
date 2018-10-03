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


	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Rigidbody>().useGravity = false;
			other.gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0,0);
			PlayerController.Instance.OnLadder = true;

		}

	}

	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Rigidbody>().useGravity = true;
			PlayerController.Instance.OnLadder = false;
		}
	}
}
