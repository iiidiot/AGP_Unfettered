using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInTriggerController_BT : MonoBehaviour
{ 

    
    public bool m_PlayerIsInRange;

    void Start()
    {
        m_PlayerIsInRange = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_PlayerIsInRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_PlayerIsInRange = false;
        }
    }

    public bool GetIsPlayerInAlertRange()
    {
        return m_PlayerIsInRange;
    }
}