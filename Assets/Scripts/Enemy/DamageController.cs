using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
	public string[] enemyTagList;
	public GameObject player;
	private Animator myAnimator;

	private Rigidbody myRigidbody;

	[SerializeField]
	private int Health = 10;
	[SerializeField]
	private int power = 1;



	// Use this for initialization
	void Start () {
		
		myRigidbody = gameObject.GetComponentInParent(typeof(Rigidbody)) as Rigidbody;
		myAnimator = gameObject.GetComponentInParent(typeof(Animator)) as Animator;
		
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider collider) {
		Debug.Log(collider.tag);
		if(CheckEnemy(collider.tag) && !PlayerTestController.instance.playerAttack ){
			
			GetDamage();
		}
	}

    private void GetDamage()
    {
		PlayerStatus.Health -= power;
		Debug.Log("Health:"+PlayerStatus.Health);
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
