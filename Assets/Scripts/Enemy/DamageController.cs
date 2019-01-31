using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
	public string[] enemyTagList;
	public GameObject player;
	private Animator myAnimator;

	[SerializeField]
	private int Health = 10;
	[SerializeField]
	private int power = 1;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.zero;
	}

	void OnTriggerEnter (Collider collider) {
		//Debug.Log(collider.tag);
		if(CheckEnemy(collider.tag)){
			
			GetDamage();
		}
	}

    private void GetDamage()
    {
		myAnimator = gameObject.GetComponentInParent(typeof(Animator)) as Animator;
		PlayerStatus.Health -= power;
		Debug.Log("Health:"+PlayerStatus.Health);
		if(PlayerStatus.Health <= 0)
		{
			myAnimator.SetTrigger("isDying");
		}
		else{
			Debug.Log(myAnimator == null);
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
