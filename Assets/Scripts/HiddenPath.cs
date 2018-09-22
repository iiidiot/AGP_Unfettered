using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPath : MonoBehaviour {

private Collider[] colliders;

	// the tranform position of the hidden box;
	[SerializeField]
	private float upperPosition = 0.5f;
	void Awake()
	{
		colliders = gameObject.GetComponents<Collider>();
	}

	void Update()
	{
		if(PlayerController.Instance.CanGoDownstair)
		{
			colliders[1].isTrigger = true;
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
		// maybe we will use it later;
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.tag == "Player" && collider.GetComponent<Transform>().position.y > upperPosition)
		{
				colliders[1].isTrigger = false;
				PlayerController.Instance.CanGoDownstair = false;	
		}
	}
}
