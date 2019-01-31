using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BeAttackedTrigger : MonoBehaviour {

    public GameObject monster;
    private Boss1AIController m_boss1AIController;
    private Transform m_playerTransform;

    void Start()
    {
        m_boss1AIController = monster.GetComponent<Boss1AIController>();
        m_playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Sword")
        {
            m_boss1AIController.GetAttack(PlayerStatus.Attack, m_playerTransform.gameObject);
        }

        if (collider.tag == "Fu")
        {
            m_boss1AIController.GetAttack(10, m_playerTransform.gameObject);
        }

    }

 
}
