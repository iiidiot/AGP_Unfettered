using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour {

	private bool canBeMoved = false;

	[SerializeField]
	private float movableStoneMass = 100f;

	private float unmovableStoneMass = 100000f;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().mass = unmovableStoneMass;
	}
	
	// Update is called once per frame
	void Update () {
		if(canBeMoved)
		{
			StoneMassChange();
		}
	}


	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			canBeMoved = true;
		}

	}

	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			canBeMoved = false;
		}
	}

	private void StoneMassChange(){

		if(PlayerController.Instance.CanMoveStone == true){
			GetComponent<Rigidbody>().mass = movableStoneMass;
		}
		else
		{
			GetComponent<Rigidbody>().mass = unmovableStoneMass;
		}
		 
	}
}
