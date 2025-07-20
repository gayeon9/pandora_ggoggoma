using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
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
        item = null  ;
        itemIcon.gameObject.SetActive(false);

    }



}
