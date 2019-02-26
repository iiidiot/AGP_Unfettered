using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class ScenePartSwitcher : MonoBehaviour {
    public float transitionTime = 0.5f;
    public GameObject oldPart,newPart;
    public GameObject oldCam,newCam;
    public Transform player;
    public Transform playerBornPlace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
        	//1.画面变黑
        	//2.完全变黑后，切换场景，移动人物，切换相机
        	//3.画面从黑变白
            oldCam.GetComponent<ProCamera2DTransitionsFX>().TransitionExit();
            Invoke("PartSwitch", transitionTime);
        }
    }

    void PartSwitch()
    {
    	oldPart.SetActive(false);
        oldCam.SetActive(false);

        newPart.SetActive(true);
        newCam.SetActive(true);

        player.position = playerBornPlace.position;
        PlayerTestController.instance.grounds.Clear();

        //ProCamera2DTransitionsFX.Instance.TransitionEnter();
        newCam.GetComponent<ProCamera2DTransitionsFX>().TransitionEnter();
    }
}
