using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{

    public int slotnum;

    public Item item;
    //image ����Ƽ ���� �ٸ��� ���ɼ�
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot() 
    {
        item = null;
        itemIcon.gameObject.SetActive(false);

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"[DEBUG] {item?.itemName} Ŭ����");

        if (item != null && item.Use())
        {
            Debug.Log("[DEBUG] onUse ���� �� ������ ������");
            Inventory.instance.RemoveItem(slotnum);
        }
        else
        {
            Debug.LogWarning($"[DEBUG] onUse ���� or null: {item?.itemName}");
        }
    }
}
