using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public Transform groundDetection;

    public float speed;

    private bool m_isRunning;
    private bool m_isWalking;
    private bool m_isIdling;

    private Rigidbody m_rigidBody;

    private Animator m_animator;
    private bool m_canFlip = true;
    private bool m_facingRight = false;
    private bool m_isTriggered = false;

    private float m_rayLength = 2.0f;

    private bool m_onGround = false;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();

         m_isRunning = false;
         m_isWalking = true;
         m_isIdling = false;

        m_animator.SetBool("isRunning", m_isRunning);
        m_animator.SetBool("isWalking", m_isWalking);
        m_animator.SetBool("isIdling", m_isIdling);
    }

    // Update is called once per frame
    void Update()
    {
        OnWalk();
    }

    public void ActivateTriggerEnterEvent(Collider other)
    {
        // Since it's 2D, if player position of x coordinate is less than the enemy's x coordinates when 
        // the enemy is facing right, or vice versa, then he is behind the enemy.
        if (TargetBehindMe(other.gameObject))
        {
            FlipFacing();
        }

        // once the enemy is flipped to a right direction, we stop flipping the enemy.
        m_canFlip = false;

        UpdateRunningStatus();

        m_isTriggered = true;
    }


    public void ActivateTriggerStayEvent(Collider other)
    {
        UpdateRunningStatus();

        // During the running, if the player is behind the monster,
        // it should turn to the player.
        if (TargetBehindMe(other.gameObject))
        {
            m_canFlip = true;
            FlipFacing();
        }

        if (m_animator.GetBool("isAttacking"))
        {
            Move(0.0f);
        }
        else
        {
            Move(speed);
        }

        m_animator.SetBool("isRunning", m_isRunning);
        m_animator.SetBool("isWalking", m_isWalking);
    }

    public void ActivateTriggerExitEvent()
    {
        m_isTriggered = false;

        m_canFlip = true;

        m_isRunning = false;
        m_isWalking = true;

        m_rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        m_animator.SetBool("isRunning", m_isRunning);
        m_animator.SetBool("isWalking", m_isWalking);
    }

    // Main function for walking behavior.
    private void OnWalk()
    {
       
        RaycastHit hit;
        Ray landingRay = new Ray(groundDetection.transform.position, Vector3.down);

        Debug.DrawRay(groundDetection.transform.position, Vector3.down * m_rayLength);

        m_onGround = Physics.Raycast(landingRay, out hit, m_rayLength);

        // If we cannot detect ground, change direction.
        if (!m_onGround)
        {
            FlipFacing();

            // reset force
            m_rigidBody.velocity = Vector3.zero;
        }

        // If it's on the ground and it's not triggering any targets,
        // then just walk.
        else if (m_onGround && !m_isTriggered)
        {
            float walkSpeed = speed / 2.0f;
            Move(walkSpeed);
        }
    }

    // Helper function to detect if the target is behind me.
    private bool TargetBehindMe(GameObject obj)
    {
        return (m_facingRight && obj.transform.position.x < transform.position.x) ||
               (!m_facingRight && obj.transform.position.x > transform.position.x);
    }

    // Helper function to perform flipping sprite.
    private void FlipFacing()
    {
        if (!m_canFlip)
        {
            return;
        }

        transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);

        m_facingRight = !m_facingRight;
    }

    // Helper function just for walking/running
    private void Move(float currentSpeed)
    {
        if (!m_facingRight)
        {
            m_rigidBody.velocity = new Vector3(-1, 0, 0) * currentSpeed;
        }
        else
        {
            m_rigidBody.velocity = new Vector3(1, 0, 0) * currentSpeed;
        }
    }

    // Helper function to check if the monster is currently attacking.
    // If it's attacking, I should stop running.
    private void UpdateRunningStatus()
    {
        if (!m_animator.GetBool("isAttacking"))
        {
            m_isRunning = true;
            m_isWalking = false;
        }
        else
        {
            m_isRunning = false;
            m_isWalking = false;
        }
    }
}

