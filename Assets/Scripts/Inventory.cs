using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //�̱��� : �� ���� �����Ǵ� Ŭ����
    //> �� �ν��Ͻ��� ��𼭵� ������ �� ����.
    #region Singleton
    public static Inventory instance;
    private void Awake()
    { 
        //inventory ��ũ��Ʈ�� �� �� �̻� ������Ʈ��
        //��ϵǴ� ��� ������ ��?
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
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
        //���� �ٲ�� �ڵ����� �̺�Ʈ ��ȭ( ���Ժ�ȭ, ��ưȰ��ȭ....)
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
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    private FieldItems currentFieldItem = null;
    private GameObject mark;

    private void OnTriggerEnter2D(Collider2D collision)
      {
          if(collision.CompareTag("FieldItem"))
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
            }

            if (mark != null)
            {    mark.SetActive(true);
                markAnimator = mark.GetComponent<Animator>();
                // Animator anim = mark.GetComponent<Animator>();
                if (markAnimator != null)
                {
                    markAnimator.enabled = true;
                    markAnimator.Play("mark", 0, 0f);
                    Debug.Log("markoff �ִϸ��̼� �����");
                 
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {

            if (mark != null)
            {
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



    // Update is called once per frame
    void Update()
    {
        if (currentFieldItem != null && Input.GetKeyDown(KeyCode.Space))
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
            else
            {
                Debug.Log("�κ��丮�� ���� á���ϴ�."); // �ʿ�� UI �޽��� ���
            }
        }
    }
}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //�̱��� : �� ���� �����Ǵ� Ŭ����
    //> �� �ν��Ͻ��� ��𼭵� ������ �� ����.
    #region Singleton
    public static Inventory instance;
    private void Awake()
    { 
        //inventory ��ũ��Ʈ�� �� �� �̻� ������Ʈ��
        //��ϵǴ� ��� ������ ��?
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
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


    //ȹ���� �������� ��� ����Ʈ
    public List<Item> items = new List<Item>();


    private int slotCnt;
    public int SlotCnt
    {
        //���� slotCnt�� ��ȣ�ϸ鼭 �ܺο��� ���� ���� �� �ֵ�����.
        //���� �ٲ�� �ڵ����� �̺�Ʈ ��ȭ( ���Ժ�ȭ, ��ưȰ��ȭ....)
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
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    private FieldItems currentFieldItem = null;

    private void OnTriggerEnter2D(Collider2D collision)
      {
          if(collision.CompareTag("FieldItem"))
          {
                
              FieldItems fieldItems = collision.GetComponent<FieldItems>();
           
          }
      }

     


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (AddItem(fieldItems.GetItem()))
                fieldItems.DestroyItem();
        }
    }
}

 */