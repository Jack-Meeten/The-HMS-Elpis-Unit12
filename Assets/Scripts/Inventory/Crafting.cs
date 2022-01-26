using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public Inventory inventory;

    public int i;
    public int _i;

    public string tag1;
    public string tag2;

    private bool item1 = false;
    private bool item2 = false;

    private int numberItems;

    private GameObject resultItem;
    public void CraftItem()
    {

        for (int _i = 0; _i < inventory.slots.Length; _i++)
        {
            //Debug.Log(inventory.slots[_i].transform.GetChild(0).name);

            if (inventory.slots[_i].transform.GetChild(0).gameObject == null)
            {
                break;
            }

            if (inventory.slots[_i].transform.GetChild(0).gameObject.tag == tag1)
            {
                Debug.Log("1");

                if (numberItems < 2)
                {
                    numberItems++;
                }
                else
                {
                    item1 = true;
                    if (item1 == true && item2 == true)
                    {
                        if (inventory.slots[_i].transform.childCount <= 0)
                        {
                            inventory.isFull[i] = false;
                        }
                        Debug.Log("Your ");
                        Destroy(inventory.slots[_i].transform.GetChild(0).gameObject);
                    }
                }
            }
            if (inventory.slots[_i].transform.GetChild(0).gameObject.tag == tag2)
            {
                Debug.Log("2");

                item2 = true;
                if (item1 == true && item2 == true)
                {
                    if (inventory.slots[_i].transform.childCount <= 0)
                    {
                        inventory.isFull[i] = false;
                    }
                    Debug.Log("Your ");
                    Destroy(inventory.slots[_i].transform.GetChild(0).gameObject);
                }
            }

        }

    }
}
