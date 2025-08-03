using Unity.VisualScripting;
using UnityEngine;

public class ItemOnUseManager
{

    public static bool Execute(Item item)
    {
        switch (item.itemType)
        {

            case ItemType.Mouse:
                item.onUse = () => ExecuteMouse(item);
                Debug.Log("mouse 아이템 델리게이트 변수에 함수 넣기 성공");
                return false;

            case ItemType.Jar:
                item.onUse = () => ExecuteJar(item);
                Debug.Log("jar 아이템 델리게이트 변수에 함수 넣기 성공");

                return true;

            case ItemType.Newspaper:
                item.onUse = () => ExecuteNewspaper(item);
                Debug.Log("newspaper 아이템 델리게이트 변수에 함수 넣기 성공");
                //?? ??
                return false;

            case ItemType.Clothesold:
                item.onUse = () => ExecuteClothesold(item);
                Debug.Log("Clocthesold 아이템 델리게이트 변수에 함수 넣기 성공");
                //
                return true;


            case ItemType.Glass:
                item.onUse = () => ExecuteGlass(item);
                Debug.Log("Clocthesold 아이템 델리게이트 변수에 함수 넣기 성공");
                //
                return true;

            default:
                return false;
        }
    }

    private static bool ExecuteMouse(Item item)
    {
        GameObject Scriptmachine = GameObject.Find("Scriptmachine");


        switch (item.itemLevel)
        {
            case 0:
                Debug.Log("mouse 아이템 델리게이트 변수있는 함수 호출 성공");
                CustomEvent.Trigger(Scriptmachine, "MouseEvent0");
                item.itemLevel++; item.consumable = true;
                return true;

            case 1:
                CustomEvent.Trigger(Scriptmachine, "MouseEvent1");
                item.consumable = false;
                return false;

            /*
               case 2:
                  CustomEvent.Trigger(Scriptmachine, "MouseEvent2");
                   item.consumable = false;
                  return false;
             */


            default:
                return false;
        }
    }


    private static bool ExecuteJar(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                DialogueManager.Instance.StartDialogue(
                    new[] { "그러고보니 오늘 만찬에 돼지고기가 나왔지." },
                    DialogueMode.Dialogue
                );
              

                return true;



            default:
                return false;
        }
    }

    private static bool ExecuteGlass(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:  return true;
            default:
                return true;
        }
    }


    private static bool ExecuteNewspaper(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                DialogueManager.Instance.StartDialogue(
                    new[] { "신문이다." },
                    DialogueMode.Dialogue
                    
                );
                  item.consumable = true;  //????? ????...?

                return true;



            default:
                return false;
        }
    }
    
    private static bool ExecuteClothesold(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                DialogueManager.Instance.StartDialogue(
                    new[] { "낡은 하인복을 어떻게 할까?" },
                    DialogueMode.Dialogue
                );
     
                return true;

            

            default:
                return false;
        }
    }



}