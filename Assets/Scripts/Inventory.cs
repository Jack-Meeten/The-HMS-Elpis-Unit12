using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //inventory slots / status.
    public bool[] isFull;
    public GameObject[] slots;

    //window toggle and mouse freedom.
    private KeyCode openInventory = KeyCode.I;
    private bool mouseFree;
    public GameObject inventoryWindow;

    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(openInventory))
        {
            if(mouseFree == false)
            {
                FreeMouse();
            }
            else if(mouseFree == true)
            {
                CloseWindow();
            }
        }
    }
    public void FreeMouse()
    {
        mouseFree = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inventoryWindow.SetActive(true);

    }
    public void CloseWindow()
    {
        mouseFree = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventoryWindow.SetActive(false);
    }
}
