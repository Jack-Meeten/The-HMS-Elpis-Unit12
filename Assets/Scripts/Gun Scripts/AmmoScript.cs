using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [SerializeField] float ammoDmg;

    // Start is called before the first frame update
    void Start()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Dealt " + ammoDmg + " damage!");
        Destroy(gameObject);
    }
}
