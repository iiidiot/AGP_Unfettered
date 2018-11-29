using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIController : MonoBehaviour
{

    // This script is for ground moving spider.

    public enum MonsterStates
    {
        IDLE, CHASE_PLAYER, ATTACK, HURT, DEAD
    }


    public double hp = 10;
    public double attack = 1;
    public float speed = 0.1f;

    public GameObject objectRoot;

    public GameObject alertRange;

    public GameObject startPosition;
    public GameObject endPosition;

    public float repellFacter = 10000000000000;

    private bool m_isInCombatRange;


    private MonsterStates m_state = MonsterStates.IDLE;
    private Rigidbody m_rigidbody;

    private Collider m_alertCollider;

    private Collider m_outCombatCollider;

    private Transform m_playerTransform;
    private Vector3 targetPosition;



    private static float CDTime;
    private static float repellT;

    private bool m_isPlayerInAttackRange;
    private bool m_isAttackLocked;

    private Animator m_animator;

    private GameObject currentAttacker;


    // animator parameter names
    const string k_isAttack = "isAttack";
    const string k_isDead = "isDead";
    const string k_isHurt = "isHurt";


    // Use this for initialization
    void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_alertCollider = alertRange.GetComponent<BoxCollider>();
        m_playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;
        m_animator = GetComponent<Animator>();

        m_state = MonsterStates.IDLE;
        m_isInCombatRange = true;

        m_isAttackLocked = false;

        this.transform.position = startPosition.transform.position;
        targetPosition = endPosition.transform.position;
        m_rigidbody.velocity = new Vector3(speed, 0, 0);

        repellT = 0;

    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_state);

        if (hp <= 0)
        {
            m_state = MonsterStates.DEAD;
            m_animator.SetBool(k_isDead, true);
        }

        if (m_state == MonsterStates.HURT)
        {

            m_animator.SetBool(k_isHurt, true);
            AnimatorStateInfo animatorInfo;
            animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("0");
            //if ((animatorInfo.normalizedTime < 1.0f) && (animatorInfo.IsName("Hurt"))) //  animation playing
            //{
              
                if (currentAttacker)
                {
                Debug.Log("1");
                    Vector3 dir = (this.transform.position - currentAttacker.transform.position).normalized  ;
                    //m_rigidbody.AddForce(dir * repellFacter);
                    Debug.Log("reppeeeeellllllllllll");
                    m_rigidbody.velocity = Vector3.Lerp(dir * speed * 20f, new Vector3(0, 0, 0), repellT);
                    repellT += Time.deltaTime/ 0.1f;

                }

            //}
            if (repellT > 1f) //  animation ends
            {
                repellT = 0.0f;
                currentAttacker = null;
                m_animator.SetBool(k_isHurt, false);
                m_state = MonsterStates.IDLE;
            }
        }
        else
        {
            GoTowardTarget();

            if (m_state == MonsterStates.DEAD)
            {
                targetPosition = this.transform.position; // stay still
                m_rigidbody.velocity = new Vector3(0, 0, 0);

                m_animator.SetBool(k_isAttack, false);

                // after death animation
                AnimatorStateInfo animatorInfo;
                animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);

                if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("Death"))) // attack animation ends
                {

                    if (objectRoot.activeInHierarchy)
                    {
                        objectRoot.SetActive(false);
                        GameObject g = Instantiate(Resources.Load("Prefabs/Effects/Particles/SpiderDeathParticle"), this.transform.position+new Vector3(0,1,0), Quaternion.identity) as GameObject;
                        Destroy(g, 5);
                    }

                }
            }

            if (m_state == MonsterStates.IDLE)    // go around
            {

                m_animator.SetBool(k_isAttack, false);
                m_animator.SetBool(k_isHurt, false);

                AdjustFacingRotation();
                // spider only move in x direction

                if (transform.position.x >= endPosition.transform.position.x)
                {
                    targetPosition = startPosition.transform.position;
                }

                if (transform.position.x <= startPosition.transform.position.x)
                {
                    targetPosition = endPosition.transform.position;
                }


            }

            if (m_state == MonsterStates.CHASE_PLAYER)
            {
                m_animator.SetBool(k_isAttack, false);
                m_animator.SetBool(k_isDead, false);

                AdjustFacingRotation();
                targetPosition = m_playerTransform.position;

                if (!m_isPlayerInAttackRange) // A不到player
                {
                    if (!m_isInCombatRange) // 出追人范围了
                    {
                        m_state = MonsterStates.IDLE;
                    }
                }
            }

            if (m_state == MonsterStates.ATTACK)
            {

                // movement control
                if (transform.position.x < m_playerTransform.position.x) // player is on the right -> face right
                {
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                targetPosition = this.transform.position;


                //m_animator.SetBool(k_isIdle, false);
                m_animator.SetBool(k_isAttack, true);

                // cannot move before animator ends
                AnimatorStateInfo animatorInfo;
                animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);

                if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("Attack"))) // attack animation ends
                {
                    //Debug.Log(animatorInfo.normalizedTime);
                    if (!m_isPlayerInAttackRange) // A不到player
                    {
                        if (!m_isInCombatRange) // 出追人范围了
                        {
                            m_state = MonsterStates.IDLE;
                        }
                        else  // otherwise try to chase player
                        {
                            m_state = MonsterStates.CHASE_PLAYER;
                        }
                    }
                    else
                    {
                        // animator stays in attack state
                        if (!m_isAttackLocked)
                        {
                            // do attack
                            m_playerTransform.GetComponent<PlayerTestController>().GetDamage(attack);
                            // lock
                            m_isAttackLocked = true;
                            CDTime = 0;
                        }

                    }

                }


                if (m_isAttackLocked)
                {
                    CDTime += Time.deltaTime;
                    if (CDTime >= 0.5f) // after 0.5s release attack lock
                    {
                        m_isAttackLocked = false;
                    }
                }

            }

        }
    }


    void GoTowardTarget()
    {
        if (transform.position.x < targetPosition.x) // go right
        {
            m_rigidbody.velocity = new Vector3(speed, 0, 0);
        }

        if (transform.position.x > targetPosition.x) // go left
        {
            m_rigidbody.velocity = new Vector3(-speed, 0, 0);
        }

    }

    void AdjustFacingRotation()
    {
        if (Mathf.Abs(targetPosition.x - this.transform.position.x) > 0.1)
        {

            if (m_rigidbody.velocity.x >= 0) // going right -> face right
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }

    }


    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        if (startPosition && endPosition)
        {
            Gizmos.DrawLine(startPosition.transform.position, endPosition.transform.position);
        }
    }



    public void SetStatus(MonsterStates state)
    {
        m_state = state;
    }

    public void GetAttack(double damage, GameObject attacker)
    {
        hp -= damage;
        m_state = MonsterStates.HURT;
        currentAttacker = attacker;
    }


    void OnTriggerStay(Collider collider)
    {
        // if player stays in spiders attack range, the spider is locked in attack state
        if (collider.tag == "Player")
        {
            m_isPlayerInAttackRange = true;
            //Debug.Log("player stays trigger");
            m_state = MonsterStates.ATTACK;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // when player touches the spider, it must immediately cast damage, even if its damage skill is in CD 
        if (collider.tag == "Player")
        {
            m_isPlayerInAttackRange = true;
            //Debug.Log("player enter trigger");
            m_state = MonsterStates.ATTACK;

        }

        if (collider.tag == "Sword")
        {
            GetAttack(PlayerStatus.Attack, m_playerTransform.gameObject);
        }

        if (collider.tag == "Fu")
        {
            GetAttack(10, m_playerTransform.gameObject);
        }

    }

    void OnTriggerExit(Collider collider)
    {
        // if player leaves 
        if (collider.tag == "Player")
        {
            // if out of "out of combat" range, try to go back

            m_isPlayerInAttackRange = false;

        }
    }

    public void SetIsInCombatRange(bool isInCombatRange)
    {
        this.m_isInCombatRange = isInCombatRange;
    }
}
