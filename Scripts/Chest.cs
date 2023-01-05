using System;
using UnityEngine;
using Random = System.Random;

public class Chest : Collectable
{
    public Animator animator;
    public Item[] RareWeapons;
    public Item[] CommonWeapons;
    public Item[] Helmets;
    public Item[] Armor;
    public Item[] Shields;
    private int value;

    protected override void OnCollect()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !(animator.GetBool("OpenChest")))
        {
            animator.SetBool("OpenChest", true);

            value = new Random().Next(1, 85);
            if (value <= 2)  // Rare Weapon
            {
                Inventory.instance.PutInEmptySlot(RareWeapons[new Random().Next(0, RareWeapons.Length)]);
            }
            else if (value <= 40) // Common Weapon
            {
                Inventory.instance.PutInEmptySlot(CommonWeapons[new Random().Next(0, CommonWeapons.Length)]);
            }
            else if (value <= 55) // Helmet
            {
                Inventory.instance.PutInEmptySlot(Helmets[new Random().Next(0, Helmets.Length)]);
            }
            else if (value <= 70) // Shield
            {
                Inventory.instance.PutInEmptySlot(Shields[new Random().Next(0, Shields.Length)]);
            }
            else if (value <= 85) // Armor
            {
                Inventory.instance.PutInEmptySlot(Armor[new Random().Next(0, Armor.Length)]);
            }
            else
            {
                Debug.Log("Акссесуар");
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (animator.GetBool("OpenChest"))
        {
            animator.SetInteger("Frames", animator.GetInteger("Frames") + 1);
        }
    }
}
