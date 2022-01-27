using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFull : MonoBehaviour
{
    public Inventory inventory;
    public int number;
    void Update()
    {
        if (transform.GetChild(0).gameObject.CompareTag("Empty"))
        {
            inventory.isFull[number] = false;
        }
        if (transform.GetChild(0).gameObject.CompareTag("Empty") && transform.GetChild(1).gameObject != null)
        {
            Destroy(transform.GetChild(0).gameObject);
            inventory.isFull[number] = true;
        }
    }
}
