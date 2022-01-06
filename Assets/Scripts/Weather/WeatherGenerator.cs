using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class WeatherGenerator : MonoBehaviour
{
    public GameObject player;
    //pn width and height
    public Vector2 perlinPos;
    [SerializeField] private float perlinNoise = 0f;

    private float _temp;
    private float _windSpeed;
    private int _weatherType;

    private float timeElapsed;
    private float lerpTime = 60;
    private float startTempValue = 0;
    private float endTempValue = 10;

    private WeatherCalculator _wC;

    private void Start()
    {
        _wC = new WeatherCalculator();
    }

    private void Update()
    {
        //player.transform.Translate(0.1f, 0, 0.1f);
        perlinPos.x = player.transform.position.x / 100;
        perlinPos.y = player.transform.position.z / 100;
        perlinNoise = Mathf.PerlinNoise(perlinPos.x, perlinPos.y);
        Debug.Log(_weatherType);

        //calls weather calculator class to get the temp, weather type and wind speed/direction
        _wC._weatherCalculator(perlinNoise, out float tempOut, out int weatherTypeOut, out float windSpeedOut);
        endTempValue = tempOut;
        _weatherType = weatherTypeOut;
        _windSpeed = windSpeedOut;
        startTempValue = _temp;
        //temp lerp
        _temp = Mathf.SmoothStep(startTempValue, endTempValue, timeElapsed / lerpTime);
        timeElapsed += Time.deltaTime;
        //Debug.Log(_temp);
    }
}
