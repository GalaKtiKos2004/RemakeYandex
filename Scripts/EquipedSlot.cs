using UnityEngine;

public class EquipedSlot : InventorySlot
{
    public void PutInSlot(Item item, InventorySlot slot)
    {
        if (SlotItem != null)
            slot.PutInSlot(SlotItem);
        else
        {
            slot.DropInSlot();
        }
        base.PutInSlot(item);
    }
}
