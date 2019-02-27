using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Com.LuisPedroFonseca.ProCamera2D;

public class FirstSeeMonster : MonoBehaviour {
	public PlayableDirector pd;

	public GameObject player;
	public GameObject t4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll)
    {
    	//1. 移动相机
    	//2. 禁用player脚本
    	//3. 调用timeline
    	//4. 还原player脚本，显示教程
    	//5. 销毁trigger
        if (coll.collider.tag == "Player")
        {
            Camera.main.GetComponent<ProCamera2DCinematics>().Play();
            pd.Play();
            List<int> blockstate = new List<int>();
            blockstate.Add(0);
            PlayerTestController.instance.BlockPlayerInput(blockstate);
        }
    }

    public void FSMCinematicFinish()
    {
    	List<int> blockstate = new List<int>();
        blockstate.Add(0);
        PlayerTestController.instance.UnblockPlayerInput(blockstate);
        t4.SetActive(true);
    	Destroy(gameObject);
    }
}
