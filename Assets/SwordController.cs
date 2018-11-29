using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    GameObject m_player;
    // Use this for initialization
    void Start()
    {
        m_player = CharactersConfigManager.GetPlayerGameObject();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (PlayerTestController.instance.playerAttack)
        {
            // when player touches the spider, it must immediately cast damage, even if its damage skill is in CD 
            if (collider.tag == "Enemy")
            {
                collider.gameObject.GetComponent<MonsterAIController>().GetAttack(PlayerStatus.Attack, m_player);
            }
        }
    }
}
