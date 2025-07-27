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

            default:
                return false;
        }
    }

    private static bool ExecuteMouse(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                Debug.Log("mouse 아이템 델리게이트 변수있는 함수 호출 성공");

                DialogueManager.Instance.StartDialogue(
                    new[] { "첫 번째 문장" },
                    DialogueMode.Dialogue
                );
                item.itemLevel++; item.consumable = false;
                return false; 

            case 1:
                DialogueManager.Instance.StartDialogue(
                    new[] { "두 번째 문장, 습득X" },
                    DialogueMode.Explanation
                );
                item.itemLevel++;
                return false;
            case 2:
                DialogueManager.Instance.StartDialogue(
                    new[] { "세 번쨰 문장, 습득O" },
                    DialogueMode.Explanation
                );
                item.itemLevel++; item.consumable = true;
                return true;
             
            case 3:
                DialogueManager.Instance.StartDialogue(
                    new[] { "습득되었을 때 여전히 같은 델리게이트 변수 공유함." },
                    DialogueMode.Explanation
                );
                item.itemLevel++;
                return false;



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
                    new[] { "병. 습득 안됨." },
                    DialogueMode.Dialogue
                );
     
                return true;

            

            default:
                return false;
        }
    }



}