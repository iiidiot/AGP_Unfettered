using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float movementSpeed = 15f;

	private bool facingRight = true;

	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private float jumpFrouce = 500f;
	[SerializeField]
	private LayerMask whatisGround;
	private Rigidbody2D myRigibody;

	private bool jump;

	private bool onGround;

	[SerializeField]
	private Transform[] groundPoints; 
	// Use this for initialization
	void Start () {
		myRigibody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		HanldeInput();
	}

	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		onGround = IsGrounded();
		HandleMovement(horizontal);
		Flip(horizontal);
		ResetParameter();
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
		myRigibody.velocity = new Vector2(horizontal * movementSpeed, myRigibody.velocity.y);

		if(jump && myRigibody.velocity.y == 0){
			onGround = false;
			myRigibody.AddForce(new Vector2(0, jumpFrouce));
		}
    }

    private bool IsGrounded()
    {
		if(myRigibody.velocity.y <= 0)
		{
			foreach(Transform point in groundPoints)
			{
				Collider2D[] collider = Physics2D.OverlapCircleAll(point.position, groundRadius, whatisGround);
				foreach(Collider2D colliderItem in collider){
					if(colliderItem.gameObject != gameObject){
						return true;
					}
				}
			}	
		}
		return false;
    }

    private void HanldeInput()
    {
		if(Input.GetKeyDown(KeyCode.Space)){
			jump = true;
		}
    }

	private void ResetParameter(){
		jump = false;
	}
}
