using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


    // shaking factors
    private bool isShaking = false;
    public float shakeTime = 0.1f; //shake time
    private float shakeTimeLeft = 0f;
    public float shakePrequencyFactor = 0.5f;

    public bool doShake = false;
    

    //public float shakeAmount = 0.7f;       //抖动幅度（振幅）
    //public float decreaseFactor = 1.0f;    //振幅越大抖动越厉害
    Vector3 originalPos;

    public Vector3 shakeMaxXYZ = new Vector3(0.5f, 0.5f, 0);
    public bool isLockZ = true;

    public void DoCameraShake()
    {
        originalPos = this.transform.position; // Camera.main.transform.localPosition;
        isShaking = true;
        shakeTimeLeft = shakeTime;
    }


    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        if (doShake) // camera shake
        {
            DoCameraShake();
            Debug.Log("shake input get");
        }


        if (isShaking)
        {
            //Transform camTransform = Camera.main.transform;
            if (shakeTimeLeft > 0)
            {
                Vector3 randomOffset = Random.insideUnitSphere;
                randomOffset.x *= shakeMaxXYZ.x;
                randomOffset.y *= shakeMaxXYZ.y;

                if (isLockZ)
                {
                    randomOffset.z = 0;
                }
                else
                {
                    randomOffset.z *= shakeMaxXYZ.z;
                }
                this.transform.position = originalPos + randomOffset;
//                camTransform.localPosition = originalPos + randomOffset;

                shakeTimeLeft -= Time.deltaTime * shakePrequencyFactor;
            }
            else
            {
                shakeTimeLeft = 0f;
                //camTransform.localPosition = originalPos;
                this.transform.position = originalPos;
                isShaking = false;
                this.GetComponent<CameraFollowSmooth>().isMove = true; // fix shake后y不归位的bug
            }
        }

    }
}
