using UnityEngine;
using System.Collections;

public class MoveCapsule : MonoBehaviour
{

    private Rigidbody rb;
    private float addForce;

    void Start()
    {

        addForce = 3;
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {

        if (transform.position.x > 3)
        {
            addForce = -3f;
        }
        if (transform.position.x < -3)
        {
            addForce = 3f;
        }

        rb.AddForce(Vector3.right * addForce);

    }
}
