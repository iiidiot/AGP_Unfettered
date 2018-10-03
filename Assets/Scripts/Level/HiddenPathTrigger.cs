using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPathTrigger : MonoBehaviour {

	[SerializeField]
	private GameObject HiddenPath;
	private bool canBeDown = false;
	[SerializeField]
	private float triggerHeight = 1f;

	void Update()
	{
		if(PlayerController.Instance.CanGoDownstair && canBeDown)
		{
			HiddenPath.GetComponent<Collider>().isTrigger = true;
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		// maybe we will use it later;
	}

	void OnCollisionExit(Collision other){
		// maybe we will use it later;
	}

	void OnTriggerEnter(Collider collider)
	{
		canBeDown = true;
	}

	void OnTriggerExit(Collider collider)
	{
		// collider is player and player's feet is higher than trigger
		if(collider.tag == "Player" && collider.GetComponent<Transform>().position.y > (transform.position.y - triggerHeight/2))
		{
				HiddenPath.GetComponent<Collider>().isTrigger = false;
				PlayerController.Instance.CanGoDownstair = false;	
				canBeDown = false;
		}
	}
}
