using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    private PlayerWeather player;
    void Start()
    {
        player = FindObjectOfType<PlayerWeather>();
    }

    
    public void HungerEat()
    {
        player._hunger = player._hunger + 20;
    }
}
