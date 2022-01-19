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

    public float _temp;
    public float _windSpeed;
    public int _weatherType;

    private float timeElapsed;
    private float lerpTime = 60;
    private float startTempValue = 0;
    private float endTempValue = 10;

    public ParticleSystem snowClose, snowFast, snowFar;
    public PostProcessVertFog fog;


    private WeatherCalculator _wC;

    private void Start()
    {
        _wC = new WeatherCalculator();
        if (_weatherType == 2)
        {
            var eM = snowFast.emission;
            eM.rateOverTime = 2000;
            var eM1 = snowClose.emission;
            eM1.rateOverTime = 2000;
            var eM2 = snowFar.emission;
            eM2.rateOverTime = 2000;
        }
        if (_weatherType == 1 || _weatherType == 3)
        {
            var eM = snowFast.emission;
            eM.rateOverTime = 0;
            var eM1 = snowClose.emission;
            eM1.rateOverTime = 0;
            var eM2 = snowFar.emission;
            eM2.rateOverTime = 0;
        }
    }
        private void Update()
    {
        //player.transform.Translate(0.1f, 0, 0.1f);
        perlinPos.x = player.transform.position.x / 100;
        perlinPos.y = player.transform.position.z / 100;
        perlinNoise = Mathf.PerlinNoise(perlinPos.x, perlinPos.y);
        //Debug.Log(_weatherType);

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
        //Debug.Log(_weatherType);

        if (_weatherType == 2)
        {
            if (fog.height >= 150)
            {
                fog.height -= Time.deltaTime * 10;
            }
            if (snowFast.emission.rateOverTime.constant <= 2000)
            {
                var eM = snowFast.emission;
                eM.rateOverTime = eM.rateOverTime.constant + Time.deltaTime * 100;
            }
            if (snowClose.emission.rateOverTime.constant <= 40)
            {
                var eM = snowClose.emission;
                eM.rateOverTime = eM.rateOverTime.constant + Time.deltaTime * 0.5f;
            }
            if (snowFar.emission.rateOverTime.constant <= 450)
            {
                var eM = snowFar.emission;
                eM.rateOverTime = eM.rateOverTime.constant + Time.deltaTime * 30;
            }
        }
        if (_weatherType == 1 || _weatherType == 3)
        {
            if (fog.height <= 800)
            {
                fog.height += Time.deltaTime * 10;
            }
            if (snowFast.emission.rateOverTime.constant >= 0)
            {
                var eM = snowFast.emission;
                eM.rateOverTime = eM.rateOverTime.constant - Time.deltaTime * 100;
            }
            if (snowClose.emission.rateOverTime.constant >= 0)
            {
                var eM = snowClose.emission;
                eM.rateOverTime = eM.rateOverTime.constant - Time.deltaTime * 0.5f;
            }
            if (snowFar.emission.rateOverTime.constant >= 0)
            {
                var eM = snowFar.emission;
                eM.rateOverTime = eM.rateOverTime.constant - Time.deltaTime * 30;
            }
        }
    }
}
