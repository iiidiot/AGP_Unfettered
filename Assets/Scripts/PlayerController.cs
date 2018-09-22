using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private static PlayerController instance;

	public static PlayerController Instance
	{
		get
		{
			if(instance == null )
			{
				instance = GameObject.FindObjectOfType<PlayerController>();
			}
			return instance;
		}
	}

	[SerializeField]
	private float jumpHeight = 5.0f;

	[SerializeField]
	private float jumpDuration = 0.5f;

	[SerializeField]
	private float movementSpeed = 15f;

	private float offsetValue = 0.01f;
	private bool facingRight = true;

	[SerializeField]
	private Rigidbody myRigibody;

	private Animator myAnimator;

	[SerializeField]
	private float maxVerticalVelocity = 100f; 
	public bool Jump{get; set;}

	public bool OnGround{get; set;}

	public bool CanGoDownstair{get; set;} 

	[SerializeField]
	private Transform[] groundPoints;
	// Use this for initialization
	void Start () {
		myRigibody = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
		SetGravity();
	}
	
	// Update is called once per frame
	void Update () {
		
		HanldeInput();
		// TODO delete it when finalized the jump parameter
		SetGravity();
		CheckVelocity();
		HandleLayer();
		OnGround = IsGrounded();
	}

    private void CheckVelocity()
    {
        if( Mathf.Abs(myRigibody.velocity.y) > maxVerticalVelocity){
			myRigibody.velocity = new Vector2(myRigibody.velocity.x, myRigibody.velocity.y > 0 ? maxVerticalVelocity : -1 * maxVerticalVelocity );
		}
    }

    void FixedUpdate () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		HandleMovement(horizontal, vertical);
		Flip(horizontal);
	}


    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal <0 && facingRight)
		{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
    }

	/*
	parameter: horizontal and vertical input 
	 */
    private void HandleMovement(float horizontal, float vertical)
    {
		if(myRigibody.velocity.y < 0){
			myAnimator.SetBool("land", true);
			//myAnimator.SetTrigger("jump");
		}

		myRigibody.velocity = new Vector2(horizontal * movementSpeed, myRigibody.velocity.y);

		myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
		Debug.Log("OnGround:"+OnGround);
		if(Jump && OnGround){

			// physic simulation is not deterministic, so add 1 to the maxheight to offset the error， maybe it's not so precision.
			myRigibody.velocity = new Vector2(horizontal * movementSpeed, Mathf.Sqrt(-2.0f * Physics.gravity.y * (jumpHeight+1)));
			
			myAnimator.SetBool("land", false);
			myAnimator.SetTrigger("jump");
			Jump = false;
			OnGround = false;
		}

		if(vertical < 0){
			CanGoDownstair = true;
		}
    }
    private void HanldeInput()
    {
		//TODO figure out a better to solve the jump problem
		if(Input.GetKey(KeyCode.Space)){
			Jump = true;
		}
	
		if(Input.GetKeyUp(KeyCode.Space)){
			Jump = false;
		}
    }

	// change to air layer when player jump
	private void HandleLayer()
	{
		if(!OnGround)
		{
			myAnimator.SetLayerWeight(1,1);
		}else
		{
			myAnimator.SetLayerWeight(1,0);
		}
	}

	private bool IsGrounded()
	{
		foreach(Transform point in groundPoints)
		{
			if(Physics.Raycast(point.position, Vector3.down, 0.5f))
			return true;
		}
		 return false;
	}

	private void SetGravity(){

		float gravity = (-2 * jumpHeight) / (Mathf.Pow((jumpDuration)/2, 2));
		Physics.gravity = new Vector3(0, gravity, 0);
	}
}
