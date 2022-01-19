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

    // Array of spawn locations for the sanity shadows & sounds
    public GameObject[] SpawnLocations;

    public GameObject[] SpawnShadows;


    void Start()
    {
        // Selects a random game object to use as a spawn location.
        int rand = Random.Range(0, SpawnLocations.Length);

        // Selects a random game object to use as a spawn location.
        int rand2 = Random.Range(0, SpawnShadows.Length);

        // Sets Current player sanity to starter sanity, sets the trigger to false so that the player doesnt lose sanity instantly.
        SanityCurrentValue = SanityStarterValue;
        SanityTrigger = false;
    }


    void FixedUpdate()
    {
        TriggerChecker();
    }


    // If the player activates the sanity trigger, sanity starts to drain.
    void TriggerChecker()
    {
        if (SanityTrigger == true)
        {
            SanityCurrentValue -= Time.deltaTime;
            // Debug.Log("Sanity is being reduced!");
        }
    }
}
