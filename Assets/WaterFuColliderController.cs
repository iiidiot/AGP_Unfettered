using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFuColliderController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Fire")
    //    {
    //        collision.collider.GetComponent<FireWallDestruct>().Do();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            other.GetComponent<FireWallDestruct>().Do();
        }
    }


}
