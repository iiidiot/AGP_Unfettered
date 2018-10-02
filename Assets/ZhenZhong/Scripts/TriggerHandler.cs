using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private GameObject m_parent;

    // Use this for initialization
    void Start ()
    {
        m_parent = transform.parent.gameObject;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EnemyMovementController movementController = m_parent.GetComponent<EnemyMovementController>();
            movementController.ActivateTriggerEnterEvent(other);

            EnemyAttackController attackController = m_parent.GetComponent<EnemyAttackController>();
            
            // attackController.Attack(other);
            attackController.Target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemyMovementController movementController = m_parent.GetComponent<EnemyMovementController>();
            movementController.ActivateTriggerStayEvent(other);

            EnemyAttackController attackController = m_parent.GetComponent<EnemyAttackController>();
           
            //attackController.Attack(other);
            attackController.Target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemyMovementController movementController = m_parent.GetComponent<EnemyMovementController>();
            movementController.ActivateTriggerExitEvent();

            //EnemyAttackController attackController = m_parent.GetComponent<EnemyAttackController>();
            //attackController.StopAttack();
        }
    }
}
