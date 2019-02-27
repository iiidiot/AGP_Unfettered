using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour {

    public Transform monsterTransform;

    private MonsterController m_monsterController;
    private Transform m_playerTransform;


    // Use this for initialization
    void Start () {
        m_monsterController = monsterTransform.GetComponent<MonsterController>();
        m_playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Sword")
        {
            m_monsterController.GetAttack(PlayerStatus.Attack);

            Vector3 contactPoint = this.GetComponent<CapsuleCollider>().ClosestPoint(m_playerTransform.position);
            SpawnSwordAttackParticleEffects(contactPoint, Quaternion.identity);

            SwordAttackCameraShakeEffects();

        }

        if (collider.tag == "Fu")
        {
            m_monsterController.GetAttack(10);
        }

    }

    private void SwordAttackCameraShakeEffects()
    {
        if (this.transform.position.x > m_playerTransform.position.x)
        {
            ProCamera2DShake.Instance.Shake("HitShakeLeft");
        }
        else
        {
            ProCamera2DShake.Instance.Shake("HitShakeRight");
        }
    }



    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Sword")
    //    {

    //        ContactPoint contact = collision.contacts[0];
    //        SpawnSwordAttackParticleEffects(contact.point, Quaternion.identity);
    //    }
    //}


    private void SpawnSwordAttackParticleEffects(Vector3 position, Quaternion rotation)
    {
        GameObject swordEffect = Instantiate(Resources.Load("Prefabs/Effects/SlashParticle"), position, rotation) as GameObject;
    }


}
