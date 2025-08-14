using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public int slotnum;
    private Button button;

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

    private void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            // 클릭 이벤트 등록
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("[ERROR] Button 컴포넌트를 찾을 수 없습니다.");
        }
    }

    private void OnButtonClick()
    {
        if (item != null)
        {
            Debug.Log($"[DEBUG] {item?.itemName} 클릭됨");

            if (item.onUse == null)
                ItemOnUseManager.Execute(item);

            item.Use();
        }
        else
        {
            Debug.LogWarning("[WARNING] 아이템이 지정되지 않았습니다.");
        }
    }

}
