﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {


    public double maxHP = 10;
    public double attackCDTime = 2; // the 2 attack methods will share the same CD


    public double m_hp;
    public double m_attackCDTimeCounter;
    private Transform m_playerTransform;

    // Use this for initialization
    void Start () {
        m_hp = maxHP;
        m_attackCDTimeCounter = attackCDTime;
        m_playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;
    }
	
	// Update is called once per frame
	void Update () {
        m_attackCDTimeCounter -= Time.deltaTime;
        if (m_attackCDTimeCounter < 0)
        {
            m_attackCDTimeCounter = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
       

        if (collider.tag == "Sword")
        {
            GetAttack(PlayerStatus.Attack, m_playerTransform.gameObject);
        }

        if (collider.tag == "Fu")
        {
            GetAttack(10, m_playerTransform.gameObject);
        }

    }


    public void GetAttack(double damage, GameObject attacker)
    {
        m_hp -= damage;
       
    }
}
