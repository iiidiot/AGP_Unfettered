using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour {

	public float zero_threshold = 0.003f;
	public List<int> grounds;
	public bool isOnGround = false;

    // Use this for initialization
    private Rigidbody m_rigidbody;

    // Update is called once per frame
    void Update () {
		StatusController();
		MyGravity();//模拟重力
		
	}

    private void MyGravity()
    {
       transform.localPosition = Vector2.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
		Debug.Log("grounds:"+grounds.Count);
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

	void StatusController()
    {
        //地面上以及在空中的状态切换
        if (grounds.Count > 0)
        {
			isOnGround = true;
            PlayerTestController.instance.isOnGround = isOnGround;
			
        }
        else
        {
			isOnGround = false;
            PlayerTestController.instance.isOnGround = isOnGround;
        }

	}
}
