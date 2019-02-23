using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour {

    private Transform m_playerTransform;
    public int damage;

    // Use this for initialization
    void Start () {
        m_playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            m_playerTransform.GetComponent<PlayerTestController>().GetDamage(damage);
        }
    }
}
