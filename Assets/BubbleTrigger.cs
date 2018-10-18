using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class BubbleTrigger : MonoBehaviour {

    public string jsonPath;
    private float Y_offset = 200;


    private bool isEnabled = true;
    private List<DialogStruct> dialogList = new List<DialogStruct>();    
    private int index;
    //private bool inDialogProcess;


    // Use this for initialization
    void Start () {
        index = 0;
        //inDialogProcess = false;
        dialogList = DialogUtil.ReadDialogContent(jsonPath);
	}

    bool nextDialog;
    bool canInput;
    string curSpeakerId = "";
    // Update is called once per frame
    void Update() {

        //if (inDialogProcess)
        //{
            if (bubble && curSpeakingTransform)
            {
                bubble.transform.position = Camera.main.WorldToScreenPoint(curSpeakingTransform.position) + new Vector3(0, Y_offset, 0);

            }
        //}


        if (canInput && (Input.GetKey(KeyCode.F5) || Input.GetMouseButtonDown(1)))
        {
            nextDialog = true;
            
        }

        if (nextDialog)
        {
            nextDialog = false;
            if (index == dialogList.Count)
            {
                Debug.Log("dialog ends");
                canInput = false;
                if(bubble)
                {
                    Destroy(bubble);
                }
                return;
            }

            
            DialogStruct dialog = dialogList[index];

            if (dialog.speakerID != curSpeakerId) // new speaker
            {
                if (bubble)
                {
                    Destroy(bubble);
                }
                Debug.Log(dialog.speakerID);
                curSpeakerId = dialog.speakerID;

                bubble = Instantiate(Resources.Load("Prefabs/DialogBubble")) as GameObject;
                Debug.Log(bubble);
                bubble.transform.SetParent(GameObject.Find("Canvas").transform);
                //curBubbleTransform = bubble.transform;
                string objectName = CharactersConfig.character[dialog.speakerID][CharactersConfig.GAME_OBJECT_PATH];
                
                curSpeakingTransform = GameObject.Find(objectName).transform;

                bubble.GetComponent<BubbleEffectController>().setSaySomething(dialog.content);

                string speakerName = CharactersConfig.character[dialog.speakerID][CharactersConfig.NAME];
                bubble.GetComponent<BubbleEffectController>().setName(speakerName);

                string spritePath = CharactersConfig.character[dialog.speakerID][CharactersConfig.SPRITE_PATH];
                bubble.GetComponent<BubbleEffectController>().setHead(spritePath);


                canInput = false;
                Invoke("EnableInput", 2f);


                curSpeakerId = dialog.speakerID;
            }
            else
            {
                bubble.GetComponent<BubbleEffectController>().setSaySomething(dialog.content);
                canInput = false;
                Invoke("EnableInput", 2f);
            }
            index++;


        }



    }

    private void EnableInput()
    {
        canInput = true;
    }

   // private Transform curBubbleTransform;
    private Transform curSpeakingTransform;

    GameObject bubble;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            nextDialog = true;
        }
    }

    




    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            
        }
    }

}
