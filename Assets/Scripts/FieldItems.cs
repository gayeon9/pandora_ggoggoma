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
            consumable = _item.consumable
        };

        image.sprite = item.itemImage;

    }
    public Item GetItem()
    {

       return item;// 필드에서 얻는
       
    }




    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}