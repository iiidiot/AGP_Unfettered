using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour {

	private bool canBeMoved = false;

	[SerializeField]
	private float movableStoneMass = 100f;

	private float unmovableStoneMass = 100000f;
	// Use this for initialization

	private Rigidbody myRigidbody;
	[SerializeField]
	private float decelerationRate = 0.99f;
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myRigidbody.mass = unmovableStoneMass;
	}
	
	// Update is called once per frame
	void Update () {
		if(canBeMoved)
		{
			StoneMassChange();
		}
		else
		{
			StopStone();
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
			PlayerTestController.Instance.IsPushing = false;
		}
	}

	private void StoneMassChange(){

		if(PlayerTestController.Instance.CanMoveStone == true){
			myRigidbody.mass = movableStoneMass;
			PlayerTestController.Instance.IsPushing = true;
		}
		else
		{
			myRigidbody.mass = unmovableStoneMass;
		}
		 
	}

	private void StopStone()
	{
		if( Mathf.Abs(myRigidbody.velocity.x) >= 0.01)
		{
			
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x * decelerationRate, myRigidbody.velocity.y);
		}
	}
}
