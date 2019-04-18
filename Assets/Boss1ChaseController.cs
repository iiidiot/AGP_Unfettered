using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the controller script for the boss chasing player in cave scene part4
public class Boss1ChaseController : MonoBehaviour {

    private static Boss1ChaseController m_instance;
    public static Boss1ChaseController instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<Boss1ChaseController>();
            }
            return m_instance;
        }
    }


    public bool m_IsOnGround;
    private Rigidbody m_rigidbody;

    public float m_gravity = -10;//重力加速度，每秒

    public float x_Velocity;
    public Animator m_animator;

    // Obj collided with the player 
    public List<int> grounds;

    // Use this for initialization
    void Start () {
        m_IsOnGround = false;
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (grounds.Count > 0)
        {
            m_IsOnGround = true;
        }
        else
        {
            m_IsOnGround = false;
        }

        if (!m_IsOnGround)
        {
            m_rigidbody.velocity = new Vector3(x_Velocity, m_rigidbody.velocity.y + m_gravity * Time.fixedDeltaTime, 0);
        }
        else
        {
            m_rigidbody.velocity = new Vector3(x_Velocity, 0, 0);
        }


        m_animator.SetFloat("HorizontalVelocity", Mathf.Abs(m_rigidbody.velocity.x));
        m_animator.SetFloat("VerticalVelocity", m_rigidbody.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + ": " + collision.collider.name);
        if (collision.collider.tag == "Ground")
        {
            grounds.Add(collision.gameObject.GetInstanceID());
            //m_IsOnGround = true;
        }

        if (collision.collider.tag == "Player")
        {
            GameObject part04 = GameObject.Find("SceneRoot/Part04");
            part04.GetComponent<Part04Restart>().DoPart04Restart();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(this.name + ": " + collision.collider.name);
        if (collision.collider.tag == "Ground")
        {
            grounds.Remove(collision.gameObject.GetInstanceID());
            //m_IsOnGround = false;
        }
    }


}
