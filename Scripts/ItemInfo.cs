using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public static ItemInfo instance;

    private Image BackGround;
    private Text Name;
    private Text Description;
    private Button UseButton;
    private Button DropButton;
    private InventorySlot Slot;

    private Item InfoItem;

    private void Start()
    {
        instance = this;

        BackGround = GetComponent<Image>();
        Name = transform.GetChild(0).GetComponent<Text>();
        Description = transform.GetChild(1).GetComponent<Text>();
        UseButton = transform.GetChild(2).GetComponent<Button>();
        DropButton = transform.GetChild(3).GetComponent<Button>();
        
        UseButton.onClick.AddListener(Use);
        DropButton.onClick.AddListener(Drop);
    }

    public void ChangeInfo(Item item)
    {
        if (item == null)
        {
            return;
        }
        Name.text = item.Name;
        Description.text = item.Description;
    }

    private void Use()
    {
        Debug.Log("Прошли первую стадию");
        if (InfoItem != null)
            UseOfItems.instance.Use(InfoItem, Slot);
        InfoItem = null;
    }

    private void Drop()
    {
        DropOfItems.instance.Drop(Slot);
    }
    
    private void Update()
    {
        if (!Inventory.instance.canvas.enabled)
        {
            Name.text = "";
            Description.text = "";
        }
    }

    public void Open(Item item, InventorySlot slot)
    {
        ChangeInfo(item);
        InfoItem = item;
        Slot = slot;
    }
}