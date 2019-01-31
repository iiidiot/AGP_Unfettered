﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AIController : MonoBehaviour {



    public enum Boss1States
    {
        IDLE, CHASE_PLAYER, ATTACK_AOE, ATTACK_FIRE, ATTACK_MELEE, HURT, DEAD
    }

    public enum Boss1Skills
    {
        AOE, FIRE, MELEE
    }

    public double maxHP = 10;
    public double hp = 10;
    public double attackAOE = 1;
    public double attackFire = 1;
    public double attackMelee = 1;
    public float speed = 3f;
    public float fastSpeed = 1f;
    public float rushSpeed = 10f;

    public GameObject idlePosition;
    public GameObject objectRoot;

    public Transform firePosition; // tongue01

    private Boss1States m_state = Boss1States.IDLE;
    private Rigidbody m_rigidbody;
    private Collider m_alertCollider;

    private bool m_isPlayerInAlertRange;

    private bool m_isPlayerInAOERange;
    private bool m_isPlayerInFireRange;
    private bool m_isPlayerInMeleeRange;

    private Boss1Skills m_nextSkill;
    private Transform m_playerTransform;

    private static float rushTimer;
    private static float fireChaseTimer;
    private static float idleTimer;



    private bool m_isAttackMeleeLocked;
    private bool m_isAttackAOELocked;
    private bool m_isAttackFireLocked;
    private Animator m_animator;


    // animator parameter names
    const string k_isWalking = "isWalking";
    const string k_isAttackAOE = "isAttackAOE";
    const string k_isAttackMelee = "isAttackMelee";
    const string k_isAttackFire = "isAttackFire";
    const string k_isDead = "isDead";
    const string k_isHurt = "isHurt";

    private Vector3 m_targetPosition;
    private Vector3 m_idlePosition;

    private GameObject m_currentAttacker;


    private GameObject m_FireEffect;

    // Use this for initialization
    void Start () {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;
        m_state = Boss1States.IDLE;
        m_animator = GetComponent<Animator>();

        m_idlePosition = idlePosition.transform.position;
        //this.transform.position = m_idlePosition;
        m_targetPosition = m_idlePosition;
        m_rigidbody.velocity = new Vector3(0, 0, 0);

        m_nextSkill = RandomSkillRoller();

        hp = maxHP;
        m_isAttackMeleeLocked = false;
        m_isAttackFireLocked = false;
        m_isAttackAOELocked = false;
        m_isPlayerInAlertRange = false;


        m_isPlayerInAOERange = false;
        m_isPlayerInFireRange = false;
        m_isPlayerInMeleeRange = false;

        rushTimer = 0;
        fireChaseTimer = 0;
        idleTimer = 0;

        m_FireEffect = null;
    }

    Boss1Skills RandomSkillRoller()
    {
        return Boss1Skills.FIRE;
        float randomNumber = Random.Range(0,2);
        if (randomNumber < 1)
        {
            return Boss1Skills.FIRE;
        }
        else 
        {
            return Boss1Skills.MELEE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_state);
       

        if (hp <= 0)
        {
            m_state = Boss1States.DEAD;
            m_animator.SetBool(k_isDead, true);
        }
        else if (hp < maxHP/2)
        {
            m_nextSkill = Boss1Skills.AOE;
        }

        if (m_state == Boss1States.DEAD)
        {
            m_targetPosition = this.transform.position; // stay still
            m_rigidbody.velocity = new Vector3(0, 0, 0);

            m_animator.SetBool(k_isDead, true);

            // after death animation
            AnimatorStateInfo animatorInfo;
            animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);

            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("Dead"))) // attack animation ends
            {

                if (objectRoot.activeInHierarchy)
                {
                    objectRoot.SetActive(false);
                    GameObject g = Instantiate(Resources.Load("Prefabs/Effects/Particles/SpiderDeathParticle"), this.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
                    Destroy(g, 5);
                }

            }
        }

        if (m_state == Boss1States.IDLE)
        {

            //if (m_FireEffect)
            //    Destroy(m_FireEffect);

            m_animator.SetBool(k_isAttackMelee, false);
            m_animator.SetBool(k_isAttackAOE, false);
            m_animator.SetBool(k_isAttackFire, false);
            m_animator.SetBool(k_isHurt, false);

            if (idleTimer > 0)
            {
                // 僵直时间
                idleTimer -= Time.deltaTime;
                m_targetPosition = this.transform.position;
                m_animator.SetBool(k_isWalking, false);
            }
            else
            {
                m_targetPosition = m_idlePosition;
                idleTimer = 0;
                GoTowardTarget(speed);// ordinary speed
                if (m_isPlayerInAlertRange)
                {
                    m_state = Boss1States.CHASE_PLAYER;
                }
            }
        }



        if (m_state == Boss1States.CHASE_PLAYER)
        {
            m_animator.SetBool(k_isWalking, true);
            Debug.Log(m_nextSkill);

            
            m_animator.SetBool(k_isAttackMelee, false);
            m_animator.SetBool(k_isAttackAOE, false);
            m_animator.SetBool(k_isAttackFire, false);

            AdjustFacingRotation();

            if (m_nextSkill == Boss1Skills.AOE )
            {
                if (m_isPlayerInAOERange)
                {
                    m_animator.SetBool(k_isAttackAOE, true);
                    m_state = Boss1States.ATTACK_AOE;
                }
                else
                {
                    // continue chasing
                }
            }

            if (m_nextSkill == Boss1Skills.FIRE)
            {

                m_targetPosition = m_playerTransform.position;
                GoTowardTarget(fastSpeed);

                if (m_isPlayerInFireRange || fireChaseTimer > 3f) //  追到 放技能 没追到 接着追 或者如果追超过3秒了 就直接放了
                {
                    m_targetPosition = this.transform.position; // stay still
                    m_rigidbody.velocity = new Vector3(0, 0, 0);
                    m_isAttackFireLocked = false;

                    m_state = Boss1States.ATTACK_FIRE;
                    fireChaseTimer = 0;
                }
                else
                {
                    fireChaseTimer += Time.deltaTime;
                    // continue chasing
                }
            }

            if (m_nextSkill == Boss1Skills.MELEE)
            {
                
                // 朝向玩家冲一段距离
                Vector3 dir = (m_playerTransform.position - this.transform.position).normalized;

                m_rigidbody.velocity = Vector3.Lerp(dir * rushSpeed, new Vector3(0, 0, 0), rushTimer);
                rushTimer += Time.deltaTime / 0.5f;
                if (rushTimer > 1f || m_rigidbody.velocity.Equals(Vector3.zero) ||  m_isPlayerInMeleeRange) // 冲完/撞墙了
                {
                    m_isAttackMeleeLocked = false;
                    m_animator.SetBool(k_isAttackMelee, true);
                    m_state = Boss1States.ATTACK_MELEE;
                    rushTimer = 0f;
                }
                Debug.Log("MELEE" + m_rigidbody.velocity);
            }
        }

        if (m_state == Boss1States.ATTACK_AOE)
        {
            
            // play aoe animation
            AnimatorStateInfo animatorInfo;
            animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);

            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("AttackAOE"))) // attack animation ends
            {

                if (m_isPlayerInMeleeRange && !m_isAttackAOELocked)
                {
                    m_playerTransform.GetComponent<PlayerTestController>().GetDamage(attackAOE);
                    m_isAttackAOELocked = true;
                    idleTimer = 2f;
                }
                else
                {
                    m_nextSkill = RandomSkillRoller();
                    m_state = Boss1States.IDLE;
                    
                    
                }
            }
          
        }

        if (m_state == Boss1States.ATTACK_MELEE)
        {
            
            // play melee animation
            AnimatorStateInfo animatorInfo;
            animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log(animatorInfo.normalizedTime);
            if ((animatorInfo.normalizedTime >= 1f) && (animatorInfo.IsName("AttackMelee"))) // attack animation ends
            {

                if (m_isPlayerInMeleeRange && !m_isAttackMeleeLocked)
                {
                    m_playerTransform.GetComponent<PlayerTestController>().GetDamage(attackMelee);
                    m_isAttackMeleeLocked = true;
                    idleTimer = 0.5f;
                }
                
                    m_nextSkill = Boss1Skills.FIRE;
                    m_state = Boss1States.IDLE;
                    
                    
                
            }
            
        }

        if (m_state == Boss1States.ATTACK_FIRE)
        {
            m_targetPosition = this.transform.position; // stay still
            m_rigidbody.velocity = new Vector3(0, 0, 0);
            m_animator.SetBool(k_isAttackFire, true);

           

            AnimatorStateInfo animatorInfo;
            animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);

            //Debug.Log(animatorInfo.normalizedTime);


           

            


            if ((animatorInfo.normalizedTime >= 1f) ) // attack animation ends
            {

                if (m_isPlayerInFireRange && !m_isAttackFireLocked)
                {
                    m_playerTransform.GetComponent<PlayerTestController>().GetDamage(attackFire);
                    m_isAttackFireLocked = true;
                    idleTimer = 0.5f;
                }
                
                
                    m_nextSkill = RandomSkillRoller();
                    m_state = Boss1States.IDLE;
                
            }

        }


        if (m_state == Boss1States.HURT)
        {
            m_animator.SetBool(k_isHurt, true);

            AnimatorStateInfo animatorInfo;
            animatorInfo = m_animator.GetCurrentAnimatorStateInfo(0);

            if ((animatorInfo.normalizedTime >= 1.0f) && (animatorInfo.IsName("Hurt"))) 
            {
                m_state = Boss1States.IDLE;
         
            }
        }


    }

    void AdjustFacingRotation()
    {
        if (Mathf.Abs(m_targetPosition.x - this.transform.position.x) > 0.2) // 防抖
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

    void GoTowardPlayer(float speed)
    {
        if (transform.position.x < m_playerTransform.position.x) // go right
        {
            m_rigidbody.velocity = new Vector3(speed, 0, 0);
        }

        if (transform.position.x > m_playerTransform.position.x) // go left
        {
            m_rigidbody.velocity = new Vector3(-speed, 0, 0);
        }

    }


    public void SetIsPlayerInAlertRange(bool yesOrNO)
    {
        m_isPlayerInAlertRange = yesOrNO;
    }
    public void SetIsPlayerInAOERange(bool yesOrNO)
    {
        m_isPlayerInAOERange = yesOrNO;
    }
    public void SetIsPlayerInFireRange(bool yesOrNO)
    {
        m_isPlayerInFireRange = yesOrNO;
    }
    public void SetIsPlayerInMeleeRange(bool yesOrNO)
    {
        m_isPlayerInMeleeRange = yesOrNO;
    }

    public void GetAttack(double damage, GameObject attacker)
    {
        hp -= damage;
        m_state = Boss1States.HURT;
        m_currentAttacker = attacker;
    }

    void GoTowardTarget(float speed)
    {
        if (Mathf.Abs(transform.position.x - m_targetPosition.x) < 0.2f)
        {
            // do nothing, stay idle
            m_animator.SetBool(k_isWalking, false); // possibly animation go from walking to idle
            m_targetPosition = transform.position;
            m_rigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            m_animator.SetBool(k_isWalking, true); // animation go from idle to walking 
            AdjustFacingRotation();
            // walk back to to start point
            Debug.Log("my pos:" + transform.position.x + "target:" + m_targetPosition.x);
            if (transform.position.x < m_targetPosition.x) // go right
            {
                m_rigidbody.velocity = new Vector3(speed, 0, 0);

            }
            else if (transform.position.x > m_targetPosition.x) // go left
            {
                m_rigidbody.velocity = new Vector3(-speed, 0, 0);

            }
        }
    }

    void OnStartFireEvent()
    {
       
        if (!m_FireEffect)
        { 
            Debug.Log("ffffffffffffffffffffffffffffffffffffffire");
            bool facingLeft = (this.transform.rotation == Quaternion.Euler(0, -90, 0));
            m_FireEffect = Instantiate(Resources.Load("Prefabs/Effects/PivotFire"), firePosition) as GameObject;
        }
    }



    void OnEndFireEvent()
    {

        if (m_FireEffect)
            Destroy(m_FireEffect);
    }
}
