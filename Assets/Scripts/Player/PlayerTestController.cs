using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour {
    public float speed = 1;//每秒移动速度
    public float jump_height = 1;
    public float jump_duration = 1;
    public float zero_threshold = 0.001f;
    public bool isOnGround = false;

    Rigidbody r;
    public float g_speed;//重力加速度，每秒
    public float jump_speed;//起跳速度

    // Use this for initialization
    void Start() {
        r = GetComponent<Rigidbody>();
        g_speed = -2f * jump_height / (jump_duration * jump_duration * 0.25f);
        jump_speed = -g_speed * jump_duration * 0.5f;
    }

    void Update()
    {
        Move();
    }


    void FixedUpdate () {
        MyGravity();
	}

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        //得到碰撞物体的上下左右边界值
        Vector3 c_Min = collision.collider.bounds.min;
        Vector3 c_Max = collision.collider.bounds.max;

        //如果碰到地面上方，那么就在地面上
        if (FloatEqualsZero(contact.point.y - c_Max.y))
        {
            isOnGround = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        //如果离开物体上方，那么就在空中
        if (transform.position.y > collision.collider.bounds.center.y) 
        {
            isOnGround = false;
        }
    }

    void Move()
    {
        //horizontal move===========================================
        float h_direction = Input.GetAxisRaw("Horizontal");
        if (FloatEqualsZero(h_direction))
        {
            //idle anime
            r.velocity = new Vector3(0, r.velocity.y, 0);
        }
        else
        {
            //run anime
            r.velocity = new Vector3(h_direction * speed * Time.deltaTime, r.velocity.y, 0);
        }

        //jump========================================================
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            r.velocity = new Vector3(r.velocity.x, jump_speed, 0);
        }
    }

    void MyGravity()
    {
        if (!isOnGround)
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
}
