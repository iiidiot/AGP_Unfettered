using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour {

    public MonsterController.DamageParts damageType = MonsterController.DamageParts.Tail;
    public Transform monster;
    private MonsterController m_monsterController;

    // Use this for initialization
    void Start () {
        m_monsterController = monster.GetComponent<MonsterController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_monsterController.damageCounter.Add(damageType);
        }
    }
}
