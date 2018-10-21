using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour
{
    private static PlayerTestController instance;

    public static PlayerTestController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerTestController>();
            }
            return instance;
        }
    }

    public float speed = 7;//每秒移动速度

    public float jump_height = 1;

    public float jump_duration = 1;

    public float zero_threshold = 0.003f;

    public bool isOnGround = false;

    public float maxClimbSpeed = 2f;

    public bool CanMoveStone { get; set; }

    public Transform[] startPlace;

    public int startPlaceNumber = 0;

    private Vector2 directionInput;
    
    private bool isHandleInput = true;

    [SerializeField]
    private Collider knifeAttack;

    private Rigidbody r;

    float g_speed;//重力加速度，每秒

    float jump_speed;//起跳速度

    public List<int> grounds;

    private Animator myAnimator;

    public bool FacingRight { get; set; }

    public bool OnLadder { get; set; }

    public bool PlayerPush { get; set; }

    public bool PlayerAttack{get; set; }
    
    public bool PlayerJump{get; set; }

    void Awake()
    {
        SetBirthPlace();
    }
    void Start()
    {
        r = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        
        g_speed = -2f * jump_height / (jump_duration * jump_duration * 0.25f);
        jump_speed = -g_speed * jump_duration * 0.5f;
        grounds = new List<int>();
    }

    void Update()
    {
        if (isHandleInput)
        {  
             HandleInput();
        }
        else
        {
            directionInput = Vector2.zero;
        }

        MoveController();
        StatusController();
        SoundEffectController();
        AnimeController();
    }


    void FixedUpdate()
    {
        MyGravity();//模拟重力
    }

    private void HandleInput()
    {

        directionInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump = true;
        }

		if(Input.GetKeyDown(KeyCode.F))
        {
			CanMoveStone = true;
		}

        if (Input.GetKey(KeyCode.F1))
        {
            SaveAndLoadUtil.SavePlayerStatus();
        }

        if (Input.GetKey(KeyCode.F2))
        {
            SaveAndLoadUtil.LoadPlayerStatus();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            GetDamage();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttack = true;
           
            
        }
    }

    private void SoundEffectController()
    {
        if( Mathf.Abs(directionInput.x) > 2 * zero_threshold && isOnGround){
			SoundController.PlaySound(0);
		}else{
			SoundController.StopPlayingSound();
		}

        if(PlayerJump && isOnGround){
            SoundController.PlaySound(2);
        }

        if(OnLadder){
            // play climbing sound effect;
           //
        }

        if(PlayerAttack){
            SoundController.PlaySound(1);
        }
    }
    private void GetDamage()
    {
        // play some animation maybe
        PlayerStatus.health -= 1; // record some damage 
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
        if (collider.tag == "Lava")
        {
            GetDamage();
       
        }
    }

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
        for(int layer=0; layer < myAnimator.layerCount; layer++){
            myAnimator.SetLayerWeight(layer,0);
        }

        // swtich layer
        if (!isOnGround && !OnLadder) 
        {
            myAnimator.SetLayerWeight(1,1);
        } 
        else if (OnLadder) 
        {
            myAnimator.SetLayerWeight(2,1);
        } 
    }

    void AnimeController()
    {
        //set anime trigger
        myAnimator.SetFloat("horizontalSpeed", Mathf.Abs(directionInput.x));
        myAnimator.SetFloat("verticalSpeed", r.velocity.y);
        myAnimator.SetFloat("absVerticalSpeed", Mathf.Abs(r.velocity.y));
        myAnimator.SetBool("isOnGround", isOnGround);
        myAnimator.SetBool("attack", PlayerAttack);
        myAnimator.SetBool("isPushing", PlayerPush);
    }

    void MoveController()
    {
        Flip(directionInput.x);
        if (FloatEqualsZero(directionInput.x))
        {
            r.velocity = new Vector3(0, r.velocity.y, 0);
        }
        else
        {
            r.velocity = new Vector3(directionInput.x * speed, r.velocity.y, 0);
        }

        //jump========================================================
        if (PlayerJump && isOnGround)
        {
            r.velocity = new Vector3(r.velocity.x, jump_speed, 0);
        }

        if(OnLadder)
        {
            isOnGround = true;
			r.velocity = new Vector2(directionInput.x * maxClimbSpeed, directionInput.y * maxClimbSpeed);
		}
    }

    void MyGravity()
    {
        if (!isOnGround && !OnLadder)
        {
            r.velocity = new Vector3(r.velocity.x, r.velocity.y + g_speed * Time.fixedDeltaTime, 0);
        }
    }



    bool FloatEqualsZero(float f)
    {
        if (Mathf.Abs(f) < zero_threshold)
        {
            return true;
        }
        return false;
    }


    public void Flip(float horizontal)
    {
        if(horizontal > 0 && !FacingRight || horizontal <0 && FacingRight)
		{
			FacingRight = !FacingRight;
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180);
        }
    }

    public void KnifeAttackAppear() {
        knifeAttack.enabled = !knifeAttack.enabled;
    }

    public void MuteAllPlayerControlInput()
    {
        isHandleInput = false;
    }
    public void RestoreAllPlayerControlInput()
    {
        isHandleInput = true;
    }

    private void SetBirthPlace () {
        if (startPlace.Length != 0)
        {
            transform.parent.position = startPlaceNumber < startPlace.Length 
                ? startPlace[startPlaceNumber].transform.position : startPlace[0].transform.position;
        }
    }
}
