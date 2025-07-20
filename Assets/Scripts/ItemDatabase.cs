using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//필드 아이템 생성할 스크립트 start로..

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
      instance = this;
    }
    public List<Item>itemDB = new List<Item>();
    [Space(9)]
    //필드 아이템 프리팹 들고오기
    public GameObject fieldItemPrefabs;
    public Vector3[] pos;

    private void Start()
    {
        int spawnCount = Mathf.Min(itemDB.Count, pos.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject go = Instantiate(fieldItemPrefabs, pos[i], Quaternion.identity);
            FieldItems fieldItem = go.GetComponent<FieldItems>();

            if (fieldItem != null)
            {
                fieldItem.SetItem(itemDB[i]);  // itemDB[i]와 pos[i] 1:1 대응
            }
            else
            {
                Debug.LogWarning("FieldItems 컴포넌트가 프리팹에 없습니다.");
            }
        }
        /*   for (int i = 0; i < 5; i++)
          {
              GameObject go = Instantiate(fieldItemPrefabs, pos[i], Quaternion.identity);
              go.GetComponent<FieldItems>().SetItem(itemDB[i%3]);

          }
        */
    }

}
    