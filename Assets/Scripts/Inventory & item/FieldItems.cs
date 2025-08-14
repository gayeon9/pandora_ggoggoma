using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
  //  public SpriteRenderer image;



    public void SetItem(Item _item)
    {
        item = new Item
        {
            itemName = _item.itemName,
            itemImage = _item.itemImage,
            itemType = _item.itemType,
            onUse = _item.onUse,
            isInventory = _item.isInventory
        };

     //   image.sprite = item.itemImage;

    }
    public Item GetItem()
    {

        return item;// 필드에서 얻는

    }




    public void DestroyItem()
    {

       gameObject.SetActive(false);
      //  Destroy(gameObject);
        
    }

    private void OnEnable()
    {
        if (item.isInventory)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
{
        /* if(item.onUse == null)
         { ItemOnUseManager.Execute(item);
             Debug.Log("20250812 아이템에 델리게이트 변수 집어넣음.");
         }
         if (item.Use()) { Inventory.instance.AddItem(item); Debug.Log(""); }
 */

        if (item != null)
        {

            Debug.Log($"[DEBUG] {item?.itemName} 필드에서 클릭됨");
            if (item.onUse == null)
            {
               ItemOnUseManager.Execute(item);
            }
           item.Use();

        }
        else Debug.Log("도대체 왜 아이템이 null인 것임.");

        //item.IncreaseLevel(); // 클릭 시 레벨 증가
        //20250810
        /*if (Inventory.instance.AddItem(GetItem()))
            {
                Debug.Log(item.itemName + "을(를) 인벤토리에 추가했습니다.");
                DestroyItem();
            }
            else
            {
                Debug.Log("인벤토리가 가득 찼습니다!");
            }*/
    }


}

