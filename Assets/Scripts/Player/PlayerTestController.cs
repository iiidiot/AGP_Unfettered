using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour
{
    private static PlayerTestController m_instance;
    public static PlayerTestController instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<PlayerTestController>();
            }
            return m_instance;
        }
    }
    public float speed = 7;//每秒移动速度
    public float jump_height = 1;
    public float jump_duration = 1;
    public float zero_threshold = 0.003f;
    public bool isOnGround = false;
    public float maxClimbSpeed = 2f;
    public GameObject in_sword;
    public GameObject scabbard;

    public Transform[] startPlace;
    // index for birth place location in the up-front places
    public int startPlaceNumber = 0;

    public bool canMoveStone { get; set; }
    public bool facingRight { get; set; }
    public bool isOnLadder { get; set; }
    public bool playerPush { get; set; }
    public bool playerAttack{get; set; }
    public bool playerJump{get; set; }

    // Obj collided with the player 
    public List<int> grounds;

    [Tooltip("The player melee attack area")]
    public Collider knifeAttack;
    // player direction input
    private Vector2 m_directionInput;
    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private float m_gravity;//重力加速度，每秒
    private float m_jumpSpeed;//起跳速度

    // the 0-6 means: [0]isBlockAllManipulation, [1]isBlockLeftMovement, [2]isBlocRightkMovement, [3]isBlockJumpMovement, [4]isBlockMeleeAttack, [5]isBlockFuAttack, [6]isBlockItemUsage
    private static int[] m_blockStatements = new int[7];
    void Awake()
    {
        SetBirthPlace();
    }
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        
        m_gravity = -2f * jump_height / (jump_duration * jump_duration * 0.25f);
        m_jumpSpeed = -m_gravity * jump_duration * 0.5f;
        grounds = new List<int>();
    }

    void Update()
    {
        HandleInput();
        MoveController();
        StatusController();
        //SoundEffectController();
        AnimeController();
        BlockStatementUpdate();
    }


    void FixedUpdate()
    {
        MyGravity();//模拟重力
    }

    //
    // Summary:
    //     listen the input from keyboard and mouse
    private void HandleInput()
    {
        //melee attack
        if( (m_blockStatements[4] == 0) && (m_blockStatements[0] == 0) )
        {
            if(Input.GetMouseButtonDown(0))
            {
                m_animator.SetTrigger("attack");
                playerAttack = true;
            }
        }
        // fu attack
        if((m_blockStatements[5] == 0) && (m_blockStatements[0] == 0) )
        {

        }

        m_directionInput = Vector3.zero;

        if(m_blockStatements[0] == 0)
        {
            float horizontalInput = 0f;
            if( (m_blockStatements[1] == 0) && Input.GetAxisRaw("Horizontal") < 0)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
            }
            else if( (m_blockStatements[2] == 0) && Input.GetAxisRaw("Horizontal") > 0 )
            {
                 horizontalInput = Input.GetAxisRaw("Horizontal");
            }

            m_directionInput = new Vector2(horizontalInput, Input.GetAxis("Vertical"));

            if(Input.GetKeyDown(KeyCode.Space) && (m_blockStatements[3] == 0) )
            {
                playerJump = true;
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                canMoveStone = true;
            }
        }

        // item usage
        if( (m_blockStatements[6] == 0) && (m_blockStatements[0] == 0) )
        {

        }

        if (Input.GetKey(KeyCode.F1) && (m_blockStatements[0] == 0) )
        {
            SaveAndLoadUtil.SavePlayerStatus();
        }

        if (Input.GetKey(KeyCode.F2) && (m_blockStatements[0] == 0) )
        {
            SaveAndLoadUtil.LoadPlayerStatus();
        }
    }

    //
    // Summary:
    //     tigger the different sound effects according to behavior state
    //
    private void SoundEffectController()
    {
        if( Mathf.Abs(m_directionInput.x) > 2 * zero_threshold && isOnGround){
			SoundController.PlaySound(0);
		}else{
			SoundController.StopPlayingSound();
		}

        if(playerJump && isOnGround){
            SoundController.PlaySound(2);
        }

        if(isOnLadder){
            // play climbing sound effect;
           //
        }

        if(playerAttack){
            SoundController.PlaySound(1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        //如果是斜坡
        if (Mathf.Abs(collision.transform.rotation.eulerAngles.z) > zero_threshold)
        {
            //计算斜坡的边界方程===========
            //计算斜率k
            float k = Mathf.Tan(Mathf.Deg2Rad * collision.transform.rotation.eulerAngles.z);//转为弧度
            //带入中央点求b
            Vector3 center = collision.collider.bounds.center;
            float b = center.y - k * center.x;

            //带入碰撞点，如果位于直线上方，则碰撞在上部，位于地面，否则碰撞在下部，位于空中
            Vector3 p = collision.contacts[0].point;
            if (p.y > k * p.x + b)//上方
            {
                grounds.Add(collision.gameObject.GetInstanceID());
            }
        }
      
        //如果不是斜坡
        else
        {
            if (collision.collider.GetType() == typeof(BoxCollider))
            {
               
                ContactPoint contact = collision.contacts[0];
                //得到碰撞物体的上下左右边界值
                Vector3 c_Min = collision.collider.bounds.min;
                Vector3 c_Max = collision.collider.bounds.max;

                //如果碰到物体上方，那么就在地面上
                if (FloatEqualsZero(contact.point.y - c_Max.y))
                {
                    grounds.Add(collision.gameObject.GetInstanceID());
                }
            }
            else if (collision.collider.GetType() == typeof(SphereCollider))
            {
                //在球体上半圆可以跳，在球体下半圆不能跳
                if (GetComponent<Collider>().bounds.center.y > collision.collider.bounds.center.y)
                {
                    grounds.Add(collision.gameObject.GetInstanceID());
                }
            }

            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        grounds.Remove(collision.gameObject.GetInstanceID());
    }

    private void OnTriggerEnter(Collider collider)
    {
        // if (collider.tag == "Lava")
        // {
        //     GetDamage();
       
        // }
    }

    //
    // Summary:
    //     control the player animation by switch the layers
    void StatusController()
    {
        //地面上以及在空中的状态切换
        if (grounds.Count > 0)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        //reset all layer 
        for(int layer=0; layer < m_animator.layerCount; layer++){
            m_animator.SetLayerWeight(layer,0);
        }
        if(!m_animator.GetBool("isDied"))
        {
            // swtich layer
            if (!isOnGround && !isOnLadder) 
            {
                m_animator.SetLayerWeight(1,1);
            } 
            else if (isOnLadder) 
            {
                m_animator.SetLayerWeight(2,1);
            } 
        }

    }

    //
    // Summary:
    //     control the player animation by setting animation parameters
    void AnimeController()
    {
        //set anime trigger
        m_animator.SetFloat("horizontalSpeed", Mathf.Abs(m_directionInput.x));
        m_animator.SetFloat("verticalSpeed", m_rigidbody.velocity.y);
        m_animator.SetFloat("absVerticalSpeed", Mathf.Abs(m_rigidbody.velocity.y));
        m_animator.SetBool("isOnGround", isOnGround);
        if(playerAttack)
        {
            //m_animator.SetTrigger("attack");
        }
        m_animator.SetBool("isPushing", playerPush);
    }

    //
    // Summary:
    //     control the horizontal, vertical and climb behavior of the player
    void MoveController()
    {
        Flip(m_directionInput.x);
        if (FloatEqualsZero(m_directionInput.x))
        {
            m_rigidbody.velocity = new Vector3(0, m_rigidbody.velocity.y, 0);
        }
        else
        {
            m_rigidbody.velocity = new Vector3(m_directionInput.x * speed, m_rigidbody.velocity.y, 0);
        }

        //jump========================================================
        if (playerJump && isOnGround)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_jumpSpeed, 0);
        }

        if(isOnLadder)
        {
            isOnGround = true;
			m_rigidbody.velocity = new Vector2(m_directionInput.x * maxClimbSpeed, m_directionInput.y * maxClimbSpeed);
		}

    }

    //
    // Summary:
    //     update vertical speed (gravity) according to different condition.
    void MyGravity()
    {
        if (!isOnGround && !isOnLadder)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y + m_gravity * Time.fixedDeltaTime, 0);
        }
    }

    //
    // Summary:
    //     check if data has changed beyond the threshold
    //
    // Parameters:
    //   f:
    //     the data needs to check
    bool FloatEqualsZero(float f)
    {
        if (Mathf.Abs(f) < zero_threshold)
        {
            return true;
        }
        return false;
    }

    //
    // Summary:
    //     Change the player direction
    //
    // Parameters:
    //   horizontal:
    //     the horizontal input; 
    //        positive: face right; negative: face left;
    public void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal <0 && facingRight)
		{
			facingRight = !facingRight;
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);
        }
    }

    //
    // Summary:
    //     Switch the enabled state of melee attack area
    public void KnifeAttackAppear() {
        knifeAttack.enabled = !knifeAttack.enabled;
    }

    //
    // Summary:
    //     Set block statement to true to unblock player behavior
    //
    // Parameters:
    //   blockState:
    //     a list of block statement which need to block
    public void BlockPlayerInput( List<int> blockState)
    {
        foreach(int state in blockState)
        {
            m_blockStatements[state] += 1;
        }
    }
    //
    // Summary:
    //     set block statement to false to unblock player behavior
    //
    // Parameters:
    //   blockState:
    //     a list of block statement name which need to unblock
    public void UnblockPlayerInput(List<int> blockState)
    {
        foreach(int state in blockState)
        {
            if(m_blockStatements[state] > 0)
                m_blockStatements[state] -= 1;
        }  
    }

    //
    // Summary:
    //     set player birthPlace according to the startPlaceNumber setting in the inspector 
    private void SetBirthPlace () {
        //if (startPlace.Length != 0)
        //{
        //    transform.parent.position = startPlaceNumber < startPlace.Length
        //        ? startPlace[startPlaceNumber].transform.position : startPlace[0].transform.position;
        //}
    }

    //
    // Summary:
    //     check the block statement as per animation statement
    private void BlockStatementUpdate () 
    {
        // is dead;
        if(m_animator.GetBool("isDied"))
        {
            m_blockStatements[0] += 1;
            m_rigidbody.mass = 10000;
        }

    }
}
