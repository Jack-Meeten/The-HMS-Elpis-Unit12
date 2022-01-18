using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeather : MonoBehaviour
{
    public WeatherGenerator weatherGen;
    public CharacterController cH;

    private float _temperature;
    private float _health;
    private float _hunger;
    private float _windSpeed;
    private float movementSpeedModifier;

    public AnimationCurve walkCurve;
    void Start()
    {
        _temperature = weatherGen._temp;
        _windSpeed = weatherGen._windSpeed;
        _health = 100;
        _hunger = 100;
    }

    void Update()
    {
        _windSpeed = weatherGen._windSpeed;
        //cH.movementMultiplier = walkCurve.Evaluate(Time.deltaTime * 4000);
        if (_hunger > 0)
        {
            _hunger -= Time.deltaTime * 20;
        }
        if (_hunger < 20)
        {
            movementSpeedModifier = 0.75f;
        }
        if (_hunger > 30)
        {
            _temperature = -10;
        }
        if (_hunger < 10 && _health > 0 || _temperature < -18 && _health > 0)
        {
            _health -= Time.deltaTime * 4;
        }
        if (_health < 20)
        {
            movementSpeedModifier = 0.75f;
        }
    }
}
