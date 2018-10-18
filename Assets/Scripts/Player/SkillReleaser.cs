using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReleaser : MonoBehaviour {

    public GameObject obj;
    public GameObject obj2;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void releaseSkill(string FuName)
    {
        Transform player = PlayerTestController.Instance.transform;
        float h_direction = player.rotation.y > 179 ? 1 : -1;

        if (FuName == "FireBall")
        {
            obj = Instantiate(obj, player.position, player.rotation) as GameObject;
            Rigidbody r = obj.GetComponent<Rigidbody>();
            r.velocity = new Vector2(h_direction * 15f, r.velocity.y);
        }
        if(FuName == "FrostBall")
        {
            obj2 = Instantiate(obj2, player.position, player.rotation) as GameObject;
            Rigidbody r = obj2.GetComponent<Rigidbody>();
            r.velocity = new Vector2(h_direction * 15f, r.velocity.y);
        }
    }
}
