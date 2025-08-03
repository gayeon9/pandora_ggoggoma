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


            case ItemType.Glass:
                item.onUse = () => ExecuteGlass(item);
                Debug.Log("Clocthesold ������ ��������Ʈ ������ �Լ� �ֱ� ����");
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
                Debug.Log("mouse ������ ��������Ʈ �����ִ� �Լ� ȣ�� ����");
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
                    new[] { "�׷����� ���� ������ ������Ⱑ ������." },
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