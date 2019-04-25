using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FragileStone : MonoBehaviour
{
    public Transform rockMeshTransform;
    public float shakeTime = 1f; //shake time
    public float fallDownSpeedFactor = 1f;
    public float shakeAmountFactor = 2f;
    public float shakeFrequencyFactor = 100f; // 100 as standard, 50 seems ok
    public bool isLockNegY;
    public bool isLockY;


    //public AudioClip audioClip;
    private GameObject m_FragileAudioSourceGameObject;
    private AudioSource m_MyAudioSource;

    // shaking factors
    private bool m_isShaking = false;
    private float m_shakeTimeLeft = 0f;

    private Vector3 m_RockOriginalPos;
    private Vector3 m_ThisOriginalPos;

    private Vector3 m_shakeMaxXYZ = new Vector3(0.5f, 0.5f, 0);

    public bool m_hasPlayerEntered = false;

    public bool m_restoreLock = false;

    private float m_timeTarget;

    private bool m_isPlaying;

    private float m_SinkTime = 1f;

    // Use this for initialization
    void Start()
    {
        
        if (!m_FragileAudioSourceGameObject)
        {
            m_FragileAudioSourceGameObject = GameObject.Find("FragileSound");
        }
        m_RockOriginalPos = rockMeshTransform.position;
        m_ThisOriginalPos = transform.position;
        m_hasPlayerEntered = false;
        m_restoreLock = false;

        m_MyAudioSource = m_FragileAudioSourceGameObject.GetComponent<AudioSource>();
        //m_MyAudioSource.clip = audioClip;
        m_isPlaying = false;
        m_SinkTime = 1f;
        Debug.Log(this.gameObject.name + "m_RockOriginalPos" + m_RockOriginalPos);
        Debug.Log(this.gameObject.name + "m_ThisOriginalPos" + m_ThisOriginalPos);
    }



    // Update is called once per frame
    void Update()
    {
        if (m_isShaking)
        {
            if (!m_MyAudioSource.isPlaying)
            {

                m_MyAudioSource.Play();

            }

            if (m_shakeTimeLeft > 0)
            {
                float timeSpent = shakeTime - m_shakeTimeLeft;
                if (timeSpent > m_timeTarget)
                {
                    ShakeTheTransform();
                    m_timeTarget = timeSpent + shakeTime/shakeFrequencyFactor;
                }
                m_shakeTimeLeft -= Time.deltaTime ;
            }
            else
            {
                EndShake();
            }
        }


        //if (m_restoreLock)
        //{
        //    Debug.Log(this.gameObject.name + ": restore lock on");
        //    Debug.Log(this.gameObject.name + "m_RockOriginalPos" + m_RockOriginalPos);
        //    Debug.Log(this.gameObject.name + "m_ThisOriginalPos" + m_ThisOriginalPos);
        //    Debug.Log(this.gameObject.name + "this.transform.position" + this.transform.position);
        //    Debug.Log(this.gameObject.name + "rockMeshTransform.position" + rockMeshTransform.position);
        //    if (!TwoVectorEqual(this.transform.position, m_ThisOriginalPos) || !TwoVectorEqual(rockMeshTransform.position, m_RockOriginalPos))
        //    {
        //        Debug.Log(this.gameObject.name + ": try restore");

        //        this.transform.position = m_ThisOriginalPos;
        //        rockMeshTransform.position = m_RockOriginalPos;
        //    }
        //    else
        //    {
        //        Debug.Log(this.gameObject.name + ": restore done");
        //        m_restoreLock = false;
        //        m_hasPlayerEntered = false;
        //    }
        //}

    }

    private bool TwoVectorEqual(Vector3 a, Vector3 b)
    {
        float delta = 0.01f;
        if (Mathf.Abs(a.x - b.x) <= delta && Mathf.Abs(a.y - b.y) <= delta && Mathf.Abs(a.z - b.z) <= delta)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    private void EndShake()
    {
        //if (m_MyAudioSource.isPlaying)
        //{
        //    m_MyAudioSource.Stop();

        //}
        m_shakeTimeLeft = 0f;
        rockMeshTransform.position = m_RockOriginalPos;
        m_isShaking = false;
        StoneSink();
        m_timeTarget = 0;
    }

    private void ShakeTheTransform()
    {
        Vector3 randomOffset = Random.insideUnitSphere;
        randomOffset.x *= m_shakeMaxXYZ.x / shakeAmountFactor;
       
        randomOffset.z *= m_shakeMaxXYZ.z / shakeAmountFactor;

        if (isLockY)
        {
            randomOffset.y = 0;
        }
        else if (isLockNegY)
        {
            randomOffset.y *= Mathf.Abs(m_shakeMaxXYZ.y) / shakeAmountFactor;
        }
        else
        {
            randomOffset.y *= m_shakeMaxXYZ.y / shakeAmountFactor;
        }


        rockMeshTransform.position = m_RockOriginalPos + randomOffset;
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
        m_RockOriginalPos = rockMeshTransform.position;
        m_isShaking = true;
        m_shakeTimeLeft = shakeTime;
        m_timeTarget = 0;
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
        this.transform.DOMoveY(curY-30, m_SinkTime);
        Invoke("SinkComplete", m_SinkTime+0.1f);
    }

    public void SinkComplete()
    {
        if (m_restoreLock)
        {
            this.transform.position = m_ThisOriginalPos;
            rockMeshTransform.position = m_RockOriginalPos;
            m_restoreLock = false;
        }
    }


    public void Restore()
    {
        

        //if (!TwoVectorEqual(this.transform.position, m_ThisOriginalPos) || !TwoVectorEqual(rockMeshTransform.position, m_RockOriginalPos))


        if (m_hasPlayerEntered)
        {
            this.transform.position = m_ThisOriginalPos;
            rockMeshTransform.position = m_RockOriginalPos;

            m_restoreLock = true;
        }
        else
        {
            m_restoreLock = false;
        }
        m_hasPlayerEntered = false;


    }
}
