using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("i am ready");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	
	void OnTriggerEnter (Collider collider) {
		Debug.Log("something collide:"+collider.tag);
		if(collider.tag == "Weapon"){
			Debug.Log("weapon touched");
		}
	}

	void OnCollisionEnter (Collision collision) 
	{
		Debug.Log("no triiger:"+collision.gameObject.tag);
	}
}
