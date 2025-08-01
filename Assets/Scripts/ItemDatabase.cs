using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        int itemCount = Mathf.Min(itemDB.Count, pos.Length);

        for (int i = 0; i < itemCount; i++)
        {
            Item item = itemDB[i];
         //   item.onUse = () => itemInitializer.Handle(item);
            //**************
          
            GameObject go = Instantiate(fieldItemPrefabs, pos[i], Quaternion.identity);
            go.transform.localScale = item.itemScale;

            BoxCollider2D col = go.GetComponent<BoxCollider2D>();
            if (col != null)
            {
                Vector2 baseSize = col.size;              // ������ ���� ������ (��: (1,1))
                Vector3 scale = item.itemScale;           // ������Ʈ �ð��� ������ (��: (0.5, 0.5, 1))

                Vector2 scaledSize = new Vector2(
                    baseSize.x * scale.x + 0.2f,
                    baseSize.y * scale.y + 0.2f
                );

                col.size = scaledSize;
            }

            Transform mark = go.transform.Find("mark");

            mark.SetParent(go.transform);
            mark.localPosition = new Vector3(0f, 0f, 0f);

            // �ڽ� ũ�� ���� ����
            //   Vector3 parentScale = go.transform.lossyScale;
            Vector3 parentScale = go.transform.lossyScale;

            Vector3 safeScale = new Vector3(
                parentScale.x != 0 ? 1.5f / parentScale.x : 1f,
                parentScale.y != 0 ? 1.5f / parentScale.y : 1f,
                parentScale.z != 0 ? 1.5f / parentScale.z : 1f
            );

            mark.localScale = safeScale;


            FieldItems fieldItem = go.GetComponent<FieldItems>();

            if (fieldItem != null)
            {
                fieldItem.SetItem(itemDB[i]);  
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
    