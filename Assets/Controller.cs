using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

[SerializeField]
	private float movementSpeed = 15f;

	//private bool facingRight = true;

	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private float jumpFrouce = 500f;
	[SerializeField]
	
	private Rigidbody myRigibody;

	//private Animator myAnimator;
	public bool Jump{get; set;}

	public bool OnGround{get; set;}
	// Use this for initialization
	void Start () {
		myRigibody = GetComponent<Rigidbody>();
		//myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}

	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		HandleMovement(horizontal);
	}

	private void HandleMovement(float horizontal)
    {


		myRigibody.velocity = new Vector2(horizontal * movementSpeed, myRigibody.velocity.y);

		
		if(Jump){

			OnGround = false;
			myRigibody.AddForce(new Vector2(0, jumpFrouce));
		}
    }
    private void HandleInput()
    {
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump = true;
		}
    }
}
