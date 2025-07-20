using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //싱글톤 : 한 번만 생성되는 클래스
    //> 그 인스턴스를 어디서든 접근할 수 있음.
    #region Singleton
    public static Inventory instance;
    private void Awake()
    { 
        //inventory 스크립트가 두 개 이상 오브젝트에
        //등록되는 경우 예외인 듯?
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion



    //아래는 인벤토리에 변화가 생겼을 때 기능 구현
    
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    //아이템 변화, 외부에서 다음과 같이 사용가능 
    //Inventory.instance.onChangeItem += RefreshItemIcons;
    //delegate : 포인터와 유사 개념,함수 대리 호출

    private Animator markAnimator;

    //획득한 아이템을 담는 리스트
    public List<Item> items = new List<Item>();


    private int slotCnt;
    public int SlotCnt
    {
        //내부 slotCnt를 보호하면서 외부에서 값을 읽을 수 있도록함.
        //값이 바뀌면 자동으로 이벤트 변화( 슬롯변화, 버튼활성화....)
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
                    Debug.Log("markoff 애니메이션 재생됨");
                 
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
                    markAnimator.Play("markoff", 0, 0f);  // "Show"는 원하는 애니메이션 Clip 이름
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
                Debug.Log("인벤토리가 가득 찼습니다."); // 필요시 UI 메시지 출력
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
    //싱글톤 : 한 번만 생성되는 클래스
    //> 그 인스턴스를 어디서든 접근할 수 있음.
    #region Singleton
    public static Inventory instance;
    private void Awake()
    { 
        //inventory 스크립트가 두 개 이상 오브젝트에
        //등록되는 경우 예외인 듯?
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion



    //아래는 인벤토리에 변화가 생겼을 때 기능 구현
    
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    //아이템 변화, 외부에서 다음과 같이 사용가능 
    //Inventory.instance.onChangeItem += RefreshItemIcons;
    //delegate : 포인터와 유사 개념,함수 대리 호출


    //획득한 아이템을 담는 리스트
    public List<Item> items = new List<Item>();


    private int slotCnt;
    public int SlotCnt
    {
        //내부 slotCnt를 보호하면서 외부에서 값을 읽을 수 있도록함.
        //값이 바뀌면 자동으로 이벤트 변화( 슬롯변화, 버튼활성화....)
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