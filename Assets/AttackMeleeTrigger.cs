using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMeleeTrigger : MonoBehaviour {
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
            m_boss1AIController.SetIsPlayerInMeleeRange(true);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInMeleeRange(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_boss1AIController.SetIsPlayerInMeleeRange(false);
        }
    }
}
