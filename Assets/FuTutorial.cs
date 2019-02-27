using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Com.LuisPedroFonseca.ProCamera2D;

public class FuTutorial : MonoBehaviour {
	public PlayableDirector pd;
	public GameObject player;

	public ProCamera2DCinematics camCinema;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision coll)
    {
    	//1. 移动相机
    	//2. 禁用player
    	//3. 调用timeline
    	//4. 还原player脚本
    	//5. 销毁trigger
        if (coll.collider.tag == "Player")
        {
        	pd.Play();
            camCinema.Play();
            List<int> blockstate = new List<int>();
            blockstate.Add(0);
            PlayerTestController.instance.BlockPlayerInput(blockstate);
        }
    }

    public void CinematicFinish()
    {
    	List<int> blockstate = new List<int>();
        blockstate.Add(0);
        PlayerTestController.instance.UnblockPlayerInput(blockstate);
    	Destroy(gameObject);
    }
}
