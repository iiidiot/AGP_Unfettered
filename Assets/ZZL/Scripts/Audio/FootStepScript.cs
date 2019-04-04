using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepScript : MonoBehaviour
{
    void OnStep()
    {
        SoundController.PlaySound(0);
    }
}
