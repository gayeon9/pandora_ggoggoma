using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ʵ� ������ ������ ��ũ��Ʈ start��..

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
      instance = this;
    }
    public List<Item>itemDB = new List<Item>();
    [Space(9)]
    //�ʵ� ������ ������ ������
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
                fieldItem.SetItem(itemDB[i]);  // itemDB[i]�� pos[i] 1:1 ����
            }
            else
            {
                Debug.LogWarning("FieldItems ������Ʈ�� �����տ� �����ϴ�.");
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
    