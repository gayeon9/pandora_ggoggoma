using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{

    public int slotnum;

    public Item item;
    //image 유니티 엔진 다른거 가능성
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
        Debug.Log($"[DEBUG] {item?.itemName} 클릭됨");

        if (item != null && item.Use())
        {
            Debug.Log("[DEBUG] onUse 성공 → 아이템 삭제됨");
            Inventory.instance.RemoveItem(slotnum);
        }
        else
        {
            Debug.LogWarning($"[DEBUG] onUse 실패 or null: {item?.itemName}");
        }
    }
}
