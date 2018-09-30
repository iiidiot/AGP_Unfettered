using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    GameObject m_parent;

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
            EnemyMovementController controller = m_parent.GetComponent<EnemyMovementController>();
            controller.ActivateTriggerEnterEvent(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemyMovementController controller = m_parent.GetComponent<EnemyMovementController>();
            controller.ActivateTriggerStayEvent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemyMovementController controller = m_parent.GetComponent<EnemyMovementController>();
            controller.ActivateTriggerExitEvent();
        }
    }
}
