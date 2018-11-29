using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DingShi : MonoBehaviour
{
	public GameObject panel;
    private Animator anim;
    // Use this for initialization
    void Start()
    {


        //play动画
        anim = gameObject.GetComponent<Animator>();
        //anim.SetInteger("MyPlay", 1);


    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("Spawn")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束
        {
            Time.timeScale = 0;
			GetComponent<AudioSource>().enabled = true;
			panel.SetActive(true);
        }


    }
}