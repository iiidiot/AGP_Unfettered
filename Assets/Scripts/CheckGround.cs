using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {


	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.tag == "Ground" )
		{
			PlayerController.Instance.OnGround = true;
		}
	}
	void OnTriggerExit(Collider collider)
	{
		if(collider.tag == "Ground" )
		{
			PlayerController.Instance.OnGround = false;
		}
	}
}
