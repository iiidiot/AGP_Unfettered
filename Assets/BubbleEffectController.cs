using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class BubbleEffectController : MonoBehaviour {

    private Transform bg;
    private Transform content;
    private Transform nameTag;
    private Transform head;

    // private BubbleTrigger bubbleTrigger;

    string saySomething;

    //public void setBubbleTrigger(BubbleTrigger bubbleTrigger)
    //{
    //    this.bubbleTrigger = bubbleTrigger;
    //}

    // Use this for initialization
    void Start () {

        content = this.transform.Find("content");
        nameTag = this.transform.Find("name");
        head = this.transform.Find("head");
        bg = this.transform.Find("bg");

        //content.Find("text").GetComponent<TextMeshProUGUI>().text = saySomething;

        this.transform.DOScale(Vector3.zero, 0);
        
        this.content.DOScale(Vector3.zero, 0); // hide content
                                               


        this.head.GetComponent<Image>().DOFade(0, 0);  // hide head
       

        Sequence mySequence = DOTween.Sequence();                            //创建空序列  
        Vector3 max = new Vector3(1, 1, 1);

        Tweener move1 = this.transform.DOScale(max, 1f);       // scale up bubble
        Tweener move2 = content.DOScale(max, 0);               // show content
        Tweener show1 = head.GetComponent<Image>().DOFade(1, 1f);   // show head
        mySequence.Append(move1);
        mySequence.Append(move2);
        mySequence.Append(show1);
    }

    public void setSaySomething(string saySomething)
    {

        this.transform.Find("content").Find("text").GetComponent<TextMeshProUGUI>().text = saySomething;
        //this.saySomething = saySomething;
        //if (content.Find("text").GetComponent<TextMeshProUGUI>())
        //{
        //    content.Find("text").GetComponent<TextMeshProUGUI>().text = saySomething;
        //}
    }


    // Update is called once per frame
    void Update () {
		
	}
}
