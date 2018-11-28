using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTriggerController : MonoBehaviour {

    public GameObject monster;
    private MonsterAIController m_monsterAIController;

    void Start()
    {
        m_monsterAIController = monster.GetComponent<MonsterAIController>();    
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_monsterAIController.SetStatus(MonsterAIController.MonsterStates.CHASE_PLAYER);
        }
    }
}
