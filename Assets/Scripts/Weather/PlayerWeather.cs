using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerWeather : MonoBehaviour
{
    public WeatherGenerator weatherGen;
    public CharacterController cH;

    private float _temperature;
    private float _health;
    private float _hunger;
    private float movementSpeedModifier;
    private bool isHeat = false;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI tempText;
    public GameObject debugUI;
    private bool uiAcitve = false;

    public AnimationCurve walkCurve;

    void Start()
    {
        _health = 100;
        _hunger = 100;
    }

    void Update()
    {
        if (!isHeat)
        {
            _temperature = weatherGen._temp;
        }
        cH.movementMultiplier = (10 * weatherGen._windSpeed) * movementSpeedModifier;//walkCurve.Evaluate(Time.deltaTime * 4000);

        healthText.text = "Health:" + _health;
        hungerText.text = "Hunger:" + _hunger;
        tempText.text = "Temp" + _temperature + "C";

        if (_hunger > 0 && !isHeat)
        {
            _hunger -= Time.deltaTime * 0.2f;
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
        if (_health <= 0)
        {
            Death();
        }
        else
        {
            movementSpeedModifier = 1;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (uiAcitve)
            {
                Debug.Log("not active");
                debugUI.SetActive(false);
                uiAcitve = false;
            }
            if (!uiAcitve)
            {
                Debug.Log("active");
                debugUI.SetActive(true);
                uiAcitve = true;
            }
        }
    }

    public void Death()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Heat")
        {
            isHeat = true;
            _temperature = -2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Heat")
        {
            isHeat = false;
        }
    }
}
