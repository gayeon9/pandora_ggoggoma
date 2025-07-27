using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;




public class itemInitializer : MonoBehaviour
{


    public static bool Handle(Item item)
    {

        string[] lines;
        bool delete;

        switch (item.itemType)
        {

            case ItemType.Mouse:

                lines = new[] { "�� ���" };
                delete = true;
                return delete;


            case ItemType.Jar:
                if (item.itemLevel == 2)
                {
                    lines = new[] { "�� ���̴�." };
                    delete = false;
                }
                else
                {
                    delete = false;
                    return delete;
                }

                item.itemLevel++; // ��� ��� ���� ���
                DialogueManager.Instance.StartDialogue(lines, DialogueMode.Explanation);
                return delete;



            default:
                DialogueManager.Instance.StartDialogue(new[] {
                    $"{item.itemName}��(��) ��� ��� �ұ�"
                }, DialogueMode.Dialogue);
                return false;
        }
    }
}