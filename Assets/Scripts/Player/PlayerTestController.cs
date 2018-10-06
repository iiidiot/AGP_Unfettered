using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestController : MonoBehaviour
{
    public float speed = 1;//每秒移动速度
    public float jump_height = 1;
    public float jump_duration = 1;
    public float zero_threshold = 0.001f;
    public bool isOnGround = false;

    Rigidbody r;
    float g_speed;//重力加速度，每秒
    float jump_speed;//起跳速度
    public List<int> grounds;


    // Use this for initialization
    void Start()
    {
        r = GetComponent<Rigidbody>();
        g_speed = -2f * jump_height / (jump_duration * jump_duration * 0.25f);
        jump_speed = -g_speed * jump_duration * 0.5f;
        grounds = new List<int>();
    }

    void Update()
    {
        StatusController();
        MoveController();
        AnimeController();
    }


    void FixedUpdate()
    {
        MyGravity();//模拟重力
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
    }

    void AnimeController()
    {
        //在空中时======================
        //如果速度为上升，调用跳，如果速度为下降，调用落

        //在地面时
        //如果有左右速度，跑。如果没有，idle
    }

    void MoveController()
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
