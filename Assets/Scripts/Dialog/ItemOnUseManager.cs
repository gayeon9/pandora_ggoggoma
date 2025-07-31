using UnityEngine;

public class ItemOnUseManager
{


    public static bool Execute(Item item)
    {
        switch (item.itemType)
        {

            case ItemType.Mouse:
                item.onUse = () => ExecuteMouse(item);
                Debug.Log("mouse ������ ��������Ʈ ������ �Լ� �ֱ� ����");
                return false;

            case ItemType.Jar:
                item.onUse = () => ExecuteJar(item);
                Debug.Log("jar ������ ��������Ʈ ������ �Լ� �ֱ� ����");

                return true;

            case ItemType.Newspaper:
                item.onUse = () => ExecuteNewspaper(item);
                Debug.Log("newspaper ������ ��������Ʈ ������ �Լ� �ֱ� ����");
                //?? ??
                return false;

            case ItemType.Clothesold:
                item.onUse = () => ExecuteClothesold(item);
                Debug.Log("Clocthesold ������ ��������Ʈ ������ �Լ� �ֱ� ����");
                //
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
                Debug.Log("mouse ������ ��������Ʈ �����ִ� �Լ� ȣ�� ����");

                DialogueManager.Instance.StartDialogue(
                    new[] { "�㸦 ����." },
                    DialogueMode.Dialogue
                );
                item.itemLevel++; item.consumable = false;
                return false;

            case 1:
                DialogueManager.Instance.StartDialogue(
                    new[] { "�㸦 ��Ҵ�." },
                    DialogueMode.Explanation
                );
                item.itemLevel++; item.consumable = true;
                return false;

            /* case 2:
                DialogueManager.Instance.StartDialogue(
                    new[] { "�� ���� ����, ����X" },
                    DialogueMode.Explanation
                );
                item.itemLevel++; 
                return true;
             
            case 3:
                DialogueManager.Instance.StartDialogue(
                    new[] { "����Ǿ��� �� ������ ���� ��������Ʈ ���� ������." },
                    DialogueMode.Explanation
                );
                item.itemLevel++;
                return false;  */



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
                    new[] { "�׷����� ���� ������ ������Ⱑ ������." },
                    DialogueMode.Dialogue
                );
              

                return true;



            default:
                return false;
        }
    }


    private static bool ExecuteNewspaper(Item item)
    {
        switch (item.itemLevel)
        {
            case 0:
                DialogueManager.Instance.StartDialogue(
                    new[] { "�Ź��̴�." },
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
                    new[] { "���� ���κ��� ��� �ұ�?" },
                    DialogueMode.Dialogue
                );
     
                return true;

            

            default:
                return false;
        }
    }



}