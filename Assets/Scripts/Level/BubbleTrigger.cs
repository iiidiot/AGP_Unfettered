using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class BubbleTrigger : MonoBehaviour
{

    public string jsonPath;
    public bool canMoveHere = true;

    private List<DialogStruct> dialogList = new List<DialogStruct>();
    private int index;
    private Transform curSpeakingTransform;
    private GameObject bubble;
    private bool nextDialog;
    private bool canInput;
    private string curSpeakerId = "";

    
    // Use this for initialization
    void Start()
    {
        index = 0;
        dialogList = DialogUtil.ReadDialogContent(jsonPath);
    }


    // Update is called once per frame
    void Update()
    {

        if (bubble && curSpeakingTransform)
        {
            //float yOffset = curSpeakingTransform.gameObject.GetComponent<Renderer>().bounds.size.y;

            bubble.transform.position = Camera.main.WorldToScreenPoint(curSpeakingTransform.position + new Vector3(0, 0, 0));

        }


        if (canInput && (Input.GetKey(KeyCode.F5) || Input.GetMouseButtonDown(0)))
        {
            nextDialog = true;

        }

        if (nextDialog)
        {
            string playerPath = CharactersConfigManager.GetCharacterGameObjectPath(CharactersConfigManager.k_PlayerID);
            if (!canMoveHere)
            {

                GameObject.Find(playerPath).GetComponent<PlayerTestController>().MuteAllPlayerControlInput();
            }

            nextDialog = false;
            if (index == dialogList.Count)
            {
                Debug.Log("dialog ends");
                canInput = false;
                if (bubble)
                {
                    Destroy(bubble);
                }
                if (!canMoveHere)
                {
                    Debug.Log("%%%%%%%%%%%%%%%restore");
                    GameObject.Find(playerPath).GetComponent<PlayerTestController>().RestoreAllPlayerControlInput();
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
                string objectName = CharactersConfigManager.GetCharacterGameObjectPath(dialog.speakerID);

                curSpeakingTransform = GameObject.Find(objectName).transform;

                bubble.GetComponent<BubbleEffectController>().setSaySomething(dialog.content);

                string speakerName = CharactersConfigManager.GetCharacterName(dialog.speakerID);
                bubble.GetComponent<BubbleEffectController>().setName(speakerName);

                string spritePath = CharactersConfigManager.GetCharacterSpritePath(dialog.speakerID);
                bubble.GetComponent<BubbleEffectController>().setHead(spritePath);


                canInput = false;
                Invoke("EnableInput", 0.5f);


                curSpeakerId = dialog.speakerID;
            }
            else
            {
                bubble.GetComponent<BubbleEffectController>().setSaySomething(dialog.content);
                canInput = false;
                Invoke("EnableInput", 0.5f);
            }
            index++;


        }



    }

    private void EnableInput()
    {
        canInput = true;
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            nextDialog = true;
            if (!canMoveHere)
            {
                string playerPath = CharactersConfigManager.GetCharacterGameObjectPath(CharactersConfigManager.k_PlayerID);
                GameObject.Find(playerPath).GetComponent<PlayerTestController>().MuteAllPlayerControlInput();
            }
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {

        }
    }

}
