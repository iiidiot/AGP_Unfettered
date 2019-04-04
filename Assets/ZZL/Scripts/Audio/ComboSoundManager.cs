using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSoundManager : MonoBehaviour
{
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if(m_animator.GetBool("playAttackSound_1"))
        //{
        //    SoundController.PlaySound(7);
        //}

        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack")) //.GetAnimatorTransitionInfo(0).IsName("AttackTransition_1"))//
        {
            SoundController.PlaySound(7);
        }

        else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("attack01"))
        {
            SoundController.PlaySound(1);
        }

        //else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("attack02"))
        //{
        //    SoundController.PlaySound(7);
        //}

        //else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("attack03"))
        //{
        //    SoundController.PlaySound(8);
        //}
    }
}
