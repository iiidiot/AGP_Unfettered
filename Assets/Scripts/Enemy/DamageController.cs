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
	private int power = 1;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider collider) {
		if(CheckEnemy(collider.tag)){
			GetDamage();
		}
	}

    private void GetDamage()
    {
		Debug.Log("Health:"+PlayerStatus.Health);
		PlayerStatus.Health -= power;
		if(PlayerStatus.Health <= 0)
		{
			 myAnimator.SetTrigger("isDying");
		}
		else{
			myAnimator.SetTrigger("isDamaged");
		}
        
    }

    private bool CheckEnemy (string colliderTag){
		foreach(string tag in enemyTagList){
			if(colliderTag == tag){
				return true;
			}
		}
		return false;
	}
}
