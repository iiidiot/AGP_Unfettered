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
    public float shakeAmountFactor = 2f;
    public float shakeFrequencyFactor = 100f; // 100 as standard, 50 seems ok

    private bool doShake = false;

    Vector3 originalPos;

    private Vector3 shakeMaxXYZ = new Vector3(0.5f, 0.5f, 0);

    public bool isLockNegY;

    public bool isLockY;
    private bool m_hasPlayerEntered = false;

    private bool m_restoreLock = false;


    // Use this for initialization
    void Start()
    {

        if (!rockMeshTransform)  // place holder!!!
        {
            rockMeshTransform = this.transform;
        }

        originalPos = rockMeshTransform.position;
        m_hasPlayerEntered = false;
        m_restoreLock = false;
    }

    float timeTarget;

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (shakeTimeLeft > 0)
            {
                float timeSpent = shakeTime - shakeTimeLeft;
                if (timeSpent > timeTarget)
                {
                    ShakeTheTransform();
                    timeTarget = timeSpent + shakeTime/shakeFrequencyFactor;
                }
                shakeTimeLeft -= Time.deltaTime ;
            }
            else
            {
                shakeTimeLeft = 0f;
                rockMeshTransform.position = originalPos;
                isShaking = false;
                StoneSink();
                timeTarget = 0;
            }
        }


        if (m_restoreLock)
        {
            if (this.transform.position != originalPos)
            {
                this.transform.position = originalPos;
            }
            else
            {
                m_restoreLock = false;
                m_hasPlayerEntered = false;
            }
        }

    }

    private void ShakeTheTransform()
    {
        Vector3 randomOffset = Random.insideUnitSphere;
        randomOffset.x *= shakeMaxXYZ.x / shakeAmountFactor;
       
        randomOffset.z *= shakeMaxXYZ.z / shakeAmountFactor;

        if (isLockY)
        {
            randomOffset.y = 0;
        }
        else if (isLockNegY)
        {
            randomOffset.y *= Mathf.Abs(shakeMaxXYZ.y) / shakeAmountFactor;
        }
        else
        {
            randomOffset.y *= shakeMaxXYZ.y / shakeAmountFactor;
        }


        rockMeshTransform.position = originalPos + randomOffset;
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
        timeTarget = 0;
    }


    public void DestroyStone()
    {
        //Instantiate(fragments, transform.position, transform.rotation);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void StoneSink()
    {
        float curY = this.transform.position.y;
        this.transform.DOMoveY(curY-20, 1);
        Invoke("DestoryStone", 1);
    }


    public void Restore()
    {
        m_hasPlayerEntered = false;
        this.transform.position = originalPos;
        m_restoreLock = true;
    }
}
