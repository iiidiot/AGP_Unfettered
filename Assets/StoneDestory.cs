using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestory : MonoBehaviour {

    public GameObject fragments;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log( this.name + ": " +  collision.collider.name);
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Instantiate(fragments, transform.position, transform.rotation);

            if (this.tag == "Ground")
            {
                PlayerTestController playerTestController = CharactersConfigManager.GetPlayerGameObject().GetComponent<PlayerTestController>();
                int gameObjectId = this.transform.gameObject.GetInstanceID();
                if (playerTestController.grounds.Contains(gameObjectId) )
                {
                    playerTestController.grounds.Remove(gameObjectId);
                }

            }
            Destroy(gameObject);
        }
          
    }

}
