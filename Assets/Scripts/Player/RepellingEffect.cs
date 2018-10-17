using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepellingEffect : MonoBehaviour {

	[SerializeField]
	private string[] damageObjectArray;
	private Rigidbody myRigidbody;

	[SerializeField]
	private float repellingforce = 10000f;
	private Animator myAnimator;

	private string damageTrigger = "isDied";

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
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

	void OnTriggerEnter (Collider other) {

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
		myRigidbody.velocity = new Vector3(0, 0, 0);
		myRigidbody.AddForce( direction * repellingforce,myRigidbody.velocity.y, 0);
		PlayerTestController.Instance.Flip(-1 * direction);

		// EnemyMovementController enemyMovementController = damageoBject.gameObject.GetComponent<EnemyMovementController>();
        // enemyMovementController.FacingRight = true;

		//myAnimator.SetTrigger(damageTrigger);
	}

}
