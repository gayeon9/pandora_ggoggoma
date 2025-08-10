using UnityEngine;

public class MouseAddwhenDisabled : MonoBehaviour


{
    public Item item;

    public Item GetItem()
    {

        return item;// 필드에서 얻는

    }
    private void OnDisable()
    {

        Inventory.instance.AddItem(GetItem());

    }





}
