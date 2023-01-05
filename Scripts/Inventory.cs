using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Canvas canvas;
    public static Inventory instance;
    public Transform SlotsParent;
    public Transform EquipedParent;
    private InventorySlot[] inventorySlots = new InventorySlot[15];
    private EquipedSlot[] eqSlots = new EquipedSlot[5];

    private void Start()
    {
        instance = this;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = SlotsParent.GetChild(i).GetComponent<InventorySlot>();
        }

        for (int i = 0; i < eqSlots.Length; i++)
        {
            eqSlots[i] = EquipedParent.GetChild(i).GetComponent<EquipedSlot>();
        }
        
        canvas.enabled = false;
    }

    public void PutInEmptySlot(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].SlotItem == null)
            {
                inventorySlots[i].PutInSlot(item);
                break;
            }
        }
    }

    public void PutInEquipedSlot(Item item, InventorySlot slot)
    {
        if (item.IsSword)
            eqSlots[0].PutInSlot(item, slot);
        if (item.IsHelmet)
            eqSlots[1].PutInSlot(item, slot);
        if (item.IsArmor)
            eqSlots[2].PutInSlot(item, slot);
        if (item.IsShield)
            eqSlots[3].PutInSlot(item, slot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}
