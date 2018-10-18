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

    //string saySomething;

    //public void setBubbleTrigger(BubbleTrigger bubbleTrigger)
    //{
    //    this.bubbleTrigger = bubbleTrigger;
    //}

    // Use this for initialization
    void Start () {

        content = this.transform.Find("bg/content");
        nameTag = this.transform.Find("bg/name");
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

        this.transform.Find("bg/content/text").GetComponent<TextMeshProUGUI>().text = saySomething;
        if (saySomething.Length > 100)
        {
            this.transform.Find("bg").GetComponent<RectTransform>().DOAnchorMax(new Vector2(0.95f, 0.85f), 1f);
        }
        else
        {
            this.transform.Find("bg").GetComponent<RectTransform>().DOAnchorMax(new Vector2(0.8f, 0.75f), 1f);
        }
    }

    public void setName(string name)
    {

        this.transform.Find("bg/name/text").GetComponent<TextMeshProUGUI>().text = name;

    }

    public void setHead(string path)
    {
        this.transform.Find("head").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        this.transform.Find("head").GetComponent<Image>().SetNativeSize();
    }




    // Update is called once per frame
    void Update () {
        bg.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        bg.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    }
}
