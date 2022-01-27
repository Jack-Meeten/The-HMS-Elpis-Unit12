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

    public GameObject empty;

    private GameObject resultItem;

    public List<GameObject> woodList = new List<GameObject>();
    public List<GameObject> stoneList = new List<GameObject>();
    public List<GameObject> clothList = new List<GameObject>();

    public int tag1Num;
    public int tag2Num;
    public GameObject button1;
    public GameObject button2;

    public GameObject campFireRecipie;

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
                woodList.Add(inventory.slots[_i].transform.GetChild(0).gameObject);

                if (numberItems < 2)
                {
                    numberItems++;
                }
                if (numberItems == 2)
                {
                    item1 = true;
                    Debug.Log("Your ");
                    if (item1 == true && item2 == true)
                    {
                        if (inventory.slots[_i].transform.childCount <= 0)
                        {
                            inventory.isFull[i] = false;
                        }
                        Destroy(inventory.slots[_i].transform.GetChild(0).gameObject);
                        Destroy(woodList[0]);
                        Destroy(stoneList[0]);
                        Instantiate(empty, woodList[0].transform.parent, true);
                        Instantiate(empty, stoneList[0].transform.parent, true);
                        woodList.Remove(woodList[1]);
                        woodList.Remove(woodList[0]);
                        stoneList.Remove(stoneList[0]);
                        Instantiate(empty, inventory.slots[_i].transform, true);
                        Debug.Log("crafted");

                        for (int ii = 0; ii <= inventory.slots.Length; ii++)
                        {
                            if (inventory.isFull[ii])
                            {
                                inventory.isFull[ii] = false;
                            }
                            if (inventory.isFull[ii] == false)
                            {
                                //add to inventory
                                inventory.isFull[ii] = true;
                                Destroy(inventory.slots[ii].transform.GetChild(0).gameObject);
                                Instantiate(campFireRecipie, inventory.slots[ii].transform, false);
                                break;
                            }
                        }
                    }
                }
            }
            //can duplicate above section to bottom one for more felxability
            if (inventory.slots[_i].transform.GetChild(0).gameObject.tag == tag2)
            {
                Debug.Log("2");
                stoneList.Add(inventory.slots[_i].transform.GetChild(0).gameObject);

                item2 = true;
                if (item1 == true && item2 == true)
                {
                    if (inventory.slots[_i].transform.childCount <= 0)
                    {
                        inventory.isFull[i] = false;
                    }
                    Destroy(inventory.slots[_i].transform.GetChild(0).gameObject);
                    Destroy(woodList[0]);
                    Destroy(stoneList[0]);
                    Instantiate(empty, woodList[0].transform.parent, true);
                    Instantiate(empty, stoneList[0].transform.parent, true);
                    woodList.Remove(woodList[1]);
                    woodList.Remove(woodList[0]);
                    stoneList.Remove(stoneList[0]);
                    Instantiate(empty, inventory.slots[_i].transform, true);
                    Debug.Log("crafted");

                    for (int ii = 0; ii <= inventory.slots.Length; ii++)
                    {
                        if (inventory.isFull[ii])
                        {
                            inventory.isFull[ii] = false;
                        }
                        if (inventory.isFull[ii] == false)
                        {
                            //add to inventory
                            inventory.isFull[ii] = true;
                            Destroy(inventory.slots[ii].transform.GetChild(0).gameObject);
                            Instantiate(campFireRecipie, inventory.slots[ii].transform, false);
                            break;
                        }
                    }
                }
            }
        }
    }
}
