using UnityEngine;

public class MouseAddwhenDisabled : MonoBehaviour


{
    public Item item;

    public Item GetItem()
    {

        return item;// �ʵ忡�� ���

    }
    private void OnDisable()
    {

        Inventory.instance.AddItem(GetItem());

    }





}
