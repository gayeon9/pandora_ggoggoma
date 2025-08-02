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

    // 예: itemInitializer는 외부에서 할당하거나 null 처리 필요
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
                // 안전한 고유 ID 생성 (씬이름_아이템타입_인덱스)
                string uniqueID = $"{SceneManager.GetActiveScene().name}_{item.itemType}_{i}";
                fieldItem.itemID = uniqueID;

                fieldItem.SetItem(item);
            }
            else
            {
                Debug.LogWarning("FieldItems 컴포넌트가 프리팹에 없습니다.");
            }
        }

        //씬에 있는 아이템 상태 로그 출력(게임 제작 완료 후 삭제예정)
        //아이템레벨 연동 확인용
        LogAllItemsInScene();
    }

    private void LogAllItemsInScene()
    {
        FieldItems[] allItems = FindObjectsOfType<FieldItems>();

        Debug.Log($"현재 씬 '{SceneManager.GetActiveScene().name}' 아이템 개수: {allItems.Length}");

        foreach (var fieldItem in allItems)
        {
            Debug.Log($"ID: {fieldItem.itemID}, 이름: {fieldItem.item.itemName}, 레벨: {fieldItem.item.itemLevel}");
        }
    }
}
