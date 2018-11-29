using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterController : MonoBehaviour
{

    public int status = 0;
    public int direction = 0;

    public float speed = 10;

    public Animator m_animator;
	public float m_gravity;
    public Rigidbody r;
    // Use this for initialization
    void Start()
    {
        status = 0;
        r = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("isRun", false);
    }

    void Update()
    {
        if (m_animator.GetBool("isRun"))
		{
            r.velocity = new Vector3(direction * speed, 0, 0);
        }
    }

	void FixedUpdate()
	{
		r.velocity = new Vector3(r.velocity.x, r.velocity.y - m_gravity * Time.fixedDeltaTime, 0);
	}
    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (status == 0)
            {
                m_animator.SetBool("isRun", false);
            }
        }
    }
}
