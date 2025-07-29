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

                lines = new[] { "쥐 사용" };
                delete = true;
                return delete;


            case ItemType.Jar:
                if (item.itemLevel == 2)
                {
                    lines = new[] { "빈 병이다." };
                    delete = false;
                }
                else
                {
                    delete = false;
                    return delete;
                }

                item.itemLevel++; // 대사 출력 이후 상승
                DialogueManager.Instance.StartDialogue(lines, DialogueMode.Explanation);
                return delete;



            default:
                DialogueManager.Instance.StartDialogue(new[] {
                    $"{item.itemName}은(는) 어떻게 써야 할까"
                }, DialogueMode.Dialogue);
                return false;
        }
    }
}