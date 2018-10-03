using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    // This is the range when the enemy should attack.
    public float attackRadius;

    // one attack every 3 seconds
    public float attackCD = 0.5f;

    private Animator m_animator;

    private float m_timer = 0.0f;

    private GameObject m_target;

    private bool m_isAttacking;
    private bool m_hasAttacked;

    private bool m_isRunning;
    private bool m_isIdling;

    private float m_currentDistance;

    public GameObject Target
    {
        get
        {
            return m_target;
        }

        set
        {
            m_target = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_isAttacking = false;
        m_hasAttacked = false;

        m_currentDistance = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_target)
        {
            m_currentDistance = Vector3.Distance(transform.position, 
                                                 m_target.transform.position);

            if (m_currentDistance < attackRadius)
            {
                // This is to make sure between each attack there will be cool down delay.
                if(!m_hasAttacked)
                {
                    m_isAttacking = true;

                    m_isIdling = false;
                    m_isRunning = false;
                    
                    // Set it to tell the current attack has been just performed,
                    // need to wait for its CD to finish.
                    m_hasAttacked = true;
                }
                else
                {
                    m_isAttacking = false;

                    m_isIdling = true;
                    m_isRunning = false;

                    m_timer += Time.deltaTime;

                    if(m_timer >= attackCD)
                    {
                        m_timer = 0.0f;
                        m_hasAttacked = false;
                    }
                }
                
                m_animator.SetBool("isAttacking", m_isAttacking);
                m_animator.SetBool("isIdling", m_isIdling);
                m_animator.SetBool("isRunning", m_isRunning);
            }
            else
            {
                m_isIdling = false;
                m_isRunning = true;

                m_animator.SetBool("isIdling", m_isIdling);
                m_animator.SetBool("isRunning", m_isRunning);
            }
        }
    }
}
