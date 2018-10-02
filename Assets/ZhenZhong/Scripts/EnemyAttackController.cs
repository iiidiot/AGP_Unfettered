using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public float attackRange;

    // one attack every 3 seconds
    public float attackCD = 3.0f;

    private Animator m_animator;

    private float m_timer = 0.0f;

    private GameObject m_target;

    private bool m_isAttacking = false;
    private bool m_isInRange = false;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_target)
        {
            float distance = Vector3.Distance(transform.position,
                                              m_target.transform.position);

            if (distance < attackRange)
            {
                m_isAttacking = true;
                m_timer = attackCD;
            }
            else
            {
                m_isAttacking = false;
            }

            if(m_isAttacking)
            {
               if(m_timer > 0.0f)
               {
                    m_timer -= Time.deltaTime;
               }
               else
               {
                    m_isAttacking = false;
               }
            }

            m_animator.SetBool("isAttacking", m_isAttacking);
        }
    }
}
