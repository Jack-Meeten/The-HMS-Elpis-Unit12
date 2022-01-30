using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWeather : MonoBehaviour
{
    public WeatherGenerator weatherGen;
    public CharacterController cH;
    public PostProcessVertFog fog;

    private float _temperature;
    public float _health;
    public float _hunger;
    private float movementSpeedModifier;
    private bool isHeat = false;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI tempText;
    public GameObject debugUI;
    private bool uiAcitve = false;

    public GameObject DeathScreen;

    public AudioSource Steps;

    public TextMeshProUGUI uiHealth;
    public TextMeshProUGUI uiHunger;
    public TextMeshProUGUI uiTemp;

    void Start()
    {
        _health = 100;
        _hunger = 100;
    }

    void Update()
    {
        uiHealth.text = "Health:  " + _health;
        uiHunger.text = "Hunger:  " + _hunger;
        uiTemp.text = "Temperature:  " + weatherGen._temp;

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

        if (GetComponent<Rigidbody>().velocity.magnitude !=0)
        {
            Steps.mute = false;
        }
        if (GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            Steps.mute = true;
        }
    }

    public void Death()
    {
        Debug.Log("Dead");
        DeathScreen.SetActive(true);
        Time.timeScale = 0.1f;
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Heat")
        {
            isHeat = true;
            _temperature = -2;
        }
        if (other.tag == "DropZone")
        {
            Debug.Log("Zone");
            _temperature = -30;
            fog.height -= Time.deltaTime * 80;
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
