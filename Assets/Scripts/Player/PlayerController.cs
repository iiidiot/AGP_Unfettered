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
	private float maxClimbSpeed = 2f;

	[SerializeField]
	private float movementSpeed = 15f;

	//private float offsetValue = 0.01f;
	public bool facingRight{get; set;}

	[SerializeField]
	private Rigidbody myRigibody;

	private Animator myAnimator;

	[SerializeField]
	private float maxVerticalVelocity = 100f; 
	public bool Jump{get; set;}

	public bool CanMoveStone{get; set;}

	public bool OnLadder{get; set;}

	public bool OnGround{get; set;}

	public bool CanGoDownstair{get; set;} 

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private Transform[] startPlace;

	[SerializeField]
	private int startPlaceNumber = 0;
	// Use this for initialization

	private LayerMask enemyLayerMask = (1 << 9) | (1 << 10);

	void Awake ()
    {
		// set the start place
		if(startPlace.Length != 0)
		{
			transform.parent.position = startPlaceNumber < startPlace.Length ? startPlace[startPlaceNumber].transform.position : startPlace[0].transform.position;
		}
	}
	void Start ()
    {
		myRigibody = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
		SetGravity();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
		HandleInput();
		// TODO delete it when finalized the jump parameter
		SetGravity();
		CheckVelocity();
		HandleLayer();
		OnGround = IsGrounded();
	}

    private void CheckVelocity()
    {
        if( Mathf.Abs(myRigibody.velocity.y) > maxVerticalVelocity)
        {
			myRigibody.velocity = new Vector2(myRigibody.velocity.x, myRigibody.velocity.y > 0 ? maxVerticalVelocity : -1 * maxVerticalVelocity );
		}
    }

    void FixedUpdate () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		HandleMovement(horizontal, vertical);
		Flip(horizontal);
		resetParameter();
	}


    public void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal <0 && facingRight)
		{
			facingRight = !facingRight;
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);
        }
    }

	/*
	parameter: horizontal and vertical input 
	 */
    private void HandleMovement(float horizontal, float vertical)
    {
		if(myRigibody.velocity.y < 0)
        {
			myAnimator.SetBool("land", true);
			//myAnimator.SetTrigger("jump");
		}

		myRigibody.velocity = new Vector2(horizontal * movementSpeed, myRigibody.velocity.y);

		myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
		// check velocity.y to avoid double jump at the edge
		if(Jump && OnGround && myRigibody.velocity.y < 0.1f)
        {
			SoundController.PlaySound(2);
			// physic simulation is not deterministic, so add 1 to the maxheight to offset the error， maybe it's not so precision.
			myRigibody.velocity = new Vector2(horizontal * movementSpeed, Mathf.Sqrt(-2.0f * Physics.gravity.y * (jumpHeight+1)));
			
			myAnimator.SetBool("land", false);
			myAnimator.SetTrigger("jump");
			Jump = false;
			OnGround = false;
		}

		if(vertical < 0)
        {
			CanGoDownstair = true;
		}

		if(OnLadder)
        {
			myRigibody.velocity = new Vector2(horizontal * maxClimbSpeed, vertical * maxClimbSpeed);
		}
    }
    private void HandleInput()
    {
		//TODO figure out a better to solve the jump problem
		if(Input.GetKey(KeyCode.Space))
        {
			Jump = true;
		}

		if(Input.GetKey(KeyCode.F))
        {
			CanMoveStone = true;
		}


        if (Input.GetKey(KeyCode.S)) // camera shake
        {
            // camera shake
            Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            mainCamera.GetComponent<CameraFollowSmooth>().CameraShake(0.1f);
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
			//enemyLayerMask = ~enemyLayerMask;
			if(point && Physics.Raycast(point.position, Vector3.down, 0.5f, enemyLayerMask.value))
            {
				return true;
			}
				
		}
		 return false;
	}

	private void SetGravity()
    {

		float gravity = (-2 * jumpHeight) / (Mathf.Pow((jumpDuration)/2, 2));
		Physics.gravity = new Vector3(0, gravity, 0);
	}

	private void resetParameter()
	{
		CanMoveStone = false;
		Jump = false;
	}
}
