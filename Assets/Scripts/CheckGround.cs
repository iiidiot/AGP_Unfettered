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
		Debug.Log("point:"+PlayerController.Instance.OnGround);
	}
	void OnTriggerExit(Collider collider)
	{
		if(collider.tag == "Ground" )
		{
			PlayerController.Instance.OnGround = false;
		}
	}
}
