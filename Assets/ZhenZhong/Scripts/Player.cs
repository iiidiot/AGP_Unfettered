using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    public float speed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleMovement();
	}

    private void HandleMovement()
    {
        float translation = speed * Time.deltaTime;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0,
                                        Input.GetAxis("Vertical") * translation));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
        }
    }
}
