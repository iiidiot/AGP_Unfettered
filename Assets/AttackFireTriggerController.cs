using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFireTriggerController : MonoBehaviour {

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
            m_boss1AIController.SetIsPlayerInFireRange(true);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInFireRange(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInFireRange(false);
        }
    }
}
