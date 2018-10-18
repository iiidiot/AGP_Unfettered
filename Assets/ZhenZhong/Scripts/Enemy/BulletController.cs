using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float destroyTime;

    private Rigidbody m_rigidBody;
    private bool m_facingRight;
    
    public bool FacingRight
    {
        get
        {
            return m_facingRight;
        }

        set
        {
            m_facingRight = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();      
	}

    // Helper function just for moving the bullet
    private void Move()
    {
        if (!m_facingRight)
        {
            m_rigidBody.velocity = new Vector3(-1, 0, 0) * speed;
        }
        else
        {
            m_rigidBody.velocity = new Vector3(1, 0, 0) * speed;
        }

        // Two ways to destroy myself
        // 1. hit an object
        // 2. certain time frame has passed
        Destroy(gameObject, destroyTime);
    }

    // Two ways to destroy myself
    // 1. hit an object
    // 2. certain time frame has passed
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy")
        {
            Destroy(gameObject);
            PlayerStatus.health -= 1;
        } 
    }
}
