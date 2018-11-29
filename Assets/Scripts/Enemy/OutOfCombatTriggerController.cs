using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCombatTriggerController : MonoBehaviour
{

    public GameObject monster;
    private MonsterAIController m_monsterAIController;

    void Start()
    {
        m_monsterAIController = monster.GetComponent<MonsterAIController>();
    }




    void OnTriggerExit(Collider collider)
    {
        if (collider.name == monster.name)
        {
            m_monsterAIController.SetIsInCombatRange(false);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.name == monster.name)
        {
            m_monsterAIController.SetIsInCombatRange(true);
        }
    }

}
