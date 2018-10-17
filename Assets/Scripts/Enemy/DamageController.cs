using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
	[SerializeField]
	private string[] enemyTagList;

	private Animator myAnimator;

	private Rigidbody myRigidbody;

	[SerializeField]
	private int Health = 10;
	[SerializeField]
	private int power = 2;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckDeath();
	}

	void OnTriggerEnter (Collider collider) {
		if(CheckEnemy(collider.tag)){
			GetDamage();
		}
	}

	void OnTriggerExit (Collider collider) {
		if(CheckEnemy(collider.tag)){
			myAnimator.SetBool("isHitted", false);
		}
	}

    private void GetDamage()
    {
		Debug.Log("Health:"+Health);
		Health -= power;
        myAnimator.SetBool("isHitted", true);
    }

    private bool CheckEnemy (string colliderTag){
		foreach(string tag in enemyTagList){
			if(colliderTag == tag){
				return true;
			}
		}
		return false;
	}

	private void CheckDeath () {

		 if(Health <= 0){
			 myAnimator.SetTrigger("isDied");
			 gameObject.SetActive(false);
		 }
	}

}
