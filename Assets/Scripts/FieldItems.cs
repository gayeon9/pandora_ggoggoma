using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;



    public void SetItem(Item _item)
    {
        item = new Item
        {
            itemName = _item.itemName,
            itemImage = _item.itemImage,
            itemType = _item.itemType,
            onUse = _item.onUse,
            consumable = _item.consumable,
            itemScale = _item.itemScale,
            borderImage = _item.borderImage

        };

        image.sprite = item.itemImage;

        Transform borderTransform = transform.Find("Border");
        if (borderTransform != null)
        {
            SpriteRenderer borderRenderer = borderTransform.GetComponent<SpriteRenderer>();
            if (borderRenderer != null && item.borderImage != null)
            {
                borderRenderer.sprite = item.borderImage;
            }
            else
            {
                Debug.LogWarning("Border SpriteRenderer�� ���ų� �̹����� ����ֽ��ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("�ڽ� ������Ʈ 'Border'�� ã�� �� �����ϴ�.");
        }
    

}
public Item GetItem()
    {

       return item;// �ʵ忡�� ���
       
    }




    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}
