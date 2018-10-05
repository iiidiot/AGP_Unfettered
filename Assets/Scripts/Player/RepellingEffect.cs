using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepellingEffect : MonoBehaviour {

	[SerializeField]
	private string[] damageObjectArray;
	private Rigidbody myRigidbody;

	[SerializeField]
	private float repellingforce = 10000f;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody>();
	}

	// void OnCollisionEnter (Collision other){
	// 	Debug.Log(other.gameObject.tag);
	// 	foreach( string damageObjectTag in damageObjectArray){
	// 		if(other.gameObject.tag == damageObjectTag){
	// 			GetDamage(other.transform);
	// 			return;
	// 		}
	// 	}
	// }

	void OnTriggerEnter (Collider other){
		Debug.Log(other.gameObject.tag);
		foreach( string damageObjectTag in damageObjectArray){
			if(other.gameObject.tag == damageObjectTag){
				GetDamage(other);
				return;
			}
		}
	}

	private void GetDamage ( Collider damageoBject ){
		// can not use position cos there is error by unity 
		float direction = (damageoBject.GetComponent<Rigidbody>().velocity.x) > 0 ? 1 : -1;
		myRigidbody.AddForce( direction * repellingforce,myRigidbody.velocity.y, 0);
		PlayerController.Instance.Flip(-1 * direction);
			
	}

}
