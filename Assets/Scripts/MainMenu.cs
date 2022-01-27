using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject controls;
    public void OnStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnControls()
    {
        if (controls.activeInHierarchy == false)
        {
            controls.SetActive(true);
        }
        else if (controls.activeInHierarchy == true)
        {
            controls.SetActive(false);
        }
    }
}
