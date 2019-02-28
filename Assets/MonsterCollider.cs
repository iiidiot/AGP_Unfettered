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



    public MonsterController GetMonsterController()
    {
        return m_monsterController;
    }

   
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("trigger enter");
        Debug.Log(collider.name);
        if (collider.tag == "Sword")
        {
            bool critical = Random.Range(0, 2) == 1;
            double damage = critical ? PlayerStatus.Attack * 2 : PlayerStatus.Attack;

            if (critical)
            {
                m_monsterController.GetAttack(MonsterController.DamageType.SwordCritical, damage);

            }
            else
            {
                m_monsterController.GetAttack(MonsterController.DamageType.Sword, damage);
            }

            Vector3 contactPoint = this.GetComponent<CapsuleCollider>().ClosestPoint(m_playerTransform.position);
            SpawnSwordAttackParticleEffects(contactPoint, Quaternion.identity);

            SwordAttackCameraShakeEffects();

        }

        //if (collider.tag == "Fu")
        //{
        //    m_monsterController.GetAttack(10);
        //}

    }
    

    private void SwordAttackCameraShakeEffects()
    {
        if (this.transform.position.x > m_playerTransform.position.x)
        {
            Camera.main.GetComponent<ProCamera2DShake>().Shake("HitShakeLeft");
           
        }
        else
        {
            Camera.main.GetComponent<ProCamera2DShake>().Shake("HitShakeRight");
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
