using UnityEngine;

[DefaultExecutionOrder(-2000)]
public class ScriptableObjsInitializer : MonoBehaviour
{
    private static bool initialized = false;

    private void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }

        initialized = true;
        RunInit();
        Destroy(gameObject); // �ʱ�ȭ �� �ڱ� �ڽ� ����
    }

    private void RunInit()
    {
        var scriptableObjs = Resources.LoadAll<Item>("Item");
        Debug.Log($"[ItemInitializer] Loaded Items: {scriptableObjs.Length}");

        foreach (var so in scriptableObjs)
        {
            so.itemLevel = 1;
            so.isInventory = false;
            if (so.itemType == ItemType.Mouse)
            {
                var spr = Resources.Load<Sprite>("UI/��_�ڴ� ��");
                Debug.Log(spr != null ? "Mouse �̹��� ���� �Ϸ�" : "Mouse �̹��� �ε� ����");
                if (spr != null) so.itemImage = spr;
            }
        }
    }
}
