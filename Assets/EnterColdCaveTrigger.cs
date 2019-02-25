using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class EnterColdCaveTrigger : MonoBehaviour {

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
            ProCamera2DTransitionsFX.Instance.TransitionExit();
        }
    }
}
