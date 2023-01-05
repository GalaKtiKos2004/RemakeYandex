using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Item SlotItem;
    public Image icon;
    private Button SlotButton;
    private Button ImageButton;

    private void Start()
    {
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        SlotButton = GetComponent<Button>();
        ImageButton = icon.GetComponent<Button>();
        SlotButton.onClick.AddListener(SlotClicked);
        ImageButton.onClick.AddListener(SlotClicked);
    }

    public void PutInSlot(Item item)
    {
        icon.sprite = item.icon;
        SlotItem = item;
        icon.enabled = true;
    }

    public void DropInSlot()
    {
        icon.sprite = null;
        SlotItem = null;
        icon.enabled = false;
    }

    private void SlotClicked()
    {
        ItemInfo.instance.Open(SlotItem, this);
    }
}
