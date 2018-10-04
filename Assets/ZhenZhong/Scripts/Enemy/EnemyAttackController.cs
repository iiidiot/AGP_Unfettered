using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum AttackType
{
    Tackle,
    //Projectile,
    Shoot
}

public class EnemyAttackController : MonoBehaviour
{
    public float angle;

    public AttackType attackType;
    public Transform spawnPoint;
    public GameObject bullet;
    
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

    private bool m_isFiring;

    private float m_currentDistance;

    private bool m_facingRight;

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
                switch (attackType)
                {
                    case AttackType.Tackle:
                        OnTackle();
                        break;
                    case AttackType.Shoot:
                        OnFire();
                        break;
                    //case AttackType.Projectile:
                    //    OnThrowWeapon();
                    //    break;
                    default:
                        break;
                }
            }
            else
            {
                OnStopAttack();
            }
        } 
    }
    
    // Main function for shooting attack.
    private void OnFire()
    {
        if (!m_hasAttacked)
        {
            // TODO: For this example, since we don't have shooting animation, I use Hit animation instead.
            m_isFiring = true;

            m_isIdling = false;
            m_isRunning = false;

            Fire();

            // Set it to tell the current attack has been just performed,
            // need to wait for its CD to finish.
            m_hasAttacked = true;
        }
        else
        {
            OnCoolDown();
        }

        m_animator.SetBool("isFiring", m_isFiring);
        m_animator.SetBool("isIdling", m_isIdling);
        m_animator.SetBool("isRunning", m_isRunning);
    }

    // Helper function for firing the bullet
    private void Fire()
    {
        GameObject currentBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

        BulletController bulletController = currentBullet.GetComponent<BulletController>();
        bulletController.FacingRight = m_facingRight;
    }

    //private void OnThrowWeapon()
    //{
    //    CalculateProjectileResult();
    //}

    ////private Vector3 CalculateProjectileResult()
    ////{
    ////    Vector3 direction = m_target.transform.position - transform.position;

    ////    // get the height difference
    ////    float height = direction.y;

    ////    // retain only the horizontal direction
    ////    direction.y = 0;

    ////    // get the horizontal distance
    ////    float distance = direction.magnitude;

    ////    float angleInRadian = angle * Mathf.Deg2Rad;

    ////    // set direction to the elevation angle
    ////    direction.y = distance * Mathf.Tan(angleInRadian);

    ////    // correct for small height differences
    ////    distance += (height / Mathf.Tan(angleInRadian));

    ////    // calculate the velocity magnitude
    ////    float currentVelocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angleInRadian));

    ////    return currentVelocity * direction.normalized;
    ////}

    // Main function for tackle attack.
    private void OnTackle()
    {
        // This is to make sure between each attack there will be cool down delay.
        if (!m_hasAttacked)
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
            OnCoolDown();
        }

        m_animator.SetBool("isAttacking", m_isAttacking);
        m_animator.SetBool("isIdling", m_isIdling);
        m_animator.SetBool("isRunning", m_isRunning);
    }

    private void OnStopAttack()
    {
        m_isIdling = false;
        m_animator.SetBool("isIdling", m_isIdling);
    }

    private void OnCoolDown()
    {
        m_isAttacking = false;
        m_isFiring = false;

        m_isIdling = true;
        m_isRunning = false;

        m_timer += Time.deltaTime;

        if (m_timer >= attackCD)
        {
            m_timer = 0.0f;
            m_hasAttacked = false;
        }
    }
}
