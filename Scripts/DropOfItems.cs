using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOfItems : MonoBehaviour
{
    public static DropOfItems instance;

    private void Start()
    {
        instance = this;
    }

    public void Drop(InventorySlot slot)
    {
        slot.DropInSlot();
    }
}
