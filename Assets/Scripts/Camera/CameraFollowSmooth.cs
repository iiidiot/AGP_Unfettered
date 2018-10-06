﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour {

    public Transform followTarget;
    public Vector3 offset;
    public float co = 2.0f;
    public float smooth = 1.0f;
    public float Y_threshold = 1.0f;

    public bool isMove = false;

    private float min = 0.1f;



    // shaking factors
    private bool isShaking = false;
    public float shake = 0f; //shake time
    //抖动幅度（振幅）
    //振幅越大抖动越厉害
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;

    public void CameraShake(float shaketime)
    {
        shake = shaketime;
        originalPos = Camera.main.transform.localPosition;
        isShaking = true;
    }


    // Use this for initialization
    void Start () {
        offset = transform.position - followTarget.position;
	}
    void Update()
    {
        if (Mathf.Abs(followTarget.position.y - transform.position.y) > Y_threshold)
        {
            isMove = true;
        }
        if (Mathf.Abs(followTarget.position.y - transform.position.y) < min)
        {
            isMove = false;
        }
    }

    // Update is called once per frame
    void LateUpdate () {

        if (isShaking)
        {
            Transform camTransform = Camera.main.transform;
            if (shake > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shake -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shake = 0f;
                camTransform.localPosition = originalPos;
                isShaking = false;
            }
        }
        else
        {
            //x 跟随
            transform.position = new Vector3((followTarget.position + offset).x, transform.position.y, transform.position.z);


            //y 平滑跟上
            //float newCamY = (followTarget.position + offset).y;
            //float deltaY = (newCamY - transform.position.y) / co;
            //transform.Translate(0, deltaY, 0);
            if (isMove)
            {
                transform.position = Vector3.Lerp(transform.position, followTarget.position + offset, Time.deltaTime * smooth);
            }
        }
    }
}
