using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using TMPro;
using UnityEngine.UI;

public class WeatherGenerator : MonoBehaviour
{
    public GameObject player;
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

    public TextMeshProUGUI tempText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI perlinText;
    public TextMeshProUGUI tranText;
    public RawImage transIm;
    public TextMeshProUGUI closeText;
    public RawImage closeIm;
    public TextMeshProUGUI farText;
    public RawImage farIm;
    public TextMeshProUGUI fastText;
    public RawImage fastIm;
    public TextMeshProUGUI fogText;
    public RawImage fogIm;

    public AudioSource weatherSound;

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
        perlinNoise = Mathf.PerlinNoise(player.transform.position.x / 450, player.transform.position.z / 450);
        tempText.text = "Temp:" + _temp + "C";
        typeText.text = "Type:" + _weatherType;
        perlinText.text = "Perlin:" + perlinNoise;

        //calls weather calculator class to get the temp, weather type and wind speed/direction
        _wC._weatherCalculator(perlinNoise, out float tempOut, out int weatherTypeOut, out float windSpeedOut);
        endTempValue = tempOut;
        _weatherType = weatherTypeOut;
        _windSpeed = windSpeedOut;
        startTempValue = _temp;
        _temp = Mathf.SmoothStep(startTempValue, endTempValue, timeElapsed / lerpTime);
        timeElapsed += Time.deltaTime;

        if (_weatherType == 2)
        {
            if (fog.height >= 1100)
            {
                fog.height -= Time.deltaTime * 40;
                fogText.text = fog.height.ToString();
                fogIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            if (snowFast.emission.rateOverTime.constant <= 2000)
            {
                var eM = snowFast.emission;
                eM.rateOverTime = eM.rateOverTime.constant + Time.deltaTime * 100;
                fastText.text = eM.rateOverTime.constant.ToString();
                fastIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            if (snowClose.emission.rateOverTime.constant <= 40)
            {
                var eM = snowClose.emission;
                eM.rateOverTime = eM.rateOverTime.constant + Time.deltaTime * 0.5f;
                closeText.text = eM.rateOverTime.constant.ToString();
                closeIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            if (snowFar.emission.rateOverTime.constant <= 450)
            {
                var eM = snowFar.emission;
                eM.rateOverTime = eM.rateOverTime.constant + Time.deltaTime * 30;
                farText.text = eM.rateOverTime.constant.ToString();
                fastIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            else
            {
                tranText.text = "Done";
                transIm.color = Color.green;
                fastIm.color = Color.green;
                closeIm.color = Color.green;
                farIm.color = Color.green;
                fogIm.color = Color.green;
            }
            if (weatherSound.volume < 1)
            {
                weatherSound.volume = weatherSound.volume + Time.deltaTime / 30;
            }
        }
        if (_weatherType == 1 || _weatherType == 3)
        {
            if (fog.height <= 3500)
            {
                fog.height += Time.deltaTime * 40;
                fogText.text = fog.height.ToString();
                fogIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            if (snowFast.emission.rateOverTime.constant >= 0)
            {
                var eM = snowFast.emission;
                eM.rateOverTime = eM.rateOverTime.constant - Time.deltaTime * 100;
                fastText.text = eM.rateOverTime.constant.ToString();
                fastIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            if (snowClose.emission.rateOverTime.constant >= 0)
            {
                var eM = snowClose.emission;
                eM.rateOverTime = eM.rateOverTime.constant - Time.deltaTime * 0.5f;
                closeText.text = eM.rateOverTime.constant.ToString();
                closeIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            if (snowFar.emission.rateOverTime.constant >= 0)
            {
                var eM = snowFar.emission;
                eM.rateOverTime = eM.rateOverTime.constant - Time.deltaTime * 30;
                farText.text = eM.rateOverTime.constant.ToString();
                farIm.color = Color.red;
                tranText.text = "Transitioning";
                transIm.color = Color.red;
            }
            else
            {
                tranText.text = "Done";
                transIm.color = Color.green;
                fastIm.color = Color.green;
                closeIm.color = Color.green;
                farIm.color = Color.green;
                fogIm.color = Color.green;
            }
            if (weatherSound.volume > 0)
            {
                weatherSound.volume = weatherSound.volume - Time.deltaTime / 30;
            }
        }
    }
}
