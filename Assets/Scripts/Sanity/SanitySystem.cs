using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{
    // Sanity Values
    [Header("Sanity Settings")]

    [SerializeField] float SanityStarterValue = 100f;
    [SerializeField] float SanityCurrentValue;


    [Header("Sanity Trigger Marks")]

    [SerializeField] float SanityTriggerMark1 = 75f;
    [SerializeField] float SanityTriggerMark2 = 50f;


    [Header("Despawn Timers")]

    [SerializeField] float TimerTier1;
    [SerializeField] float TimerTier2;


    [Header("Sanity Level")]

    [SerializeField] int SanityLevel;

    public bool SanityTrigger = false;

    // Array of spawn locations for the sanity shadows & sounds
    public GameObject[] SpawnLocations;

    public GameObject[] SpawnShadows;


    void Start()
    {
        // Sets Current player sanity to starter sanity, sets the trigger to false so that the player doesnt lose sanity instantly.
        SanityCurrentValue = SanityStarterValue;
        SanityTrigger = false;

        // Set the sanity Level to 1 on begin play.
        SanityLevel = 1;
    }


    void FixedUpdate()
    {
        TriggerChecker();
    }


    // If the player activates the sanity trigger, sanity starts to drain.
    void TriggerChecker()
    {
        // If the trigger is set to true the sanity will reduce according to the time passed in game.
        if (SanityTrigger == true)
        {
            SanityCurrentValue -= Time.deltaTime;
            // Debug.Log("Sanity is being reduced!");
        }

        // Sanity Level setting to 2.
        if (SanityLevel == 1 && SanityCurrentValue <= SanityTriggerMark1 && SanityTrigger == true)
        {
            SanityTrigger = false;
            SanityCurrentValue = 74f;
            SanityLevel = 2;

            SanityLevel2();
        }

        // Sanity Level setting to 3.
        if (SanityLevel == 2 && SanityCurrentValue <= SanityTriggerMark2 && SanityTrigger == true)
        {
            SanityTrigger = false;
            SanityCurrentValue = 49f;
            SanityLevel = 3;

            SanityLevel3();
        }
    }

    void SanityLevel2()
    {
        // Selects a random game object to use as a spawn location.
        int rand = Random.Range(0, SpawnLocations.Length);

        // Selects a random game object to use as a spawn location.
        int rand2 = Random.Range(0, SpawnShadows.Length);

        // Spawning of shadows.
        Instantiate(SpawnShadows[rand2], SpawnLocations[rand].transform.position, Quaternion.identity);

        // Start Coroutine to despawn the shadow prefab.
        StartCoroutine(DespawnTimeTier1());

        Debug.Log("Current sanity level is " + SanityLevel);
    }

    void SanityLevel3()
    {
        // Selects a random game object to use as a spawn location.
        int rand = Random.Range(0, SpawnLocations.Length);

        // Selects a random game object to use as a spawn location.
        int rand2 = Random.Range(0, SpawnShadows.Length);

        // Spawning of shadows.
        Instantiate(SpawnShadows[rand2], SpawnLocations[rand].transform.position, Quaternion.identity);

        // Start Coroutine to despawn the shadow prefab.
        StartCoroutine(DespawnTimeTier2());

        Debug.Log("Current sanity level is" + SanityLevel);
    }

    IEnumerator DespawnTimeTier1()
    {
        yield return new WaitForSeconds(TimerTier1);
        Destroy(GameObject.FindWithTag("Shadows"));
    }

    IEnumerator DespawnTimeTier2()
    {
        yield return new WaitForSeconds(TimerTier2);
        Destroy(GameObject.FindWithTag("Shadows"));
    }
}