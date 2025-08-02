using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public List<Item> itemDB = new List<Item>();

    [Space(9)]
    public GameObject fieldItemPrefabs;
    public Vector3[] pos;

    // ��: itemInitializer�� �ܺο��� �Ҵ��ϰų� null ó�� �ʿ�
    public itemInitializer itemInitializer;

    private void Start()
    {
        int itemCount = Mathf.Min(itemDB.Count, pos.Length);

        for (int i = 0; i < itemCount; i++)
        {
            Item item = itemDB[i];
            item.onUse = () => itemInitializer.Handle(item);

            GameObject go = Instantiate(fieldItemPrefabs, pos[i], Quaternion.identity);
            FieldItems fieldItem = go.GetComponent<FieldItems>();

            if (fieldItem != null)
            {
                // ������ ���� ID ���� (���̸�_������Ÿ��_�ε���)
                string uniqueID = $"{SceneManager.GetActiveScene().name}_{item.itemType}_{i}";
                fieldItem.itemID = uniqueID;

                fieldItem.SetItem(item);
            }
            else
            {
                Debug.LogWarning("FieldItems ������Ʈ�� �����տ� �����ϴ�.");
            }
        }

        //���� �ִ� ������ ���� �α� ���(���� ���� �Ϸ� �� ��������)
        //�����۷��� ���� Ȯ�ο�
        LogAllItemsInScene();
    }

    private void LogAllItemsInScene()
    {
        FieldItems[] allItems = FindObjectsOfType<FieldItems>();

        Debug.Log($"���� �� '{SceneManager.GetActiveScene().name}' ������ ����: {allItems.Length}");

        foreach (var fieldItem in allItems)
        {
            Debug.Log($"ID: {fieldItem.itemID}, �̸�: {fieldItem.item.itemName}, ����: {fieldItem.item.itemLevel}");
        }
    }
}
