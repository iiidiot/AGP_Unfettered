using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTriggerBoss01Controller : MonoBehaviour
{

    public GameObject monster;
    private Boss1AIController m_boss1AIController;

    void Start()
    {
        m_boss1AIController = monster.GetComponent<Boss1AIController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInAlertRange(true);
            m_boss1AIController.SetIsPlayerInAOERange(true);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInAlertRange(true);
            m_boss1AIController.SetIsPlayerInAOERange(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInAlertRange(false);
            m_boss1AIController.SetIsPlayerInAOERange(false);
        }
    }
}
