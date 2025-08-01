using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //인스턴스를 어디서든 접근할 수 있음.
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
                      markAnimator.Play("markoff", 0, 0f);  // "Show"는 원하는 애니메이션 Clip 이름
                  }

              }

             
            currentFieldItem = null;
            
        }
    }

    public void DestroyFromFieldandAdd()
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
            // 매번 델리게이트 갱신
            ItemOnUseManager.Execute(item);

            bool isDestroyed = item.Use(); // Use() 내부에서 onUse.Invoke() 실행
            if (isDestroyed)
            {
                DestroyFromFieldandAdd();
                Debug.Log("성공적으로 삭제");
            }
            else Debug.Log("삭제 실패");

        }

    }
    }
