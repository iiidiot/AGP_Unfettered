using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestory : MonoBehaviour {

    public GameObject fragments;


    private GameObject m_instantiatedFragments;
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
            m_instantiatedFragments = Instantiate(fragments, transform.position, transform.rotation);

            if (this.tag == "Ground")
            {
                
                int gameObjectId = this.transform.gameObject.GetInstanceID();
                if (PlayerTestController.instance.grounds.Contains(gameObjectId) )
                {
                    PlayerTestController.instance.grounds.Remove(gameObjectId);
                }
                if (Boss1ChaseController.instance.grounds.Contains(gameObjectId))
                {
                    Boss1ChaseController.instance.grounds.Remove(gameObjectId);
                }



            }
            gameObject.SetActive(false);
            //Destroy(gameObject); 
            Invoke("DestroyInstantiatedFragments", 2);
        }
          
    }

    private void DestroyInstantiatedFragments()
    {
        Destroy(m_instantiatedFragments);
    }
}
