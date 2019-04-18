using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FragileStone : MonoBehaviour
{
    public Transform rockMeshTransform;

    // shaking factors
    private bool isShaking = false;
    public float shakeTime = 1f; //shake time
    private float shakeTimeLeft = 0f;
    public float fallDownSpeedFactor = 1f;


    private bool doShake = false;


    //public float shakeAmount = 0.7f;       //抖动幅度（振幅）
    //public float decreaseFactor = 1.0f;    //振幅越大抖动越厉害
    Vector3 originalPos;

    private Vector3 shakeMaxXYZ = new Vector3(0.5f, 0.5f, 0);
    private bool isLockZ = false;


    private bool m_hasPlayerEntered = false;



    // Use this for initialization
    void Start()
    {

        if (!rockMeshTransform)  // place holder!!!
        {
            rockMeshTransform = this.transform;
        }

    


        originalPos = rockMeshTransform.position;
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


                rockMeshTransform.position = originalPos + randomOffset;


                shakeTimeLeft -= Time.deltaTime;
            }
            else
            {
                shakeTimeLeft = 0f;
                rockMeshTransform.position = originalPos;
                isShaking = false;
                StoneSink();
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
                //Invoke("StoneSink", 1);
            }
        }

    }

    public void DoShake()
    {
        originalPos = rockMeshTransform.position;
        isShaking = true;
        shakeTimeLeft = shakeTime;
    }


    public void DestoryStone()
    {
        //Instantiate(fragments, transform.position, transform.rotation);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void StoneSink()
    {
        //rockMeshTransform.position = originalPos;

        float curY = this.transform.position.y;
        this.transform.DOMoveY(curY-40, 2*fallDownSpeedFactor);
 //       this.GetComponent<Rigidbody>().velocity = new Vector3(0,0, -1 * sinkSpeed);

        Invoke("DestoryStone", 2);
    }
}
