using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCanvas : MonoBehaviour
{
    public Canvas prefab;

	// Use this for initialization
	void Start ()
    {
        Instantiate(prefab);
	}
}
