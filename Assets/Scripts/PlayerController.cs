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

	private bool facingRight = true;

	[SerializeField]
	private Rigidbody myRigibody;

	private Animator myAnimator;
	public bool Jump{get; set;}

	public bool OnGround{get; set;}

	[SerializeField]
	private Transform[] groundPoints;
	// Use this for initialization
	void Start () {
		myRigibody = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
		setGravity();
	}
	
	// Update is called once per frame
	void Update () {
		HanldeInput();
		// delete it when finalized the jump parameter
		setGravity();
	}

	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		HandleMovement(horizontal);
		OnGround = IsGrounded();
		Flip(horizontal);
		HandleLayer();
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

    private void HandleMovement(float horizontal)
    {
		if(myRigibody.velocity.y < 0){
			myAnimator.SetBool("land", true);
			//myAnimator.SetTrigger("jump");
		}

		myRigibody.velocity = new Vector2(horizontal * movementSpeed, myRigibody.velocity.y);

		myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

		if(Jump && OnGround){

			// physic simulation is not deterministic, so add 1 to the maxheight to offset the error， maybe it's not so precision.
			myRigibody.velocity = new Vector2(horizontal * movementSpeed, Mathf.Sqrt(-2.0f * Physics.gravity.y * (jumpHeight+1)));
			
			myAnimator.SetBool("land", false);
			myAnimator.SetTrigger("jump");
			Jump = false;
			OnGround = false;
		}
    }
    private void HanldeInput()
    {
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump = true;
		}
    }

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
		if(myRigibody.velocity.y <= 0){
			foreach(Transform point in groundPoints){
				if(Physics.Raycast(point.position, -Vector3.up, 2f))
				return true;
			}
		}
		return false;
	}

	private void setGravity(){
		float gravity = (-2 * jumpHeight) / (Mathf.Pow((jumpDuration)/2, 2));
		Physics.gravity = new Vector3(0, gravity, 0);
	}
}
