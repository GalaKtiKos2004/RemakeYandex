using System;
using UnityEngine;
using UnityEngine.UI;

public class UseOfItems : MonoBehaviour
{
    public static UseOfItems instance;


    private void Start()
    {
        instance = this;
    }

    public void Use(Item item, InventorySlot slot)
    {
        Debug.Log("Прошли вторую стадию");
        Inventory.instance.PutInEquipedSlot(item, slot);
    }
}
