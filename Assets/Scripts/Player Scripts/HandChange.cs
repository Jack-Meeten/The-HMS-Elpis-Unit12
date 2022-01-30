using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChange : MonoBehaviour
{
    [SerializeField] GameObject Hands;
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject Sword;
    [SerializeField] int selectedItem;


    void Update()
    {
        IntLimiter();
        ScrollUse();
        ChangeItem();
    }

    void IntLimiter()
    {
        if (selectedItem <= 0)
        {
            selectedItem = 3;
        }

        if (selectedItem >= 2)
        {
            selectedItem = 0;
        }
    }

    void ScrollUse()
    {
        if (Input.mouseScrollDelta.y == 1f)
        {
            selectedItem = +1;
        }

        if (Input.mouseScrollDelta.y == -1f)
        {
            selectedItem = -1;
        }
    }

    void ChangeItem()
    {
        if (selectedItem == 0)
        {
            Hands.SetActive(true);
            Gun.SetActive(false);
            Sword.SetActive(false);
        }

        if (selectedItem == 1)
        {
            Gun.SetActive(true);
            Hands.SetActive(false);
            Sword.SetActive(false);
        }

        if (selectedItem == 2)
        {
            Sword.SetActive(true);
            Hands.SetActive(false);
            Gun.SetActive(false);
        }
    }
}
