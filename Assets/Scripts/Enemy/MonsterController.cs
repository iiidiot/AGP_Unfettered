using Guirao.UltimateTextDamage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {


    public double maxHP = 10;
    public double attackCDTime = 2; // the 2 attack methods will share the same CD


    public double m_hp;
    public double m_attackCDTimeCounter;
    public SimpleHealthBar healthBar;
    public UltimateTextDamageManager ultimateTextDamageManager;

    private Transform m_playerTransform;

    // parts that can hurt player
    public enum DamageParts
    {
        Tail,
    }

    public static Dictionary<DamageParts, double> damageAmount = new Dictionary<DamageParts, double>()
    {
        {DamageParts.Tail, 1},
    };

    // type that will hurt the monster
    public enum DamageType
    {
        Fu,
        Sword,
        SwordCritical,
    }

 



    // right now we only do damage check-out to player
    public HashSet<DamageParts> damageCounter = new HashSet<DamageParts>();

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


    // override this method for different kinds of monsters
    public void GetAttack(DamageType type, double damage)
    {

        m_hp -= damage;

        if (healthBar != null)
            healthBar.UpdateBar((float)m_hp, (float)maxHP);

        if (type == DamageType.Sword)
        {
            ultimateTextDamageManager.Add(damage.ToString(), transform, "default");
        }

        if (type == DamageType.SwordCritical)
        {
            ultimateTextDamageManager.Add(damage.ToString(), transform, "critical");
        }

        if (type == DamageType.Fu)
        {
            ultimateTextDamageManager.Add(damage.ToString(), transform, "fu");
        }
    }


    public void DamageCheckout()
    {

        foreach (DamageParts dp in damageCounter)
        {
            m_playerTransform.GetComponent<PlayerTestController>().GetDamage(damageAmount[dp]);
        }

        damageCounter.Clear();
    }
}

