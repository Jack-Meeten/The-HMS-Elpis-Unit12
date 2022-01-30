using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public SanitySystem SanityScript;


    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Sanity");
        SanityScript = g.GetComponent<SanitySystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SanityScript.SanityTrigger = true;
        Debug.Log("Sanity Reduction triggered!");
    }
}
