using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    //�ν��Ͻ��� ��𼭵� ������ �� ����.
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); //
    }
    #endregion



    //�Ʒ��� �κ��丮�� ��ȭ�� ������ �� ��� ����

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    //������ ��ȭ, �ܺο��� ������ ���� ��밡�� 
    //Inventory.instance.onChangeItem += RefreshItemIcons;
    //delegate : �����Ϳ� ���� ����,�Լ� �븮 ȣ��

    private Animator markAnimator;

    //ȹ���� �������� ��� ����Ʈ
    public List<Item> items = new List<Item>();


    private int slotCnt;
    public int SlotCnt
    {
        //���� slotCnt�� ��ȣ�ϸ鼭 �ܺο��� ���� ���� �� �ֵ�����.
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }

    }

    void Start()
    {
        slotCnt = 4;
    }
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();


    }

    private FieldItems currentFieldItem = null;
    private GameObject mark;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            Debug.Log("mark active");
            currentFieldItem = collision.GetComponent<FieldItems>();


            Transform[] allChildren = currentFieldItem.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                if (child.name == "mark")
                {
                    Debug.Log("mark found");

                    mark = child.gameObject;
                    break;
                }
                else Debug.Log("mark !found");

            }

            if (mark != null)
            {

                mark.SetActive(true);

            }

        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {


            if (mark != null)

            {
                mark.SetActive(false);

                Debug.Log("mark !active");
                if (markAnimator != null)
                {

                    markAnimator.enabled = true;
                    markAnimator.Play("markoff", 0, 0f);  // "Show"�� ���ϴ� �ִϸ��̼� Clip �̸�
                }

            }


            currentFieldItem = null;

        }
    }

    public void ADDandDestroyFromField()
    {

        if (AddItem(currentFieldItem.GetItem()))
        {
            currentFieldItem.DestroyItem();
            currentFieldItem = null;


            if (mark != null)
            {
                mark.SetActive(false);
                mark = null;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentFieldItem != null && Input.GetKeyDown(KeyCode.Space))
        {
            var item = currentFieldItem.GetItem();
            // �Ź� ��������Ʈ ����
            ItemOnUseManager.Execute(item);

            bool isDestroyed = item.Use(); // Use() ���ο��� onUse.Invoke() ����
            if (isDestroyed)
            {
                ADDandDestroyFromField();
                Debug.Log("���������� ����~");
            }
            else Debug.Log("���� ����");

        }

    }




    public bool HasItemType(ItemType type)
    {
        return items.Any(item => item.itemType == type);
    }


}