using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public SanitySystem SanityScript;

    private void OnTriggerEnter(Collider other)
    {
        SanityScript.SanityTrigger = true;
        Debug.Log("Sanity Reduction triggered!");
    }
}
