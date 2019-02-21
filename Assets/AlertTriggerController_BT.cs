using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTriggerController_BT : MonoBehaviour
{ 
    public GameObject monster;
    
    private bool m_PlayerIsInAlertRange;

    void Start()
    {
        m_PlayerIsInAlertRange = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_PlayerIsInAlertRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_PlayerIsInAlertRange = false;
        }
    }

    public bool GetIsPlayerInAlertRange()
    {
        return m_PlayerIsInAlertRange;
    }
}