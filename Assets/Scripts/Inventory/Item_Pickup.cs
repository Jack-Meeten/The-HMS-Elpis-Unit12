using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("PlayerScripts").GetComponent<Inventory>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerScripts"))
        {
            for (int i = 0; i <= inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //add to inventory
                    inventory.isFull[i] = true;
                    Destroy(inventory.slots[i].transform.GetChild(0).gameObject);
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
