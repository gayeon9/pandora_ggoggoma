using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    [Header("���� ID")]
    public string itemID;

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

        // ����� ���� �ҷ�����
        item.itemLevel = ItemSaveManager.instance.GetItemLevel(itemID, _item.itemLevel);

        image.sprite = item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    public void UpdateItemLevel(int newLevel)
    {
        item.itemLevel = newLevel;
        ItemSaveManager.instance.SaveItem(itemID, newLevel);
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
