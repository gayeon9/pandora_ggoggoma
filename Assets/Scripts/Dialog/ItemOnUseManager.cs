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
                    new[] { "ù ��° ����" },
                    DialogueMode.Dialogue
                );
                item.itemLevel++; item.consumable = false;
                return false; 

            case 1:
                DialogueManager.Instance.StartDialogue(
                    new[] { "�� ��° ����, ����X" },
                    DialogueMode.Explanation
                );
                item.itemLevel++;
                return false;
            case 2:
                DialogueManager.Instance.StartDialogue(
                    new[] { "�� ���� ����, ����O" },
                    DialogueMode.Explanation
                );
                item.itemLevel++; item.consumable = true;
                return true;
             
            case 3:
                DialogueManager.Instance.StartDialogue(
                    new[] { "����Ǿ��� �� ������ ���� ��������Ʈ ���� ������." },
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
                    new[] { "��. ���� �ȵ�." },
                    DialogueMode.Dialogue
                );
     
                return true;

            

            default:
                return false;
        }
    }



}