using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBreak : MonoBehaviour
{
    public GameObject oldBG;
    public GameObject oldMG;
    public GameObject mapBreak;
    public CameraShake cs;

	public GameObject groundCollider;

    public float shakeTime = 1;

    private float startTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cs.doShake)
        {
            if (Time.time - startTime > shakeTime)
            {
                cs.doShake = false;
                List<int> blockstate = new List<int>();
                blockstate.Add(0);
                PlayerTestController.instance.UnblockPlayerInput(blockstate);
				oldBG.SetActive(false);
				oldMG.SetActive(false);
				mapBreak.SetActive(true);
				groundCollider.transform.Translate(Vector3.up * -100, Space.World);;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            cs.doShake = true;
            startTime = Time.time;
            List<int> blockstate = new List<int>();
            blockstate.Add(0);
            PlayerTestController.instance.BlockPlayerInput(blockstate);
        }
    }
}
