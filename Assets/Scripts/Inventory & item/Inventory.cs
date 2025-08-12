using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
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
    }
    #endregion




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
        slotCnt = 9;
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


    public void AddItemAndDestroyFromField()
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
    public bool HasItemType(ItemType type)
    {
        return items.Any(item => item.itemType == type);
    }


    public bool AddItemByType(ItemType type)
    {
        if (ItemDatabase.instance == null || ItemDatabase.instance.itemDB == null)
        {
            Debug.LogError("[Inventory] ItemDatabase가 준비되지 않았습니다.");
            return false;
        }

        // DB에서 프로토타입(원본 에셋) 찾기
        Item proto = ItemDatabase.instance.itemDB
            .FirstOrDefault(i => i.itemType == type);

        if (proto == null)
        {
            Debug.LogWarning($"[Inventory] DB에 {type} 타입 아이템이 없습니다.");
            return false;
        }

        //  ScriptableObject는 반드시 Instantiate로 복제
        Item itemCopy = ScriptableObject.Instantiate(proto);

        // (선택) 런타임 초기화 값 세팅
        // itemCopy.itemLevel = 0;   // 기본 레벨을 0으로 시작시키고 싶다면
        // itemCopy.consumable = proto.consumable;

        // (중요) 런타임 델리게이트 바인딩이 필요하다면 여기서 다시 연결
        // SO에는 델리게이트가 직렬화되지 않으니, 실행 시점에 재바인딩하세요.
        ItemOnUseManager.Execute(itemCopy); // onUse 등 설정

        items.Add(itemCopy);
        onChangeItem?.Invoke();             // UI 갱신 이벤트
        Debug.Log($"[Inventory] {type} 아이템 복제본을 인벤토리에 추가했습니다. (총 {items.Count})");
        return true;


    }

    public void AddItemNode(Item item)
    {
        if (item != null)
        {
            if (items.Count < SlotCnt)
            {
                Item itemCopy = ScriptableObject.Instantiate(item);

                items.Add(itemCopy);
                if (onChangeItem != null)
                {
                    Debug.Log($"{item.itemName} 아이템을 인벤토리에 추가했습니다.");
                    onChangeItem.Invoke();
                }
            }
        }


    }





    // 2. 아이템 검색
    public Item GetItemByType(ItemType type)
    {
        return items.FirstOrDefault(item => item.itemType == type);
    }

    // 3. 아이템 삭제
    public bool RemoveItemByType(ItemType type)
    {


        Item target = GetItemByType(type);
        if (target != null)
        {
            Debug.Log("제거 타겟 찾기 성공했는데 왜 안되는겨");
            items.Remove(target);
            onChangeItem.Invoke();

            return true;
        }
        else Debug.Log("제거 타겟 찾기 실패");
        return false;
    }

    public int GetLevel(ItemType type)
    {
        Item item = GetItemByType(type);
        if (item != null)
            return item.itemLevel;

        return -1; // 아이템 없을 경우
    }





}