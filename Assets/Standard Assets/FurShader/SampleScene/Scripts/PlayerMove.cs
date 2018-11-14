using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    CharacterController charcont;
    public float rotationSpeed;
    public float movementSpeed;


    void Start()
    {
        charcont = GetComponent<CharacterController>();
    }


    void Update()
    {

        charcont.Move(Input.GetAxis("Vertical") * charcont.transform.forward * movementSpeed * Time.deltaTime);
        charcont.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * rotationSpeed * Time.deltaTime);

    }

}
