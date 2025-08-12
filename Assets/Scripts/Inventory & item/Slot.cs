using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{

    public int slotnum;

    public Item item;
    //image 유니티 엔진 다른거 가능성
    public Image itemIcon;
    public Sprite ItemImage { get; set; }
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        if(itemIcon != null )  itemIcon.gameObject.SetActive(true);
       
    }

    public void RemoveSlot()
    {
        item = null;
        if (itemIcon != null) itemIcon.gameObject.SetActive(false);
     
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(item!=null)
        {

            Debug.Log($"[DEBUG] {item?.itemName} 클릭됨");
            if (item.onUse == null)  ItemOnUseManager.Execute(item);
            item.Use();

        }
        /*  if (item != null)
          {

          //    bool isDestroyed = item.Use(); // Use() 내부에서 onUse.Invoke() 실행
              if (true)
              {
                  Debug.Log("아이템 삭제완료");
                  Inventory.instance.RemoveItem(slotnum);
              } 
              else
              {
                  Debug.LogWarning($"아이템 삭제 X");
              }
          }*/

    }
}
