using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using TMPro;
using DG.Tweening;

public class DialogController : MonoBehaviour {

    public string path;

    List<DialogStruct> dialogList = new List<DialogStruct>();

    private Transform bg;
    private Transform content;
    private Transform nameTag;
    private Transform head;

    private bool start = false;

    // Use this for initialization
    void Start () {

        content = this.transform.Find("content");
        nameTag = this.transform.Find("name");
        head = this.transform.Find("head");
        bg = this.transform.Find("bg");

        // 先弹背景窗，再淡入头像，再出名字和内容
        hide();

        ReadDialogContent("Text/Dialog/test");
//        ReadDialogContent(Application.streamingAssetsPath + path);

        

        index = 0;
    }

    void hide()
    {
        bg.gameObject.SetActive(false);
        head.gameObject.SetActive(false); // 淡入
        nameTag.gameObject.SetActive(false);
        content.gameObject.SetActive(false);
    }

    public bool nextDialog = false;
    int index; // dialog id
    bool canInput = true;
	// Update is called once per frame
	void Update () {

        if (canInput && (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            nextDialog = true;
            hide();

        }

        if (nextDialog)
        {
            nextDialog = false;

           
            if(index == dialogList.Count)
            {
                Debug.Log("dialog ends");
                canInput = false;
                this.gameObject.SetActive(false); // todo should be load prefeb and destory GO
            }

            
            string curSpeakerId = "";
            DialogStruct dialog = dialogList[index];

            if (dialog.speakerID != curSpeakerId) // new speaker
            {
                
                bg.DOScale(Vector3.zero, 0);
                bg.gameObject.SetActive(true);

                
                this.content.DOScale(Vector3.zero, 0);
                content.Find("text").GetComponent<TextMeshProUGUI>().text = dialog.content;
                this.content.gameObject.SetActive(true);

             
                this.head.GetComponent<Image>().DOFade(0,0);
                this.head.gameObject.SetActive(true);

                Sequence mySequence = DOTween.Sequence();                            //创建空序列  
                Vector3 max = new Vector3(1, 1, 1);
               
            
                Tweener move1 = bg.DOScale(max, 1f);              //   scale up in 1s
                Tweener move2 = this.content.DOScale(max, 0);
                Tweener show1 = this.head.GetComponent<Image>().DOFade(1, 1f);
                mySequence.Append(move1);                         
                mySequence.Append(move2);
                mySequence.Append(show1);
               
                canInput = false;
                Invoke("EnableInput", 2f);

                
                curSpeakerId = dialog.speakerID;
            }
            else
            {
                content.Find("text").GetComponent<TextMeshProUGUI>().text = dialog.content;
            }
            index++;
        }



    }


    private delegate void dlgShowContent(string dialogContent, TextMeshProUGUI content);
    private dlgShowContent DShowContent = new dlgShowContent(ShowContent);
    



    private void EnableInput()
    {
        canInput = true;
    }

    public void ReadDialogContent(string filepath)
    {
        TextAsset ta = (TextAsset)Resources.Load(filepath, typeof(TextAsset));//从表中加载数据
        string json = ta.text;
        JsonData data = LitJson.JsonMapper.ToObject(json);
        
        foreach(JsonData element in data)
        {
            DialogStruct dialog = new DialogStruct();
            dialog.speakerID = element["id"].ToString();
            dialog.content = element["content"].ToString();
            dialogList.Add(dialog);
            
        }
        Debug.Log("###################" + dialogList);
    }


    //public void DoStartShowDialog()
    //{
    //    string curSpeakerId = "";
    //    foreach(DialogStruct dialog in dialogList)
    //    {
    //        if(dialog.speakerID != curSpeakerId) // new speaker
    //        {
    //            this.gameObject.SetActive(true);
    //            this.transform.DOScale(Vector3.zero, 0); 
    //            Vector3 max = new Vector3(1, 1, 1);
    //            this.transform.DOScale(max, 1f); //  scale up in 1s
                

               
    //            curSpeakerId = dialog.speakerID;
    //        }
    //        else
    //        {
    //            content.Find("text").GetComponent<TextMeshProUGUI>().text = dialog.content;
    //        }
    //    }
    //}


     static void ShowContent(string dialogContent, TextMeshProUGUI content)
    {
        //this.transform.Find("name/text").GetComponent<TextMeshProUGUI>().text = dialog.speakerID; // todo: change to speaker name in other json config files
        content.text = dialogContent;
    }

}
