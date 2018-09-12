using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		
		if(collision.gameObject.CompareTag("Ground"))
		{
			PlayerController.Instance.OnGround = true;
		}
		Debug.Log("point:"+PlayerController.Instance.OnGround);
	}
	void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.CompareTag("Ground"))
		{
			PlayerController.Instance.OnGround = false;
		}
	}
}
