using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBreak : MonoBehaviour
{
    public GameObject oldBG;
    public GameObject oldMG;
    public GameObject mapBreak;
    public CameraShake cs;
    public GameObject caveShakeAudio;

	public GameObject groundCollider;

    public float shakeTime = 1;

    public SisterController sister;

    public float startTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cs.doShake)
        {
            caveShakeAudio.SetActive(true);
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
                cs.enabled=false;
                caveShakeAudio.GetComponent<AudioSource>().Stop();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            sister.m_animator.SetBool("isRun",true);
            sister.direction = -1;
            // cs.doShake = true;
            // startTime = Time.time;
            List<int> blockstate = new List<int>();
            blockstate.Add(0);
            PlayerTestController.instance.BlockPlayerInput(blockstate);
        }
    }
}
