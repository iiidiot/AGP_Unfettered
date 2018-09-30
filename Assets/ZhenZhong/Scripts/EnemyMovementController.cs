using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public float speed;

    // Attacking
    public float chargeTime;

    // What time it will start the charging
    private float m_startChargeTime;
    private bool m_isRunning;

    private Rigidbody m_rigidBody;

    private Animator m_animator;
    private bool m_canFlip = true;
    private bool m_facingRight = false;

    // gap time to flip.
    private float m_flipTime = 5.0f;

    // When will the next time the enemy will flip.
    private float m_nextFlipChance = 0.0f;

    // prevent using magic number
    private float m_maxChanceToFlipEnemy = 5.0f;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();

        m_isRunning = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // This example is: for every 5 seconds, there is a possibility for the enemy to flip.
		if(Time.time > m_nextFlipChance)
        {
            // Making a random chance for the enemy to flip
            if(UnityEngine.Random.Range(0,10) >= m_maxChanceToFlipEnemy)
            {
                FlipFacing();    
            }

            m_nextFlipChance = Time.time + m_flipTime;
        }
	}

    public void ActivateTriggerEnterEvent(Collider other)
    {
        // Since it's 2D, if player position of x coordinate is less than the enemy's x coordinates when 
        // the enemy is facing right, or vice versa, then he is behind the enemy.
        if ((m_facingRight && other.transform.position.x < transform.position.x) ||
           (!m_facingRight && other.transform.position.x > transform.position.x))
        {
            FlipFacing();
        }

        // once the enemy is flipped to a right direction, we stop flipping the enemy.
        m_canFlip = false;
        m_isRunning = true;
        m_startChargeTime = Time.time + chargeTime;
    }

    public void ActivateTriggerStayEvent()
    {
        if (m_startChargeTime >= Time.time)
        {
            if (!m_facingRight)
            {
                m_rigidBody.AddForce(new Vector3(-1, 0, 0) * speed);
            }
            else
            {
                m_rigidBody.AddForce(new Vector3(1, 0, 0) * speed);
            }

            m_animator.SetBool("isRunning", m_isRunning);
        }
    }

    public void ActivateTriggerExitEvent()
    {
        m_canFlip = true;
        m_isRunning = false;
        m_rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_animator.SetBool("isRunning", m_isRunning);
    }

    private void FlipFacing()
    {
        if (!m_canFlip)
        {
            return;
        }

        transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);

        m_facingRight = !m_facingRight;
    }
}

