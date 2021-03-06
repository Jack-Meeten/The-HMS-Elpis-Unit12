using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    public GameObject empty;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("PlayerScripts").GetComponent<Inventory>();
    }
    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
    public void DropItem()
    {
        foreach (Transform child in transform)
        {        
            child.GetComponent<Spawn>().SpawnDropped();
            GameObject.Destroy(child.gameObject);
            Instantiate(empty, gameObject.transform, true);
        }
    }
    public void DropItemDIE()
    {
        GameObject.Destroy(transform.GetChild(0).gameObject);
        Instantiate(empty, gameObject.transform, true);
    }
}
