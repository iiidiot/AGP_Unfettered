using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileStone : MonoBehaviour
{


    public GameObject fragments;

    // shaking factors
    private bool isShaking = false;
    public float shakeTime = 1f; //shake time
    private float shakeTimeLeft = 0f;
    public float shakePrequencyFactor = 0.5f;

    public bool doShake = false;


    //public float shakeAmount = 0.7f;       //抖动幅度（振幅）
    //public float decreaseFactor = 1.0f;    //振幅越大抖动越厉害
    Vector3 originalPos;

    public Vector3 shakeMaxXYZ = new Vector3(0.5f, 0.5f, 0);
    public bool isLockZ = false;


    private bool m_hasPlayerEntered = false;



    // Use this for initialization
    void Start()
    {
        originalPos = this.transform.position;
        m_hasPlayerEntered = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isShaking)
        {
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


                shakeTimeLeft -= Time.deltaTime * shakePrequencyFactor;
            }
            else
            {
                shakeTimeLeft = 0f;
                this.transform.position = originalPos;
                isShaking = false;
            }
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!m_hasPlayerEntered)
            {
                m_hasPlayerEntered = true;
                DoShake();
                Invoke("DestoryStone", 1);
            }
        }

    }

    public void DoShake()
    {
        originalPos = this.transform.position;
        isShaking = true;
        shakeTimeLeft = shakeTime;
    }


    public void DestoryStone()
    {
        Instantiate(fragments, transform.position, transform.rotation);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
