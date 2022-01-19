using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{
    // Sanity Values
    [Header("Sanity Settings")]
    [SerializeField] float SanityStarterValue = 100f;
    [SerializeField] float SanityCurrentValue;
    public bool SanityTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        SanityCurrentValue = SanityStarterValue;
        SanityTrigger = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TriggerChecker();
    }

    void TriggerChecker()
    {
        if (SanityTrigger == true)
        {
            SanityCurrentValue -= Time.deltaTime;
            Debug.Log("Sanity is being reduced!");
        }
    }
}
