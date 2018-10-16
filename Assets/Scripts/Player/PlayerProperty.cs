using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private float health = 100; 
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		damage ();
	}

	private void damage (){
		health -= 0.0001f;
		Debug.Log(health);
	}



}
